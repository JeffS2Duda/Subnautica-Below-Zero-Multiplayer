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
        UserManager.Disconnect(request.Body);
        serverStruct.Response.MakeOkResponse();
        serverStruct.SendResponse();
        return true;
    }
}
