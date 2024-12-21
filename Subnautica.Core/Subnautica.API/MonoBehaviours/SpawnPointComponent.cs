namespace Subnautica.API.MonoBehaviours
{
    using Subnautica.API.Extensions;
    using Subnautica.Network.Models.WorldStreamer;
    using System.Collections;
    using UnityEngine;

    public class SpawnPointComponent : MonoBehaviour
    {
        public Coroutine Coroutine;

        public ZeroSpawnPoint SpawnPoint { get; set; }

        public bool IsAutoRespawnRunning { get; set; }

        public void SetSpawnPoint(ZeroSpawnPoint spawnPoint)
        {
            this.SpawnPoint = spawnPoint;
            this.SpawnToggle();
        }

        public void Start()
        {

        }

        public void OnEnable()
        {
            if (this.SpawnPoint != null)
            {
                this.DisableCoroutine();
                this.RegisterResourceTracker();
                this.DrillableHealthSync();
                this.HealthSync();

                if (!this.IsRespawnable())
                {
                    this.gameObject.SetActive(false);
                }
            }
        }

        public void OnDisable()
        {
            this.DisableCoroutine();
            this.UnRegisterResourceTracker();

            if (this.IsRespawnActive())
            {
                this.StartAutoRespawn();
            }
        }

        public void OnDestroy()
        {
            this.DisableCoroutine();
        }

        public void SpawnToggle()
        {
            if (!this.IsRespawnActive())
            {
                this.gameObject.SetActive(false);
            }
            else if (!this.IsRespawnable())
            {
                this.gameObject.SetActive(false);
                this.StartAutoRespawn();
            }
            else
            {
                this.gameObject.SetActive(true);
            }

            this.DrillableHealthSync();
            this.HealthSync();
        }

        public void StartAutoRespawn()
        {
            this.DisableCoroutine();

            if (this.IsRespawnActive())
            {
                this.IsAutoRespawnRunning = true;
                this.Coroutine = UWE.CoroutineHost.StartCoroutine(this.AutoRespawnAsync());
            }
        }

        public IEnumerator AutoRespawnAsync()
        {
            while (this != null && this.IsRespawnActive() && !this.IsRespawnable())
            {
                if (this.RespawnLeftTime() < 2f)
                {
                    yield return UWE.CoroutineUtils.waitForNextFrame;
                }
                else
                {
                    yield return new WaitForSecondsRealtime(1f);
                }
            }

            if (this != null)
            {
                this.IsAutoRespawnRunning = false;

                if (this.IsRespawnActive())
                {
                    this.gameObject?.SetActive(true);
                }
            }
        }

        public void DisableCoroutine(bool forceDisable = false)
        {
            if (this.IsAutoRespawnRunning && this.Coroutine != null)
            {
                this.IsAutoRespawnRunning = false;

                UWE.CoroutineHost.StopCoroutine(this.Coroutine);
            }
        }

        public bool IsRespawnActive()
        {
            if (this.gameObject == null || this.SpawnPoint == null || this.SpawnPoint.IsRespawnActive() == false)
            {
                return false;
            }

            var totalAmount = UWE.Utils.OverlapSphereIntoSharedBuffer(this.gameObject.transform.position, 1.5f);
            for (int index = 0; index < totalAmount; ++index)
            {
                var gameObject = UWE.Utils.sharedColliderBuffer[index].gameObject;
                if (gameObject == null)
                {
                    continue;
                }

                if (gameObject.GetComponentInParent<Base>())
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsRespawnable()
        {
            return this.SpawnPoint != null && this.SpawnPoint.IsRespawnable(DayNightCycle.main.timePassedAsFloat);
        }

        public float RespawnLeftTime()
        {
            return this.SpawnPoint.NextRespawnTime - DayNightCycle.main.timePassedAsFloat;
        }

        public void RegisterResourceTracker()
        {
            if (this.gameObject.TryGetComponent<ResourceTracker>(out var resourceTracker))
            {
                resourceTracker.Register();
            }
        }

        public void UnRegisterResourceTracker()
        {
            if (this.gameObject.TryGetComponent<ResourceTracker>(out var resourceTracker))
            {
                resourceTracker.Unregister();
            }
        }

        public void HealthSync()
        {
            if (this.gameObject.TryGetComponent<global::LiveMixin>(out var liveMixin))
            {
                liveMixin.ResetHealth();
            }
        }

        public void DrillableHealthSync(bool isSpawnFx = false)
        {
            if (this.gameObject.TryGetComponent<global::Drillable>(out var drillable))
            {
                drillable.SetHealth(this.SpawnPoint.Health, this.IsRespawnable(), isSpawnFx);
            }
        }
    }
}
