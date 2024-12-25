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
        if (!serverStruct.Parameters.ContainsKey("hostIp"))
        {
            Console.WriteLine("CheckHost requested with no hostIp!");
            serverStruct.Response.MakeErrorResponse();
            serverStruct.SendResponse();
            return true;
        }
        Console.WriteLine("CheckHost requested!");
        var hostIp = serverStruct.Parameters["hostIp"];
        serverStruct.Response.MakeGetResponse(UserManager.CurrentHosts.Contains(hostIp).ToString());
        serverStruct.SendResponse();
        return true;
    }
}
