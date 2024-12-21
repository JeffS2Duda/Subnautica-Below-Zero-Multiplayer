namespace Subnautica.API.MonoBehaviours
{
    using UnityEngine;

    using Subnautica.API.Features;

    public class BaseGhostRotationComponent : MonoBehaviour
    {
        public int LastRotation { get; private set; } = 0;

        public void SetLastRotation(int lastRotation)
        {
            this.LastRotation = lastRotation;
        }

        public static void ClampRotation(BaseGhost baseGhost, int max)
        {
            if (BaseGhostRotationComponent.GetComponentLastRotation(baseGhost) == -1)
            {
                global::Builder.ClampRotation(max);
            }
        }

        public static bool UpdateRotation(BaseGhost baseGhost, int max)
        {
            if (BaseGhostRotationComponent.GetComponentLastRotation(baseGhost) == -1)
            {
                return global::Builder.UpdateRotation(max);
            }

            return true;
        }

        public static int GetLastRotation(BaseGhost baseGhost)
        {
            int lastRotation = BaseGhostRotationComponent.GetComponentLastRotation(baseGhost);
            return lastRotation == -1 ? global::Builder.lastRotation : lastRotation;
        }

        private static int GetComponentLastRotation(BaseGhost baseGhost)
        {
            if (!Network.IsMultiplayerActive)
            {
                return -1;
            }

            if (baseGhost.TryGetComponent<BaseGhostRotationComponent>(out var component))
            {
                return component.LastRotation;
            }

            return -1;
        }
    }
}