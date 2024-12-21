namespace Subnautica.API.Features
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    using Subnautica.API.Enums;

    public static class Log
    {
        public static List<string> Messages { get; set; } = new List<string>();

        private static System.Timers.Timer Timer { get; set; } = null;

        private static bool IsTimerInitialized { get; set;} = false;

        public static bool IsWritingToDisk { get; set; } = false;

        public static void Info(object message)
        {
            Log.Send($"[{Assembly.GetCallingAssembly().GetName().Name}] {message}", LogLevel.Info);
        }

        public static void Warn(object message)
        {
            Log.Send($"[{Assembly.GetCallingAssembly().GetName().Name}] {message}", LogLevel.Warn);
        }

        public static void Error(object message)
        {
            Log.Send($"[{Assembly.GetCallingAssembly().GetName().Name}] {message}", LogLevel.Error);
        }

        public static void Info(string message)
        {
            Log.Send($"[{Assembly.GetCallingAssembly().GetName().Name}] {message}", LogLevel.Info);
        }
        
        public static void Warn(string message)
        {
            Log.Send($"[{Assembly.GetCallingAssembly().GetName().Name}] {message}", LogLevel.Warn);
        }

        public static void Error(string message)
        {
            Log.Send($"[{Assembly.GetCallingAssembly().GetName().Name}] {message}", LogLevel.Error);
        }

        public static void Send(string message, LogLevel level)
        {
            Log.SendRaw($"[{level.ToString().ToUpper()}] {message}");
        }

        public static void SendRaw(string message)
        {
            lock (Log.Messages)
            {
                Log.Messages.Add(String.Format("[{0}] {1}\n", DateTime.Now.ToString("HH:mm:ss.fff"), message));
            }

            if (!Log.IsTimerInitialized)
            {
                Log.IsTimerInitialized = true;

                Log.Timer = new System.Timers.Timer();
                Log.Timer.Interval = 200;
                Log.Timer.Elapsed += Log.OnTimerElapsed;
                Log.Timer.Start();
            }
        }

        private static void OnTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!Log.IsWritingToDisk && Log.Messages.Count > 0)
            {
                Log.IsWritingToDisk = true;

                lock (Log.Messages)
                {
                    try
                    {
                        File.AppendAllText(Log.GetErrorFilePath(), string.Join("", Log.Messages));
                    }
                    catch (Exception ex)
                    {
                        Log.Messages.Add($"Exception Log: {ex}");
                    }
                    finally
                    {
                        Log.Messages.Clear();
                    }
                }

                Log.IsWritingToDisk = false;
            }
        }

        public static string GetErrorFilePath()
        {
            if (Settings.IsAppLog)
            {
                return String.Format("{0}{1}.log", Paths.GetLauncherLogPath(), DateTime.Now.ToString("yyyy-MM-dd"));
            }

            return String.Format("{0}{1}.log", Paths.GetGameLogsPath(), DateTime.Now.ToString("yyyy-MM-dd"));
        }
    }
}
