namespace Subnautica.Client.Modules
{
    using Subnautica.Client.Multiplayer.World;
    using Subnautica.Events.EventArgs;

    public class MultiplayerElevator
    {
        public static void OnElevatorInitialized(ElevatorInitializedEventArgs ev)
        {
            ev.Instance.gameObject.EnsureComponent<MultiplayerMovingPlatform>().SetPlatform(ev.Instance.elevatorTrans);
        }
    }
}
