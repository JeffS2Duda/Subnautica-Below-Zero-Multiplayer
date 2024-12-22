namespace LobbyServer.Formats;

internal class LobbyCreateServerResponse
{
    public bool IsError { get; set; }

    public string ErrorMessage { get; set; } = string.Empty;

    public string JoinCode { get; set; } = string.Empty;

    public string AccessToken { get; set; } = string.Empty;

    public string ServerIp { get; set; } = string.Empty;

    public int ServerPort { get; set; }
}
