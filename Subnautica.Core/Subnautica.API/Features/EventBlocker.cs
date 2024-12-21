namespace Subnautica.API.Features
{
    using Subnautica.API.Enums;
    using System;
    using System.Collections.Generic;

    public class EventBlocker : IDisposable
    {
        private static List<ProcessType> ProcessList { get; set; } = new List<ProcessType>();

        private static List<TechType> TechList { get; set; } = new List<TechType>();

        private ProcessType ProcessType { get; set; } = ProcessType.None;

        private TechType TechType { get; set; } = TechType.None;

        public static EventBlocker Create(ProcessType type)
        {
            return new EventBlocker(type);
        }

        public static EventBlocker Create(TechType type)
        {
            return new EventBlocker(type);
        }

        public static bool IsEventBlocked(ProcessType type)
        {
            return ProcessList.Contains(type);
        }

        public static bool IsEventBlocked(TechType type)
        {
            return TechList.Contains(type);
        }

        public EventBlocker(ProcessType type)
        {
            this.ProcessType = type;
            ProcessList.Add(type);
        }

        public EventBlocker(TechType type)
        {
            this.TechType = type;
            TechList.Add(type);
        }

        public void Dispose()
        {
            if (this.ProcessType != ProcessType.None)
            {
                ProcessList.Remove(this.ProcessType);
            }

            if (this.TechType != TechType.None)
            {
                TechList.Remove(this.TechType);
            }
        }
    }
}
