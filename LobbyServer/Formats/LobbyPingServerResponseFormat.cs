namespace LobbyServer.Formats;

internal class LobbyPingServerResponseFormat
{
    public bool IsError { get; set; }

    public string ErrorMessage { get; set; } = string.Empty;
}
