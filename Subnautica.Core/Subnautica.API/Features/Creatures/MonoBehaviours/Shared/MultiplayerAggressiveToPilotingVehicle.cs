namespace Subnautica.API.Features.Creatures.MonoBehaviours.Shared
{
    public class MultiplayerAggressiveToPilotingVehicle : BaseMultiplayerCreature
    {
        private global::AggressiveToPilotingVehicle AggressiveToPilotingVehicle { get; set; }

        public void Awake()
        {
            this.AggressiveToPilotingVehicle = this.GetComponent<global::AggressiveToPilotingVehicle>();
        }

        public void OnEnable()
        {
            this.StopUpdateAggression();

            if (this.MultiplayerCreature.CreatureItem.IsMine())
            {
                this.StartUpdateAggression();
            }
        }

        public void OnChangedOwnership()
        {
            this.StopUpdateAggression();

            if (this.MultiplayerCreature.CreatureItem.IsMine())
            {
                this.StartUpdateAggression();
            }
        }

        public void OnDisable()
        {
            this.StopUpdateAggression();
        }

        private void StopUpdateAggression()
        {
            this.CancelInvoke("MultiplayerUpdateAggression");
        }

        private void StartUpdateAggression()
        {
            this.InvokeRepeating("MultiplayerUpdateAggression", global::UnityEngine.Random.value * this.AggressiveToPilotingVehicle.updateAggressionInterval, this.AggressiveToPilotingVehicle.updateAggressionInterval);
        }

        private void MultiplayerUpdateAggression()
        {
            var playerInRange = ZeroPlayer.GetPlayersByInRange(this.transform.position, this.AggressiveToPilotingVehicle.range * this.AggressiveToPilotingVehicle.range, true);
            if (playerInRange.IsExistsPlayer())
            {
                this.AggressiveToPilotingVehicle.lastTarget.SetTarget(playerInRange.NearestPlayer.GetVehicle(), this.AggressiveToPilotingVehicle.targetPriority);
                this.AggressiveToPilotingVehicle.creature.Aggression.Add(this.AggressiveToPilotingVehicle.aggressionPerSecond * this.AggressiveToPilotingVehicle.updateAggressionInterval);
            }
        }
    }
}
