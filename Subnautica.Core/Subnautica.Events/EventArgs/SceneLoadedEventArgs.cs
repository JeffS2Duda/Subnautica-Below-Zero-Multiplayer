namespace Subnautica.Events.EventArgs
{
    using System;
    using UnityEngine.SceneManagement;

    public class SceneLoadedEventArgs : EventArgs
    {
        public SceneLoadedEventArgs(Scene scene)
        {
            Scene = scene;
        }

        public Scene Scene { get; set; }
    }
}
