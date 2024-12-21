namespace Subnautica.API.Features
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;

    using Subnautica.API.Enums;

    using UWE;

    public class WaitingScreen
    {
        private static HashSet<ProcessType> List { get; set; } = new HashSet<ProcessType>();

        public static IEnumerator AddWaitScreen(ProcessType type, Action startingCallback, Action failureCallback)
        {
            AddWaitItem(type);

            startingCallback.Invoke();

            float sleepTime = Settings.ModConfig.ConnectionTimeout.GetInt() * 1000f;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            while (IsLoading(type, stopwatch.ElapsedMilliseconds, sleepTime))
            {
                yield return CoroutineUtils.waitForNextFrame;
            }

            if (IsLoading(type))
            {
                failureCallback.Invoke();
            }
        }

        public static void RemoveScreen(ProcessType type)
        {
            List.Remove(type);
        }

        private static void AddWaitItem(ProcessType type)
        {
            List.Add(type);
        }

        private static bool IsLoading(ProcessType type, float currentTime = 0f, float sleepTime = 0f)
        {
            if (sleepTime <= 0)
            {
                return List.Contains(type);
            }

            if (!List.Contains(type))
            {
                return false;
            }

            return currentTime < sleepTime;
        }

        public static void Dispose()
        {
            List.Clear();
        }
    }
}