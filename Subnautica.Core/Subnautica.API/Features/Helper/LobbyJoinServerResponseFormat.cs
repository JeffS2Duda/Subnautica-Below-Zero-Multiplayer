namespace Subnautica.API.Features.Helper
{
    public class LobbyJoinServerResponseFormat
    {
        public bool IsError { get; set; }

        public string ErrorMessage { get; set; }

        public string ServerIp { get; set; }

        public int ServerPort { get; set; }
    }
}
