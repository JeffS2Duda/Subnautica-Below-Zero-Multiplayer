namespace Subnautica.API.Features.Creatures.MonoBehaviours
{
    using UnityEngine;

    public class BaseMultiplayerCreature : MonoBehaviour
    {
        public MultiplayerCreature MultiplayerCreature { get; private set; }

        public void SetMultiplayerCreature(MultiplayerCreature multiplayerCreature)
        {
            this.MultiplayerCreature = multiplayerCreature;
        }
    }
}
