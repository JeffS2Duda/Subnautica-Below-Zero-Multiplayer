using LobbyServer.Managers;
using ModdableWebServer;
using ModdableWebServer.Attributes;
using ModdableWebServer.Helper;
using NetCoreServer;

namespace LobbyServer.Web;

internal class Connect
{
    [HTTP("POST", "/connect")]
    public static bool OnConnect(HttpRequest request, ServerStruct serverStruct)
    {
        if (!request.Body.Contains(" | "))
        {
            Console.WriteLine("Request Body with no splitter!");
            serverStruct.Response.MakeErrorResponse();
            serverStruct.SendResponse();
            return true;
        }
        var splitted = request.Body.Split(" | ");
        var id = splitted[0];
        var ip = splitted[1];
        var hostonport = splitted[2];
        var peerId = UserManager.Login(id, ip , int.Parse(hostonport));
        Console.WriteLine($"{id} {ip} {hostonport} => {peerId}");
        serverStruct.Response.MakeGetResponse(peerId);
        serverStruct.SendResponse();
        return true;
    }
}
