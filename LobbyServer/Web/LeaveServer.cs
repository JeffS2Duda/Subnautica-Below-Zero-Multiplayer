using LobbyServer.Formats;
using ModdableWebServer;
using ModdableWebServer.Attributes;
using ModdableWebServer.Helper;
using NetCoreServer;
using Newtonsoft.Json;

namespace LobbyServer.Web;

internal class LeaveServer
{
    [HTTP("GET", "/leaveserver?{args}")]
    public static bool OnLeave(HttpRequest request, ServerStruct serverStruct)
    {
        //var peerIp = serverStruct.Parameters["peerIp"];
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
