namespace Subnautica.API.Features
{
    using Subnautica.API.Enums;
    using Subnautica.API.Extensions;
    using Subnautica.API.Features.PlayerUtility;
    using Subnautica.API.MonoBehaviours;
    using Subnautica.Network.Core.Components;
    using Subnautica.Network.Models.Server;
    using Subnautica.Network.Structures;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class ZeroPlayer
    {
        public const string PlayerSignalName = "MultiplayerPlayerSignal";

        public const string DontUseThisMethod = "You can't use this method if you are the player! This method is only available for other players.";

        public static bool IsPlayerMine(string uniqueId)
        {
            if (uniqueId.IsNull())
            {
                return false;
            }

            var player = ZeroPlayer.GetPlayerByUniqueId(uniqueId);
            return player != null && player.IsMine;
        }

        public static bool IsPlayerMine(byte playerId)
        {
            var player = ZeroPlayer.GetPlayerById(playerId);
            return player != null && player.IsMine;
        }

        public ZeroPlayer(string uniqueId, bool isLocalPlayer = false)
        {
            this.UniqueId = uniqueId;
            this.IsMine = isLocalPlayer;

            if (isLocalPlayer)
            {
                this.PlayerObject = new GameObject(String.Format("MP_Client_{0}", Guid.NewGuid()));
                this.SetCurrentPlayer(this);
            }

            this.SetDefaultAnimationQueue();
            this.AddPlayerToList(this);
        }

        public void AddPlayerToList(ZeroPlayer player)
        {
            Players.Add(player);
        }

        public static ZeroPlayer CreateOrGetPlayerByUniqueId(string uniqueId, byte playerId)
        {
            var player = Players.Where(q => q.UniqueId == uniqueId).FirstOrDefault() ?? new ZeroPlayer(uniqueId, false);
            player.PlayerId = playerId;
            return player;
        }

        public static ZeroPlayer GetPlayerByUniqueId(string uniqueId)
        {
            if (uniqueId.IsNull())
            {
                return null;
            }

            return Players.Where(q => q.UniqueId == uniqueId).FirstOrDefault();
        }

        public static ZeroPlayer GetPlayerByGameObject(GameObject gameObject)
        {
            return Players.Where(q => q.PlayerModel == gameObject).FirstOrDefault();
        }

        public static ZeroPlayer GetPlayerByVehicleGameObject(GameObject vehicleGameObject)
        {
            if (vehicleGameObject == null)
            {
                return null;
            }

            foreach (var player in ZeroPlayer.GetAllPlayers())
            {
                if (player.GetVehicle() == vehicleGameObject)
                {
                    return player;
                }
            }

            return null;
        }

        public static ZeroPlayer GetPlayerById(byte playerId)
        {
            if (playerId <= 0)
            {
                return null;
            }

            return Players.Where(q => q.PlayerId == playerId).FirstOrDefault();
        }

        public static ZeroPlayer GetPlayerById(string playerId)
        {
            return Players.Where(q => q.UniqueId == playerId).FirstOrDefault();
        }

        public static List<ZeroPlayer> GetPlayers()
        {
            return Players.Where(q => !q.IsMine).ToList();
        }

        public static List<ZeroPlayer> GetAllPlayers()
        {
            return Players.ToList();
        }

        public static PlayerRange GetPlayersByInRange(Vector3 sourcePosition, float range, bool inVehicle = false)
        {
            var playerRange = new PlayerRange();

            foreach (var player in ZeroPlayer.GetAllPlayers())
            {
                var distance = ZeroVector3.Distance(player.PlayerModel.transform.position, sourcePosition);
                if (distance > range)
                {
                    continue;
                }

                if (inVehicle && player.GetVehicle() == null)
                {
                    continue;
                }

                if (distance < playerRange.NearestPlayerDistance)
                {
                    playerRange.SetNearestPlayer(player, distance);
                }

                if (distance > playerRange.FarthestPlayerDistance)
                {
                    playerRange.SetFarthestPlayer(player, distance);
                }

                playerRange.AddPlayer(player, distance);
            }

            return playerRange;
        }

        public static void DisposeAll()
        {
            try
            {
                if (ZeroPlayer.CurrentPlayer?.PlayerObject)
                {
                    GameObject.DestroyImmediate(ZeroPlayer.CurrentPlayer.PlayerObject.gameObject);
                }

                ZeroPlayer.CurrentPlayer = null;
                ZeroPlayer.Players.Clear();

                global::Player.allowSaving = true;
            }
            catch (Exception e)
            {
                Log.Error($"ZeroPlayer.DisposeAll Exception: {e}");
            }
        }

        public void SetVelocity(Vector3 velocity)
        {
            if (!this.IsMine)
            {
                this.Velocity = velocity;
            }
        }

        public void SetUniqueId(string uniqueId)
        {
            this.UniqueId = uniqueId;
        }

        public bool IsHypnotized()
        {
            if (this.IsMine)
            {
                return this.Main.lilyPaddlerHypnosis.IsHypnotized();
            }

            return this.LastHypnotizeTime > Network.Session.GetWorldTime();
        }

        public void SetAnimationQueue(Dictionary<string, bool> animations)
        {
            if (this.IsMine)
            {
                throw new Exception(DontUseThisMethod);
            }
            else
            {
                if (animations != null)
                {
                    foreach (var animation in animations)
                    {
                        if (PlayerAnimationTypeExtensions.Animations.Contains(animation.Key))
                        {
                            this.Animations[animation.Key] = animation.Value;
                        }
                    }

                    this.AnimationQueue.Enqueue(this.Animations.ToDictionary(entry => entry.Key, entry => entry.Value));
                }
            }
        }

        public void SetSelfieMode(float selfieId)
        {
            if (this.IsMine)
            {
                this.Main.playerAnimator.SetBool("selfies", true);
                this.Main.playerAnimator.SetFloat("selfie_number", selfieId);
            }
            else
            {
                this.Animator?.SetBool("selfies", true);
                this.Animator?.SetFloat("selfie_number", selfieId);
            }
        }

        private void SetDefaultAnimationQueue()
        {
            if (!this.IsMine)
            {
                this.ClearAnimationQueue();
                this.SetAnimationQueue(PlayerAnimationTypeExtensions.Animations.ToDictionary(q => q, x => false));
            }
        }

        public Vector3 GetVelocity()
        {
            if (this.IsMine)
            {
                return this.Main.rigidBody.velocity;
            }

            return this.Velocity;
        }

        public GameObject GetVehicle()
        {
            if (this.IsMine)
            {
                if (this.Main.mode != global::Player.Mode.LockedPiloting)
                {
                    return null;
                }

                var parentTransform = this.Main.transform.parent;
                if (parentTransform == null)
                {
                    return null;
                }

                var techType = parentTransform.gameObject.GetTechType();
                if (techType == TechType.SeaTruck)
                {
                    var seaTruckMotor = this.Main.GetComponentInParent<global::SeaTruckMotor>();
                    if (seaTruckMotor && seaTruckMotor.IsPiloted())
                    {
                        return seaTruckMotor.gameObject;
                    }
                }
                else if (techType == TechType.Exosuit)
                {
                    var exosuit = this.Main.GetComponentInParent<global::Vehicle>();
                    if (exosuit && exosuit.pilotId.IsNotNull())
                    {
                        return exosuit.gameObject;
                    }
                }
                else if (techType == TechType.Hoverbike)
                {
                    var hoverBike = this.Main.GetComponentInParent<global::Hoverbike>();
                    if (hoverBike && hoverBike.isPiloting)
                    {
                        return hoverBike.gameObject;
                    }
                }

                return null;
            }

            if (this.VehicleId <= 0)
            {
                return null;
            }

            var vehicle = Network.DynamicEntity.GetEntity(this.VehicleId);
            if (vehicle == null || !vehicle.TechType.IsVehicle())
            {
                return null;
            }

            return vehicle.GameObject;
        }

        public bool IsPlayerInVoid()
        {
            if (VoidLeviathansSpawner.main == null)
            {
                return false;
            }

            if (this.IsMine)
            {
                return VoidLeviathansSpawner.main.IsVoidBiome(this.Main.GetBiomeString());
            }

            return VoidLeviathansSpawner.main.IsVoidBiome(LargeWorld.main.GetBiome(this.PlayerModel.transform.position));
        }

        public bool CanBeAttacked()
        {
            if (this.IsMine)
            {
                return this.Main.CanBeAttacked();
            }

            if (GameModeManager.HasNoCreatureAggression())
            {
                return false;
            }

            if (this.IsFrozen)
            {
                return false;
            }

            if (this.IsCinematicModeActive || this.CurrentSubRootId.IsNotNull())
            {
                return false;
            }

            if (this.CurrentInteriorId.IsNotNull())
            {
                return false;
            }

            return true;
        }

        public bool CanHypnotizePlayer()
        {
            if (this.IsMine)
            {
                return this.Main.CanBeAttacked() && this.Main.IsSwimming() && !this.Main.IsInside() && !this.IsHypnotized() && !this.Main.frozenMixin.IsFrozen() && !this.Main.cinematicModeActive;
            }

            if (!this.CanBeAttacked())
            {
                return false;
            }

            if (!this.IsUnderwater)
            {
                return false;
            }

            if (this.IsHypnotized())
            {
                return false;
            }

            return true;
        }

        public bool LooksAtMe(GameObject target, bool checkPhysics = true)
        {
            var direction = Vector3.Normalize(target.transform.position - this.PlayerModel.transform.position);
            var transform = this.IsMine ? MainCameraControl.main.transform : this.PlayerModel.transform;
            var forward = this.IsMine ? MainCameraControl.main.transform.forward : this.CameraForward;

            if (Vector3.Dot(direction, forward) < 0.65)
            {
                return false;
            }

            if (checkPhysics)
            {
                int num = UWE.Utils.RaycastIntoSharedBuffer(transform.position, direction, Vector3.Distance(target.transform.position, transform.position), -1, QueryTriggerInteraction.Ignore);
                for (int index = 0; index < num; ++index)
                {
                    var raycastHit = UWE.Utils.sharedHitBuffer[index];
                    var gameObject = raycastHit.collider.attachedRigidbody == null ? raycastHit.collider.gameObject : raycastHit.collider.attachedRigidbody.gameObject;
                    if (gameObject != this.PlayerModel && gameObject != target)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool CreateModel(Vector3 position, Quaternion rotation)
        {
            if (this.IsMine)
            {
                return false;
            }

            if (this.IsCreatedModel)
            {
                return false;
            }

            var playerModel = GameObject.Find("player_view_female");
            if (playerModel == null)
            {
                return false;
            }

            this.Position = position;
            this.Rotation = rotation;

            this.PlayerModel = UnityEngine.Object.Instantiate<GameObject>(playerModel, this.Position, this.Rotation);
            this.PlayerModel.AddComponent<Rigidbody>().mass = global::Player.main.rigidBody.mass;
            this.PlayerModel.AddComponent<TechTag>().type = TechType.Player;
            this.PlayerModel.layer = LayerID.Player;
            this.PlayerModel.name = this.NickName;

            Network.Identifier.SetIdentityId(this.PlayerModel, this.UniqueId);

            this.Animator = this.PlayerModel.GetComponent<Animator>();

            this.RightHandItemTransform = this.PlayerModel.gameObject.transform.Find(GameIndex.PLAYER_ATTACH_IN_RIGHT_HAND);
            this.LeftHandItemTransform = this.PlayerModel.gameObject.transform.Find(GameIndex.PLAYER_ATTACH_IN_LEFT_HAND);
            this.LeftHandItemTransform.gameObject.SetActive(false);

            Component[] components = this.PlayerModel.GetComponents(typeof(Component));

            for (int i = 0; i < components.Length; i++)
            {
                var castedToBehaviour = components[i] as MonoBehaviour;
                if (castedToBehaviour != null)
                {
                    castedToBehaviour.enabled = false;
                }
            }

            foreach (Transform child in this.RightHandItemTransform)
            {
                if (!child.gameObject.name.Contains("attach1_"))
                {
                    UnityEngine.Object.Destroy(child.gameObject);
                }
            }

            this.CreateObstacle();
            this.CreateCapsuleCollider();
            this.CreatePingInstance();
            this.CreateEcoTarget();

            this.IsCreatedModel = true;
            this.PlayerModel.SetActive(true);

            return true;
        }

        public void OnCurrentPlayerLoaded()
        {
            this.PlayerModel.SetIdentityId(this.UniqueId);
        }

        public void ResetSelfieMode()
        {
            if (this.IsMine)
            {
                this.Main.playerAnimator.SetBool("selfies", false);
            }
            else
            {
                this.Animator?.SetBool("selfies", false);
            }
        }

        public string GetCurrentWaterParkUniqueId()
        {
            if (this.IsMine)
            {
                if (this.Main.currentWaterPark == null)
                {
                    return null;
                }

                var baseDeconstructable = this.Main.currentWaterPark.GetBaseDeconstructable();
                if (baseDeconstructable == null)
                {
                    return null;
                }

                return baseDeconstructable.gameObject.GetIdentityId();
            }

            return null;
        }

        public void ResetEmotes()
        {
            this.EmoteIndex = 0.0f;

            if (this.Animator)
            {
                this.Animator.SetFloat("FP_Emotes", 0.0f);
            }
        }

        public void ResetAnimations()
        {
            if (this.Animator)
            {
                foreach (var animation in this.Animations)
                {
                    SafeAnimator.SetBool(this.Animator, animation.Key, false);
                }
            }

            this.ResetEmotes();
            this.ResetSelfieMode();
            this.ClearAnimationQueue();
        }

        public void SetLastHypnotizeTime(float time)
        {
            this.LastHypnotizeTime = time;
        }

        public void SetUsingRoomId(string roomId)
        {
            this.UsingRoomId = roomId;
        }

        public void EnableMovement()
        {
            this.IsMovementActive = true;
        }

        public void DisableMovement()
        {
            this.IsMovementActive = false;
        }

        public void EnableCinematicMode()
        {
            this.IsCinematicModeActive = true;
        }

        public void DisableCinematicMode()
        {
            this.IsCinematicModeActive = false;
        }

        public void EnableFreeze(float time = -1f)
        {
            this.IsFrozen = true;

            if (this.FrozenOverlay)
            {
                this.FrozenOverlay.RemoveOverlay();
            }

            this.FrozenOverlay = this.PlayerModel.AddComponent<VFXOverlayMaterial>();
            this.FrozenOverlay.ApplyAndForgetOverlay(global::Player.main.frozenMixin.iceMaterial, "VFXOverlay: Frozen", Color.clear, time, this.GetRenderers(true));

            this.Animator.SetBool(AnimatorHashID.frozen, true);
            this.Animator.speed = 0.0f;
        }

        public void DisableFreeze()
        {
            this.IsFrozen = false;

            if (this.FrozenOverlay)
            {
                this.FrozenOverlay.RemoveOverlay();
            }

            this.Animator.SetBool(AnimatorHashID.frozen, false);
            this.Animator.speed = 1f;
        }

        public void SendMessageToParent(string eventName, object value = null)
        {
            if (this.PlayerModel)
            {
                if (value == null)
                {
                    this.PlayerModel.SendMessageUpwards(eventName);
                }
                else
                {
                    this.PlayerModel.SendMessageUpwards(eventName, value);
                }
            }
        }

        public void SetParent(Transform parent, bool resetPositions = false)
        {
            if (this.PlayerModel)
            {
                this.PlayerModel.transform.parent = parent;

                if (resetPositions)
                {
                    this.PlayerModel.transform.localPosition = Vector3.zero;
                    this.PlayerModel.transform.localRotation = Quaternion.identity;
                }
            }
        }

        public void ClearAnimationQueue()
        {
            this.AnimationQueue.Clear();
        }

        public void SetHandItemComponent(NetworkPlayerItemComponent component)
        {
            this.HandItemComponent = component;
        }

        public void SetCameraPitch(float cameraPitch)
        {
            this.CameraPitch = cameraPitch;
        }

        public void SetCameraForward(Vector3 cameraForward)
        {
            this.CameraForward = cameraForward;
        }

        public void InstantyAnimationMode()
        {
            if (this.Animator)
            {
                this.Animator.speed = 999f;
            }
        }

        public void NormalAnimationMode()
        {
            if (this.Animator)
            {
                this.Animator.speed = 1f;
            }
        }

        public void SetCurrentPlayer(ZeroPlayer player)
        {
            CurrentPlayer = player;
        }

        public void SetPlayerName(string nickName)
        {
            this.NickName = nickName;
        }

        public void SetSubRootId(string subrootId)
        {
            this.CurrentSubRootId = subrootId;
        }

        public void SetInteriorId(string interiorId)
        {
            this.CurrentInteriorId = interiorId;
        }

        public void SetSurfaceType(VFXSurfaceTypes surfaceType)
        {
            this.CurrentSurfaceType = surfaceType;
        }

        public void SetUsingToolFirstMode(bool isActive)
        {
            this.Animator?.SetBool("using_tool_first", isActive);
        }


        public bool IsAnimationActive(string animation)
        {
            return this.Animations.TryGetValue(animation, out bool value) && value;
        }

        public void Hide(bool instanty = true)
        {
            if (this.IsVisible && this.PlayerModel)
            {
                this.IsVisible = false;

                this.StopFading();

                if (instanty)
                {
                    this.PlayerModel.SetActive(this.IsVisible);
                }
                else
                {
                    this.StartFading(false);
                }
            }
        }

        public void Show(bool instanty = true)
        {
            if (!this.IsVisible && this.PlayerModel)
            {
                this.IsVisible = true;

                if (this.Position != null)
                {
                    this.PlayerModel.transform.position = this.Position;
                    this.PlayerModel.transform.rotation = this.Rotation;
                }

                this.StopFading();

                if (instanty)
                {
                    this.SetOpacity(1f);
                    this.PlayerModel.SetActive(this.IsVisible);
                }
                else
                {
                    this.StartFading(true);
                }
            }
        }

        private void StopFading()
        {
            if (this.FadeCoroutine != null)
            {
                UWE.CoroutineHost.StopCoroutine(this.FadeCoroutine);

                this.FadeCoroutine = null;
            }
        }

        private void StartFading(bool isShow)
        {
            this.FadeCoroutine = UWE.CoroutineHost.StartCoroutine(this.StartFadingAsync(this.FadeTime, isShow));
        }

        private IEnumerator StartFadingAsync(float fadeTime, bool isShow)
        {
            if (isShow)
            {
                this.PlayerModel.SetActive(true);
            }

            while (fadeTime > 0f)
            {
                fadeTime -= Time.unscaledDeltaTime;

                var opacity = Mathf.Clamp01(fadeTime / this.FadeTime);

                if (isShow)
                {
                    opacity = 1f - opacity;
                }

                this.SetOpacity(opacity);

                yield return UWE.CoroutineUtils.waitForNextFrame;
            }

            if (!isShow)
            {
                this.PlayerModel.SetActive(false);
            }
        }

        public void SetOpacity(float opacity)
        {
            if (opacity <= 0.0001f)
            {
                opacity = 0.0001f;
            }

            if (opacity > 1f)
            {
                opacity = 1f;
            }

            foreach (var fadeRenderer in this.GetRenderers(false))
            {
                fadeRenderer.fadeAmount = opacity;
            }
        }


        public Renderer[] GetRenderers(bool isOnlyPlayer)
        {
            if (isOnlyPlayer == false)
            {
                return this.PlayerModel.GetComponentsInChildren<Renderer>(true);
            }

            var renderers = new List<Renderer>();

            foreach (var item in this.PlayerModel.GetComponentsInChildren<Renderer>(true))
            {
                if (item.name.Contains("female_base_"))
                {
                    renderers.Add(item);
                }
            }

            return renderers.ToArray();
        }

        private bool CreateObstacle()
        {
            this.PlayerModel.AddComponent<PlayerObstacle>();
            return true;
        }

        private bool CreateCapsuleCollider()
        {
            CapsuleCollider capsuleCollider = global::Player.mainCollider as CapsuleCollider;
            if (capsuleCollider == null)
            {
                return false;
            }

            var collider = this.PlayerModel.AddComponent<CapsuleCollider>();
            collider.center = Vector3.up;
            collider.radius = capsuleCollider.radius;
            collider.direction = capsuleCollider.direction;
            collider.contactOffset = capsuleCollider.contactOffset;
            collider.isTrigger = true;
            return true;
        }

        private void CreatePingInstance()
        {
            this.PingInstance = this.PlayerModel.AddComponent<PingInstance>();
            this.PingInstance.name = ZeroPlayer.PlayerSignalName;
            this.PingInstance.origin = this.PlayerModel.transform;
            this.PingInstance.minDist = 5f;
            this.PingInstance.range = 1f;
            this.PingInstance.SetLabel(this.NickName);
            this.PingInstance.SetType(PingType.Signal);
        }

        private bool CreateEcoTarget()
        {
            if (global::Player.main.TryGetComponent<EcoTarget>(out var ecoTarget))
            {
                this.PlayerModel.AddComponent<EcoTarget>().SetTargetType(ecoTarget.GetTargetType());
                return true;
            }

            return true;
        }

        public static bool Destroy(string playerUniqueId)
        {
            var player = GetPlayerByUniqueId(playerUniqueId);

            return Destroy(player);
        }

        public static bool Destroy(ZeroPlayer player)
        {
            if (player == null)
            {
                return false;
            }

            player.IsDestroyed = true;

            if (player.PlayerObject != null)
            {
                GameObject.DestroyImmediate(player.PlayerObject);
            }

            if (player.PlayerModel != null)
            {
                GameObject.DestroyImmediate(player.PlayerModel);
            }

            Players.Remove(player);
            return true;
        }

        private GameObject _PlayerModel;

        private global::Player _Main;

        private FreecamController _FreeCamController;

        public GameObject PlayerModel
        {
            get
            {
                if (this.IsMine)
                {
                    return global::Player.main.gameObject;
                }

                return this._PlayerModel;
            }
            set
            {
                if (this.IsMine)
                {
                    throw new Exception("Don't, Not Possible!");
                }

                this._PlayerModel = value;
            }
        }

        public global::Player Main
        {
            get
            {
                if (this.IsMine == false)
                {
                    return null;
                }

                if (this._Main == null)
                {
                    this._Main = global::Player.main;
                }

                return this._Main;
            }
        }

        public FreecamController FreecamController
        {
            get
            {
                if (this.IsMine == false)
                {
                    return null;
                }

                if (this._FreeCamController == null && MainCameraControl.main)
                {
                    this._FreeCamController = MainCameraControl.main.GetComponent<FreecamController>();
                }

                return this._FreeCamController;
            }
        }

        public Vector3 Velocity { private get; set; }































        private static HashSet<ZeroPlayer> Players { get; set; } = new HashSet<ZeroPlayer>();

        public static ZeroPlayer CurrentPlayer { get; set; } = null;

        public Transform RightHandItemTransform { get; set; } = null;

        public Transform LeftHandItemTransform { get; set; } = null;

        private VFXOverlayMaterial FrozenOverlay { get; set; }

        public byte PlayerId { get; set; }

        public string UniqueId { get; set; }

        public float EmoteIndex { get; set; }

        public bool IsMine { get; set; }

        private bool IsVisible { get; set; } = true;

        public bool IsUnderwater { get; set; }

        public bool IsOnSurface { get; set; }

        public bool IsInSeaTruck { get; set; }

        public string CurrentServerId { get; set; }

        public string CurrentSubRootId { get; set; }

        public string CurrentInteriorId { get; set; }

        public VFXSurfaceTypes CurrentSurfaceType { get; set; }

        public bool IsFrozen { get; private set; }

        public bool IsCinematicModeActive { get; private set; }

        public bool IsStoryCinematicModeActive { get; set; }

        public GameObject PlayerObject { get; set; }

        public Animator Animator { get; set; }

        public PingInstance PingInstance { get; set; }

        public Vector3 Position { get; set; } = new Vector3();

        public Quaternion Rotation { get; set; } = new Quaternion();

        public TechType TechTypeInHand { get; set; }

        public bool IsCreatedModel { get; set; } = false;

        public bool IsDestroyed { get; set; } = false;

        public string NickName { get; set; }

        public ushort VehicleId { get; set; }

        public TechType VehicleType { get; set; }

        public Vector3 VehiclePosition { get; set; } = new Vector3();

        public Quaternion VehicleRotation { get; set; } = new Quaternion();

        public VehicleUpdateComponent VehicleComponent { get; set; }

        public NetworkPlayerItemComponent HandItemComponent { get; set; }

        public Quaternion RightHandItemRotation { get; set; }

        public Quaternion LeftHandItemRotation { get; set; }

        public float CameraPitch { get; set; }

        public Vector3 CameraForward { get; set; }

        public bool IsPrecursorArm { get; set; }

        public bool IsInWaterPark { get; set; }

        public bool IsVehicleDocking { get; set; }

        public string CurrentCinematicUniqueId { get; set; }

        public string UsingRoomId { get; set; }

        public bool IsMovementActive { get; set; } = true;

        public float FadeTime { get; private set; } = 1f;

        public float LastHypnotizeTime { get; private set; }

        private Coroutine FadeCoroutine { get; set; }

        public Dictionary<string, bool> Animations { get; private set; } = new Dictionary<string, bool>();

        public Queue<Dictionary<string, bool>> AnimationQueue { get; private set; } = new Queue<Dictionary<string, bool>>();

        public List<TechType> Equipments { get; set; } = new List<TechType>()
        {
            TechType.None,
            TechType.None,
            TechType.None,
            TechType.None,
            TechType.None,
        };
    }
}
