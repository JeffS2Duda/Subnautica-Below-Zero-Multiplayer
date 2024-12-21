namespace Subnautica.API.MonoBehaviours
{
    using UnityEngine;

    public class PlayerObstacle : MonoBehaviour, IObstacle
    {
        public bool CanDeconstruct(out string reason)
        {
            reason = global::Language.main.Get("PlayerObstacle");
            return false;
        }

        public bool IsDeconstructionObstacle()
        {
            return true;
        }
    }
}
