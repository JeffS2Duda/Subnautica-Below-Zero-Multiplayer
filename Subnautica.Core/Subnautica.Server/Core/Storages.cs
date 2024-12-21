namespace Subnautica.Server.Core
{
    public class Storages
    {
        public Storage.Encyclopedia Encyclopedia { get; set; } = new Storage.Encyclopedia();

        public Storage.Construction Construction { get; set; } = new Storage.Construction();

        public Storage.Technology Technology { get; set; } = new Storage.Technology();

        public Storage.Player Player { get; set; } = new Storage.Player();

        public Storage.World World { get; set; } = new Storage.World();

        public Storage.Scanner Scanner { get; set; } = new Storage.Scanner();

        public Storage.PictureFrame PictureFrame { get; set; } = new Storage.PictureFrame();

        public Storage.Story Story { get; set; } = new Storage.Story();

        public void Start(string serverId)
        {
            this.Encyclopedia.Start(serverId);
            this.Construction.Start(serverId);
            this.PictureFrame.Start(serverId);
            this.Technology.Start(serverId);
            this.Scanner.Start(serverId);
            this.Player.Start(serverId);
            this.World.Start(serverId);
            this.Story.Start(serverId);
        }

        public void Dispose()
        {
            this.Encyclopedia = null;
            this.Construction = null;
            this.PictureFrame = null;
            this.Technology = null;
            this.Scanner = null;
            this.Player = null;
            this.World = null;
            this.Story = null;
        }
    }
}
