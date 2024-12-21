namespace Subnautica.Client.MonoBehaviours.World
{
    using Subnautica.API.Features;
    using UnityEngine;

    public class MultiplayerPipeSurfaceFloater : MonoBehaviour
    {
        private bool IsFinished { get; set; } = false;

        private StopwatchItem Timing { get; set; } = new StopwatchItem(2000f);

        public void FixedUpdate()
        {
            if (!this.IsFinished && this.Timing.IsFinished())
            {
                this.UpdateOxygenPipes();
            }
        }

        private void UpdateOxygenPipes()
        {
            this.IsFinished = true;

            if (this.TryGetComponent<PipeSurfaceFloater>(out var floater))
            {
                foreach (var uniqueId in floater.children)
                {
                    var oxygenPipe = Network.Identifier.GetComponentByGameObject<OxygenPipe>(uniqueId);
                    if (oxygenPipe)
                    {
                        oxygenPipe.parentPosition = this.transform.position;
                        oxygenPipe.UpdatePipe();
                    }
                }
            }
        }

        public void OnEnable()
        {
            this.IsFinished = false;

            this.Timing.Restart();
        }
    }
}
