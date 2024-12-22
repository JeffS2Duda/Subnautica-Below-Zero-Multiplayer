using LobbyServer.Managers;
using ModdableWebServer;
using ModdableWebServer.Attributes;
using ModdableWebServer.Helper;
using NetCoreServer;

namespace LobbyServer.Web;

internal class CheckHost
{
    [HTTP("GET", "/checkhost?{args}")]
    public static bool OnCheck(HttpRequest request, ServerStruct serverStruct)
    {
        var hostIp = serverStruct.Parameters["hostIp"];
        serverStruct.Response.MakeGetResponse(UserManager.CurrentHosts.Contains(hostIp).ToString());
        serverStruct.SendResponse();
        return true;
    }
}
