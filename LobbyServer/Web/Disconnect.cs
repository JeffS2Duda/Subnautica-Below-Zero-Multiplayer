using LobbyServer.Managers;
using ModdableWebServer;
using ModdableWebServer.Attributes;
using ModdableWebServer.Helper;
using NetCoreServer;

namespace LobbyServer.Web;

internal class Disconnect
{
    [HTTP("POST", "/disconnect")]
    public static bool OnDisConnect(HttpRequest request, ServerStruct serverStruct)
    {
        if (string.IsNullOrEmpty(request.Body))
        {
            Console.WriteLine("Disconnect doesnt have peerId!");
            serverStruct.Response.MakeErrorResponse();
            serverStruct.SendResponse();
            return true;
        }
        UserManager.Disconnect(request.Body);
        serverStruct.Response.MakeOkResponse();
        serverStruct.SendResponse();
        return true;
    }
}
