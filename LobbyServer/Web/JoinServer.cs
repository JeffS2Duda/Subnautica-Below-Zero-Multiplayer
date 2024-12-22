using LobbyServer.Formats;
using LobbyServer.Managers;
using ModdableWebServer;
using ModdableWebServer.Attributes;
using ModdableWebServer.Helper;
using NetCoreServer;
using Newtonsoft.Json;

namespace LobbyServer.Web;

internal class JoinServer
{
    [HTTP("GET", "/joinserver?{args}")]
    public static bool OnJoin(HttpRequest request, ServerStruct serverStruct)
    {
        var joinCode = serverStruct.Parameters["joincode"];
        //var peerId = serverStruct.Parameters["peerid"];

        var server = UserManager.GetServerToJoin(joinCode);

        LobbyJoinServerResponseFormat joinServerResponseFormat = new()
        { 
            ErrorMessage = "",
            IsError = false,
            ServerIp = server.Ip,
            ServerPort = server.Port,

        }; 
        serverStruct.Response.MakeGetResponse(JsonConvert.SerializeObject(joinServerResponseFormat));
        serverStruct.SendResponse();
        return true;
    }
}
