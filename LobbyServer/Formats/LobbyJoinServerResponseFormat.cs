namespace LobbyServer.Formats;

public class LobbyJoinServerResponseFormat
{
    public bool IsError { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
    public string ServerIp { get; set; } = string.Empty;
    public int ServerPort { get; set; }
}
