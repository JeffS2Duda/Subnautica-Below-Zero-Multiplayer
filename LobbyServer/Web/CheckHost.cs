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
        foreach (var item in serverStruct.Parameters)
        {
            Console.WriteLine(item.Value + " " + item.Value);
        }
        /*
        var hostIp = serverStruct.Parameters["hostIp"];
        serverStruct.Response.MakeGetResponse(UserManager.CurrentHosts.Contains(hostIp).ToString());*/
        serverStruct.Response.MakeOkResponse();
        serverStruct.SendResponse();
        return true;
    }
}
