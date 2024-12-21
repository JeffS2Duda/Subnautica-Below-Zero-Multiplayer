namespace Subnautica.Client.Abstracts
{
    public class BaseProcessor
    {
        public virtual void OnStart()
        {

        }

        public virtual void OnUpdate()
        {

        }

        public virtual void OnLateUpdate()
        {

        }

        public virtual void OnFixedUpdate()
        {

        }

        public virtual void OnDispose()
        {

        }

        public void OnFinishedSuccessCallback()
        {
            this.SetFinished(true);
        }

        public void SetFinished(bool isFinished)
        {
            this.isFinished = isFinished;
        }

        public bool IsFinished()
        {
            return this.isFinished;
        }

        public void SetWaitingForNextFrame(bool isWaitingForNextFrame)
        {
            this.isWaitingForNextFrame = isWaitingForNextFrame;
        }

        public bool IsWaitingForNextFrame()
        {
            return this.isWaitingForNextFrame;
        }

        private bool isFinished { get; set; } = false;

        private bool isWaitingForNextFrame { get; set; } = false;
    }
}
