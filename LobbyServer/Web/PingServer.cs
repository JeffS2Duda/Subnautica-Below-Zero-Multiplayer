using LobbyServer.Formats;
using ModdableWebServer;
using ModdableWebServer.Attributes;
using ModdableWebServer.Helper;
using NetCoreServer;
using Newtonsoft.Json;

namespace LobbyServer.Web;

internal class PingServer
{
    [HTTP("GET", "/pingserver?{args}")]
    public static bool OnPing(HttpRequest request, ServerStruct serverStruct)
    {
        //var accessToken = serverStruct.Parameters["accessToken"];
        LobbyPingServerResponseFormat lobbyPingServerResponseFormat = new LobbyPingServerResponseFormat()
        {
            ErrorMessage = "",
            IsError = false
        };
        serverStruct.Response.MakeGetResponse(JsonConvert.SerializeObject(lobbyPingServerResponseFormat));
        serverStruct.SendResponse();
        return true;
    }
}
