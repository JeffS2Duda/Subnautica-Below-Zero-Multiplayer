namespace Subnautica.Events.EventArgs
{
    using UnityEngine.SceneManagement;
    using System;

    public class SceneLoadedEventArgs : EventArgs
    {
        public SceneLoadedEventArgs(Scene scene)
        {
            Scene = scene;
        }

        public Scene Scene { get; set; }
    }
}
