namespace Subnautica.API.Features.Helper
{
    public class LobbyCreateServerResponse
    {
        public bool IsError { get; set; }

        public string ErrorMessage { get; set; }

        public string JoinCode { get; set; }

        public string AccessToken { get; set; }

        public string ServerIp { get; set; }

        public int ServerPort { get; set; }
    }
}
