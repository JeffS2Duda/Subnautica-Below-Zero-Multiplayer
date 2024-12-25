using ModdableWebServer.Attributes;
using ModdableWebServer;
using ModdableWebServer.Servers;
using NetCoreServer;
using System.Reflection;
using ModdableWebServer.Helper;

namespace LobbyServer.Web;

internal class ServerManager
{
    static HTTP_Server? Server;

    public static void Init(string[] args)
    {
        string ip = "0.0.0.0";
        int port = 80;
        if (args.Length > 0)
            ip = args[0];
        if (args.Length > 1)
            port = int.Parse(args[1]);
        if (args.Length > 2 && args[2].Contains("debug"))
        {
            DebugPrinter.PrintToConsole = true;
            DebugPrinter.EnableLogs = true;
        }

        Server = new(ip, port);
        Server.OverrideAttributes(Assembly.GetExecutingAssembly());
        Server.ReceivedRequestError += ReqError;
        Server.Start();
        Console.WriteLine(Server.Address + " " + Server.Port);
    }

    private static void ReqError(object? sender, (HttpRequest request, string error) e)
    {
        Console.WriteLine($"Error: {e.request.Url} {e.error}");
    }

    public static void Shutdown()
    {
        Server?.Stop();
    }

    [HTTP("GET", "/")]
    public static bool Nothing(HttpRequest request, ServerStruct serverStruct)
    {
        serverStruct.Response.MakeOkResponse();
        serverStruct.SendResponse();
        return true;
    }
}
