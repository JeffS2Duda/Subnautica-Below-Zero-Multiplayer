### Subnautica Below Zero Multiplayer Mod

Created for people who want to use in LAN.\
Removed unwanted software (netbird)\
Added option to host custom Lobby.

## How to install
Go to installed SubnauticaZero folder.
Create Folders:
- Multiplayer/Game/Core
- Multiplayer/Game/Dependencies
- Multiplayer/Game/Logs
- Multiplayer/Game/Plugins
- Multiplayer/Game/Saves

Put Subnautica.Loader.dll to Multiplayer/Game\
Put Data/SpawnPoints.bin to Multiplayer/Game/Core\
Replace Data/Assembly-CSharp.dll in SubnauticaZero_Data/Managed\
Put Dependencies to Multiplayer/Game/Dependencies\

Dependencies are everything that building Subnautica.Core results.
List if you unsure:
- 0Harmony.dll
- LiteNetLib.dll
- MessagePack.Annotations.dll
- MessagePack.dll
- Microsoft.Bcl.AsyncInterfaces.dll
- Microsoft.NET.StringTools.dll
- Newtonsoft.Json.dll
- Subnautica.Core.dll
- System.Buffers.dll
- System.Collections.Immutable.dll
- System.Memory.dll
- System.Numerics.Vectors.dll
- System.Runtime.CompilerServices.Unsafe.dll
- System.Threading.Tasks.Extensions.dll

## Config
- Run the game first time. Then close it!
- Go to "Multiplayer\Game\Core"
- Open Config.json
- Edit the LobbyURL to your or anyones Lobby Server ip and port. [If you running locally you can use 172.0.0.1]
- Edit the MyIp to your ip. (Make sure users can join to you, LAN mode use lan ip, otherwise use Public IP)
- Edit the HostOnPort to a desired FREE port to make the game can host on it.
- Save it.

## Lobby
You need .NET 8 to build/run it.
You can run it with desired IP and Port to host on it.\
Added because users does not want to host the lobby on port 80.\
`LobbyServer IP Port`\
Example: `LobbyServer.exe 192.168.3.50 8888`

## Thanks
Thanks BOT Benson for creating this.\
dnSpyEx for able to read C# dlls to able to edit it.