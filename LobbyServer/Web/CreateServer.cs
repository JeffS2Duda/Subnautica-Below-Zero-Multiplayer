using LobbyServer.Formats;
using LobbyServer.Managers;
using ModdableWebServer;
using ModdableWebServer.Attributes;
using ModdableWebServer.Helper;
using NetCoreServer;
using Newtonsoft.Json;

namespace LobbyServer.Web;

internal class CreateServer
{
    [HTTP("GET", "/createserver?{args}")]
    public static bool OnCreate(HttpRequest request, ServerStruct serverStruct)
    {
        if (!serverStruct.Parameters.ContainsKey("peerId"))
        {
            Console.WriteLine("CreateServer with no peerId!");
            serverStruct.Response.MakeErrorResponse();
            serverStruct.SendResponse();
            return true;
        }
        var peerId = serverStruct.Parameters["peerId"];
        var server = UserManager.Host(peerId);
        Console.WriteLine(server.Ip + " " + server.Port + " " + server.AccessToken + " " + server.Code + " ");
        LobbyCreateServerResponse response = new LobbyCreateServerResponse()
        { 
            AccessToken = server.AccessToken,
            ServerIp = server.Ip,
            ServerPort = server.Port,
            JoinCode = server.Code,
            ErrorMessage = "",
            IsError = false
        };
        serverStruct.Response.MakeGetResponse(JsonConvert.SerializeObject(response));
        serverStruct.SendResponse();
        return true;
    }
}
