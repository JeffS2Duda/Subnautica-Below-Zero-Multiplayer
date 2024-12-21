namespace Subnautica.Client.MonoBehaviours.Player
{
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.API.Features.Helper;
    using Subnautica.Client.Extensions;
    using Subnautica.Client.Multiplayer.Cinematics;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Playables;
    using UnityEngine.Timeline;
    using UWE;

    public class CinematicController : MonoBehaviour
    {
        public void Initialize(ZeroPlayer player)
        {
            this.ZeroPlayer = player;
            this.Player = player.PlayerModel;
            this.PlayerAnimator = player.Animator;
        }

        public bool IsValid()
        {
            return this.IsValidCinematic && this.ZeroPlayer != null && this.Target;
        }

        public void SetValid(bool isValid)
        {
            this.IsValidCinematic = isValid;
        }

        public void SetUniqueId(string uniqueId)
        {
            this.UniqueId = uniqueId;
        }

        public void SetProperties(List<GenericProperty> properties)
        {
            this.Properties = properties;
        }

        public void RegisterProperty(string key, object value)
        {
            this.Properties.Add(new GenericProperty(key, value));
        }

        public T GetProperty<T>(string key)
        {
            var property = this.Properties.Where(q => q.Key == key).FirstOrDefault();
            if (property == null || property.Value == null)
            {
                return default(T);
            }

            return (T)property.Value;
        }

        public void SetCinematicModeActive(bool isActive)
        {
            this.IsCinematicModeActive = isActive;

            if (isActive)
            {
                this.ZeroPlayer.CurrentCinematicUniqueId = this.UniqueId;
            }
            else
            {
                this.ZeroPlayer.CurrentCinematicUniqueId = null;
            }
        }

        public void SetCinematicEndMode(Action cinematicEndModeAction, bool isCinematicEndPosition = true)
        {
            this.CinematicEndModeAction = cinematicEndModeAction;
            this.IsCinematicEndPosition = isCinematicEndPosition;
        }

        public void SetState(global::PlayerCinematicController.State state)
        {
            this.TimeStateChanged = Time.time;
            this.State = state;
        }

        public bool SetAnimState(bool state)
        {
            if (this.AnimState == state)
            {
                return false;
            }

            this.AnimState = state;

            if (this.Director)
            {
                if (state)
                {
                    this.Director.GotoAndPlay(this.DirectorLabelTrack, this.DirectorLabel);
                }
                else
                {
                    this.Director.Stop();
                }
            }

            if (this.Animator && this.AnimParam.IsNotNull())
            {
                if (this.IsHasExitTime)
                {
                    if (state)
                    {
                        this.Animator.Play(this.AnimParam);
                    }
                    else
                    {
                        this.Animator.Update(30f);
                    }
                }
                else
                {
                    this.Animator.SetBool(this.AnimParam, state);
                }
            }

            if (this.ReceiversAnimParam.IsNotNull())
            {
                for (int index = 0; index < this.AnimParamReceivers.Length; ++index)
                {
                    this.AnimParamReceivers[index].GetComponent<IAnimParamReceiver>()?.ForwardAnimationParameterBool(this.ReceiversAnimParam, state);
                }
            }

            if (this.PlayerViewAnimationName.IsNotNull() && this.Player)
            {
                if (this.PlayerAnimator && this.PlayerAnimator.gameObject.activeInHierarchy)
                {
                    Log.Info("PlayerAnimator: " + this.PlayerViewAnimationName + ", AnimParam: " + this.AnimParam + ", state => " + state + ", IsHasExitTime: " + this.IsHasExitTime);
                    if (this.IsHasExitTime)
                    {
                        if (state)
                        {
                            this.PlayerAnimator.Play(this.PlayerViewAnimationName);
                        }
                        else
                        {
                            this.PlayerAnimator.Update(30f);
                        }
                    }
                    else
                    {
                        this.PlayerAnimator.SetBool(this.PlayerViewAnimationName, state);
                    }
                }
            }

            return true;
        }

        public void SetCinematic(global::PlayerCinematicController cinematic, bool isHasExitTime = false, bool isFastInterpolation = true, bool isSkipEndAnimation = false, bool isSkipFirstAnimation = true)
        {
            this.CinematicEndModeAction = null;
            this.IsCinematicEndPosition = true;
            this.IsHasExitTime = isHasExitTime;
            this.ReceiversAnimParam = cinematic.receiversAnimParam;
            this.AnimParamReceivers = cinematic.animParamReceivers;
            this.PlayerViewAnimationName = cinematic.playerViewAnimationName;
            this.PlayerViewInterpolateAnimParam = cinematic.playerViewInterpolateAnimParam;
            this.InterpolateAnimParam = cinematic.interpolateAnimParam;
            this.InterpolationTime = isFastInterpolation ? 0.0f : cinematic.interpolationTime;
            this.IsSkipFirstAnimation = isSkipFirstAnimation;
            this.IsSkipEndAnimation = isSkipEndAnimation;
            this.InterpolationTimeOut = cinematic.interpolationTimeOut;
            this.InterpolationDelayTime = cinematic.interpolationDelayTime;
            this.InterpolateDuringAnimation = cinematic.interpolateDuringAnimation;
            this.EnforceCinematicModeEnd = true;
            this.AnimParam = cinematic.animParam;

            this.Animator = cinematic.animator;
            this.AnimatedTransform = cinematic.animatedTransform;
            this.EndTransform = cinematic.endTransform;
            this.Director = cinematic.director;
            this.DirectorLabelTrack = cinematic.labelTrack;
            this.DirectorLabel = cinematic.label;
            this.LayerId = this.GetLayerId();

            this.DirectorInitialize();
        }

        public virtual void OnStart(PlayerCinematicQueueItem item)
        {
            this.Target = Network.Identifier.GetGameObject(item.UniqueId, true);
        }

        public virtual void OnResetAnimations(PlayerCinematicQueueItem item)
        {

        }

        public virtual IEnumerator OnResetAnimationsAsync(PlayerCinematicQueueItem item)
        {
            yield return CoroutineUtils.waitForNextFrame;

            for (int i = 0; i < 15; i++)
            {
                if (!this.Target)
                {
                    yield return CoroutineUtils.waitForFixedUpdate;

                    this.OnStart(item);
                }
            }

            if (!this.Target)
            {
                this.SetValid(false);
            }

            this.EnableResetCinematic();
        }

        public virtual bool OnPlayerDisconnected()
        {
            return false;
        }

        public bool StartCinematicMode()
        {
            if (this.ZeroPlayer == null || this.ZeroPlayer.PlayerModel == null)
            {
                return false;
            }

            this.Stopwatch.Restart();

            this.ZeroPlayer.ResetAnimations();
            this.ZeroPlayer.SetHandItem(TechType.None);

            this.Animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
            this.PlayerAnimator.cullingMode = AnimatorCullingMode.AlwaysAnimate;

            this.SetCinematicModeActive(true);
            this.SetState(global::PlayerCinematicController.State.In);

            if (this.Player)
            {
                this.PlayerFromPosition = this.Player.transform.position;
                this.PlayerFromRotation = this.Player.transform.rotation;

                if (this.PlayerAnimator && this.PlayerViewInterpolateAnimParam.IsNotNull())
                {
                    this.PlayerAnimator.SetBool(this.PlayerViewInterpolateAnimParam, true);
                }
            }

            if (this.Animator && this.InterpolateAnimParam.IsNotNull())
            {
                this.Animator.SetBool(this.InterpolateAnimParam, true);
            }

            if (this.InterpolateDuringAnimation)
            {
                this.SetAnimState(true);
            }

            return true;
        }

        public void LateUpdate()
        {
            if (!this.IsCinematicModeActive)
            {
                return;
            }

            float num = Time.time - this.TimeStateChanged;

            switch (this.State)
            {
                case global::PlayerCinematicController.State.In:

                    float t1;

                    if (this.InterpolationTime == 0.0)
                    {
                        t1 = 1f;
                    }
                    else
                    {
                        if (this.IsSkipFirstAnimation)
                        {
                            t1 = 1f;

                            this.SkipFirstCinematic(num);
                        }
                        else
                        {
                            if (this.InterpolateDuringAnimation)
                            {
                                num -= this.InterpolationDelayTime;
                            }

                            t1 = Mathf.Clamp01(num / this.InterpolationTime);
                        }
                    }

                    if (this.Player)
                    {
                        this.Player.transform.position = Vector3.Lerp(this.PlayerFromPosition, this.AnimatedTransform.position, t1);
                        this.Player.transform.rotation = Quaternion.Slerp(this.PlayerFromRotation, this.AnimatedTransform.rotation, t1);
                    }

                    if (t1 != 1.0f)
                    {
                        break;
                    }

                    this.SetState(global::PlayerCinematicController.State.Update);
                    this.SetAnimState(true);

                    if (this.Animator && this.InterpolateAnimParam.IsNotNull())
                    {
                        this.Animator.SetBool(this.InterpolateAnimParam, false);
                    }

                    if (this.Player && this.PlayerAnimator && this.PlayerViewInterpolateAnimParam.IsNotNull())
                    {
                        this.PlayerAnimator.SetBool(this.PlayerViewInterpolateAnimParam, false);
                    }

                    break;

                case global::PlayerCinematicController.State.Update:

                    if (this.Player)
                    {
                        this.UpdatePlayerPosition();
                    }

                    if (!this.EnforceCinematicModeEnd || this.Animator.IsInTransition(0))
                    {
                        break;
                    }

                    if (this.LayerId == -1)
                    {
                        break;
                    }

                    var animatorStateInfo = this.Animator.GetCurrentAnimatorStateInfo(this.LayerId);
                    if (num < animatorStateInfo.length)
                    {
                        break;
                    }

                    this.OnPlayerCinematicModeEnd();

                    break;

                case global::PlayerCinematicController.State.Out:

                    float t2 = this.IsSkipEndAnimation || this.InterpolationTimeOut == 0.0 ? 1f : Mathf.Clamp01(num / this.InterpolationTimeOut);

                    this.Player.transform.position = Vector3.Lerp(this.PlayerFromPosition, this.EndTransform.position, t2);
                    this.Player.transform.rotation = Quaternion.Slerp(this.PlayerFromRotation, this.EndTransform.rotation, t2);

                    if (t2 != 1.0f)
                    {
                        break;
                    }

                    this.EndCinematicMode(false);

                    break;
            }
        }

        public bool OnPlayerCinematicModeEnd()
        {
            if (!this.IsCinematicModeActive)
            {
                return false;
            }

            if (this.Player)
            {
                this.UpdatePlayerPosition();
            }

            if (this.EndTransform)
            {
                this.SetAnimState(false);
                this.SetState(global::PlayerCinematicController.State.Out);

                if (this.Player)
                {
                    this.PlayerFromPosition = this.Player.transform.position;
                    this.PlayerFromRotation = this.Player.transform.rotation;
                }
            }
            else
            {
                this.EndCinematicMode();
            }

            return true;
        }

        private void SkipFirstCinematic(float num)
        {
            float currentTime = 0f;
            float elapsedTime = 0f;
            while (currentTime < 1f || elapsedTime < 3f)
            {
                elapsedTime += Time.unscaledDeltaTime;

                this.Animator.Update(Time.unscaledDeltaTime);

                currentTime = Mathf.Clamp01((num + elapsedTime) / this.InterpolationTime);
            }
        }

        public void UpdatePlayerPosition()
        {
            this.Player.transform.position = this.AnimatedTransform.position;
            this.Player.transform.rotation = this.AnimatedTransform.rotation;
        }

        public void UpdateLastPosition()
        {
            if (this.ZeroPlayer != null)
            {
                this.ZeroPlayer.ResetAnimations();

                this.ZeroPlayer.Position = this.Player.transform.position;
                this.ZeroPlayer.Rotation = this.Player.transform.rotation;
                this.ZeroPlayer.DisableCinematicMode();
            }
        }

        public bool EndCinematicMode(bool updatePosition = true)
        {
            if (!this.IsCinematicModeActive)
            {
                return false;
            }

            this.ZeroPlayer.IsVehicleDocking = false;

            this.Stopwatch.Stop();

            Log.Info("Animation Time: " + this.Stopwatch.ElapsedMilliseconds);

            Network.HandTarget.AddTemporaryBlock(this.UniqueId);

            if (this.Director)
            {
                this.Director.stopped -= new Action<PlayableDirector>(this.OnDirectorStopped);
                this.Director.time = this.Director.duration;
                this.Director.Evaluate();
            }

            if (this.Player && updatePosition)
            {
                this.UpdatePlayerPosition();
            }

            this.UpdateLastPosition();

            this.CinematicEndModeAction?.Invoke();

            this.SetValid(false);
            this.SetAnimState(false);
            this.SetState(global::PlayerCinematicController.State.None);
            this.SetCinematicModeActive(false);
            this.SetUniqueId(null);

            if (this.IsCinematicEndPosition)
            {
                if (this.Player && updatePosition)
                {
                    this.UpdatePlayerPosition();
                }

                this.UpdateLastPosition();
            }

            return true;
        }

        private void OnDirectorStopped(PlayableDirector director)
        {
            this.EndCinematicMode();
        }

        private int GetLayerId()
        {
            if (this.Animator.layerCount <= 0)
            {
                return -1;
            }

            if (this is BulkheadDoorCinematic)
            {
                return 1;
            }

            if (this is UseableDiveHatchCinematic)
            {
                return this.Animator.layerCount - 1;
            }

            return 0;
        }

        private void DirectorInitialize()
        {
            if (this.Director)
            {
                this.Director.stopped -= new Action<PlayableDirector>(this.OnDirectorStopped);
                this.Director.stopped += new Action<PlayableDirector>(this.OnDirectorStopped);

                foreach (var output in this.Director.playableAsset.outputs)
                {
                    var sourceObject = output.sourceObject;
                    var streamName = output.streamName;

                    var type = sourceObject?.GetType();
                    if (type == typeof(AnimationTrack))
                    {
                        if (streamName == "Player")
                        {
                            this.Director.SetGenericBinding(sourceObject, this.PlayerAnimator);
                        }
                    }
                }
            }
        }

        public void OnDisable()
        {
            this.EndCinematicMode();
        }

        public void EnableResetCinematic()
        {
            this.IsNotActiveResetCinematic = true;
        }

        public void DisableResetCinematic()
        {
            this.IsNotActiveResetCinematic = false;
        }

        public string UniqueId { get; set; }

        public bool IsCinematicModeActive { get; set; } = false;

        public GameObject Target { get; set; }

        public int LayerId { get; set; }

        private bool IsHasExitTime { get; set; }

        public List<GenericProperty> Properties { get; set; } = new List<GenericProperty>();

        public Action CinematicEndModeAction { get; set; }

        public bool IsCinematicEndPosition { get; set; }

        public bool IsRebindAllowed { get; set; }

        public ZeroPlayer ZeroPlayer { get; set; }

        public float TimeStateChanged { get; set; } = 0f;

        public global::PlayerCinematicController.State State { get; set; }

        public bool AnimState { get; set; } = false;

        public Vector3 PlayerFromPosition { get; set; } = Vector3.zero;

        public Quaternion PlayerFromRotation { get; set; } = Quaternion.identity;

        public GameObject Player { get; set; }

        public Animator PlayerAnimator { get; set; }

        public Animator Animator { get; set; }

        public Transform AnimatedTransform { get; set; }

        public Transform EndTransform { get; set; }

        public PlayableDirector Director { get; set; }

        public string DirectorLabelTrack { get; set; }

        public PropertyName DirectorLabel { get; set; }

        public GameObject[] AnimParamReceivers { get; set; }

        public Stopwatch Stopwatch { get; set; } = new Stopwatch();

        public bool IsNotActiveResetCinematic { get; private set; } = true;

        public bool IsValidCinematic { get; set; }

        public string ReceiversAnimParam { get; set; }
        public string PlayerViewAnimationName { get; set; }
        public string PlayerViewInterpolateAnimParam { get; set; }
        public string InterpolateAnimParam { get; set; }
        public string AnimParam { get; set; }

        public float InterpolationTime { get; set; }
        public float InterpolationTimeOut { get; set; }
        public float InterpolationDelayTime { get; set; }

        public bool IsSkipFirstAnimation { get; set; }
        public bool IsSkipEndAnimation { get; set; }
        public bool InterpolateDuringAnimation { get; set; }
        public bool EnforceCinematicModeEnd { get; set; }
    }
}