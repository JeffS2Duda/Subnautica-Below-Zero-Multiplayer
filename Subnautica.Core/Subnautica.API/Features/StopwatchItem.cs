namespace Subnautica.API.Features
{
    using System.Diagnostics;

    public class StopwatchItem : Stopwatch
    {
        public float DelayTime { get; set; }

        public object CustomData { get; set; }

        public StopwatchItem(float delayTime = -1f, object customData = null, bool autoStart = true)
        {
            this.DelayTime = delayTime;
            this.CustomData = customData;

            if (autoStart)
            {
                this.Start();
            }
        }

        public bool IsFinished()
        {
            return this.ElapsedMilliseconds >= this.DelayTime;
        }

        public float ElapsedTime()
        {
            return this.ElapsedMilliseconds;
        }

        public T GetCustomData<T>()
        {
            if (this.CustomData == null)
            {
                return default;
            }

            return (T)this.CustomData;
        }
    }
}
