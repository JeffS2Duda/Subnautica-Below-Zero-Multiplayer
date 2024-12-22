using System.Security.Cryptography;

namespace LobbyServer.Managers;

public class UserManager
{
    public static List<string> CurrentHosts = [];
    public static List<User> Users = [];
    public static List<Server> Servers = [];

    public struct User
    {
        // platform id
        public string UserId; 
        // peer id
        public string PeerId;
        // Ip of the user
        public string Ip;
        // Port to host the server on
        public int Port;
    }

    public struct Server
    {
        // server ip
        public string Ip;
        // server port
        public int Port;
        // random token
        public string AccessToken;
        // code to join, int
        public string Code;
    }

    public static string Login(string id, string ip, int port)
    {
        if (Users.Any(x=>x.UserId == id))
        {
            Users.Remove(Users.FirstOrDefault(x=>x.UserId == id));
        }
        var peerId = Guid.NewGuid().ToString().ToUpper().Replace("-","");
        Users.Add(new()
        { 
            Ip = ip,
            PeerId = peerId,
            Port = port,
            UserId = id
        });
        return peerId;
    }

    public static void Disconnect(string peerId)
    {
        var user = Users.FirstOrDefault(x => x.PeerId == peerId);
        if (!string.IsNullOrEmpty(user.PeerId))
            Users.Remove(user);
    }


    public static Server Host(string peerId)
    {
        var user = Users.FirstOrDefault(x => x.PeerId == peerId);
        if (Servers.Any(x => x.Ip == user.Ip && x.Port == user.Port))
        {
            // what to do now?
        }
        Server server = new()
        {
            Ip = user.Ip,
            Port = user.Port
        };
        server.AccessToken = RandomNumberGenerator.GetHexString(20);
        server.Code = RandomNumberGenerator.GetInt32(10000).ToString();
        CurrentHosts.Add(server.Ip);
        Servers.Add(server);
        return server;
    }

    public static Server GetServerToJoin(string accessCode)
    {
        return Servers.FirstOrDefault(x=>x.Code == accessCode);
    }
}
