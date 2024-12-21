namespace Subnautica.Client.Synchronizations.Processors.Player
{
    using Subnautica.API.Features;
    using Subnautica.Client.Abstracts;
    using Subnautica.Client.Multiplayer.Cinematics;
    using Subnautica.Network.Models.Core;

    using UWE;

    using ClientModel = Subnautica.Network.Models.Client;

    public class JoiningProcessor : NormalProcessor
    {
        public override bool OnDataReceived(NetworkPacket networkPacket)
        {
            var packet = networkPacket.GetPacket<ClientModel.JoiningServerArgs>();
            if (string.IsNullOrEmpty(packet.ServerId))
            {
                return false;
            }

            ZeroPlayer.CurrentPlayer.UniqueId         = packet.PlayerUniqueId;
            ZeroPlayer.CurrentPlayer.NickName         = Tools.GetLoggedInName();
            ZeroPlayer.CurrentPlayer.PlayerId         = packet.PlayerId;
            ZeroPlayer.CurrentPlayer.CurrentServerId  = packet.ServerId;
            ZeroPlayer.CurrentPlayer.CurrentSubRootId = packet.PlayerSubRootId;

            Network.IsMultiplayerActive = true;
            Network.Session.SetSession(packet);

            PlayerCinematicQueue.Initialize();

            ZeroGame.RunInBackgroundChange(true);
            ErrorMessage.AddMessage(ZeroLanguage.Get("GAME_SERVER_PLAYER_CONNECTED"));

            CoroutineHost.StartCoroutine(uGUI_MainMenu.main.StartNewGame(packet.GameMode));
            return true;
        }
    }
}
