namespace Subnautica.Client.MonoBehaviours.Player
{
    using mset;

    using Subnautica.API.Extensions;
    using Subnautica.API.Features;

    using UnityEngine;

    public class PlayerLighting : MonoBehaviour
    {
        public ZeroPlayer Player { get; set; }

        private string CurrentSubRootId { get; set; }

        private Sky LastSky { get; set; }

        private float LastPower { get; set; } = 0f;

        private MaterialPropertyBlock Block = new MaterialPropertyBlock();

        public void Start()
        {
            var skyApplier = this.Player.AddComponent<SkyApplier>();
            skyApplier.anchorSky = Skies.Auto;
            skyApplier.emissiveFromPower = false;
            skyApplier.dynamic = true;
            skyApplier.renderers = this.Player.GetComponentsInChildren<SkinnedMeshRenderer>(true);
        }

        public void FixedUpdate()
        {
            if (this.IsChangedSubRoot())
            {
                this.ApplySkybox();
            }

            this.UpdateBaseLighting();
        }

        public bool IsChangedSubRoot()
        {
            if (this.Player == null || !World.IsLoaded)
            {
                return false;
            }

            if (this.CurrentSubRootId == this.Player.CurrentSubRootId)
            {
                return false;
            }

            this.CurrentSubRootId = this.Player.CurrentSubRootId;
            return true;
        }

        public bool ApplySkybox()
        {
            if (string.IsNullOrEmpty(this.CurrentSubRootId))
            {
                this.LastPower = -99f;
                SkyEnvironmentChanged.Broadcast(this.Player.PlayerModel, null);
                return false;
            }

            SubRoot subRoot = Network.Identifier.GetComponentByGameObject<SubRoot>(this.CurrentSubRootId, true);
            if (subRoot == null)
            {
                return false;
            }

            this.LastPower = -99f;
            SkyEnvironmentChanged.Broadcast(this.Player.PlayerModel, subRoot);
            return true;
        }

        public void UpdateBaseLighting()
        {
            float power = 0.0f;
            Sky customSky = null;

            if (!string.IsNullOrEmpty(this.CurrentSubRootId))
            {
                var gameObject = Network.Identifier.GetGameObject(this.CurrentSubRootId, true);
                if (gameObject != null)
                {
                    var subBase = gameObject.GetComponent<Base>();
                    if (subBase != null)
                    {
                        var cellLightingFor = subBase.GetCellLightingFor(this.transform.position);
                        if (cellLightingFor)
                        {
                            power = cellLightingFor.GetPowerLossValue();
                            customSky = cellLightingFor.interiorSky;
                        }
                    }
                }
            }

            if (customSky == null)
            {
                customSky = WaterBiomeManager.main.GetBiomeEnvironment(this.transform.position);
            }

            bool isSkyChanged = this.LastSky != customSky;
            bool isPowChanged = this.LastPower != power;
            if (!isSkyChanged && !isPowChanged)
            {
                return;
            }

            this.LastPower = power;
            this.LastSky = customSky;

            foreach (SkyApplier skyApplier in this.GetComponentsInChildren<SkyApplier>(true))
            {
                if (isSkyChanged)
                {
                    skyApplier.SetCustomSky(customSky);
                }

                foreach (Renderer renderer in skyApplier.renderers)
                {
                    if (renderer != null)
                    {
                        this.Block.Clear();
                        renderer.GetPropertyBlock(this.Block);
                        this.Block.SetFloat(ShaderPropertyID._UwePowerLoss, power);
                        customSky.ApplyToBlock(ref this.Block, 0);
                        renderer.SetPropertyBlock(this.Block);
                    }
                }
            }
        }
    }
}
