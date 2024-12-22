using LobbyServer.Web;

namespace LobbyServer;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Starting lobby Server!");
        try
        {
            ServerManager.Init(args);
            Console.ReadLine();
            ServerManager.Shutdown();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex}");
            Console.ReadLine();
        }

    }
}
