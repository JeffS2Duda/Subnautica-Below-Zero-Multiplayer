namespace Subnautica.API.Extensions
{
    using System;

    using Subnautica.API.Features;

    public static class EventExtensions
    {
        public static void CustomInvoke<T>(this SubnauticaPluginEventHandler<T> ev, T arg)
        {
            if (ev != null)
            {
                foreach (SubnauticaPluginEventHandler<T> handler in ev.GetInvocationList())
                {
                    try
                    {
                        handler.Invoke(arg);
                    }
                    catch (Exception e)
                    {
                        Log.Error(string.Format("Method Name: {0}, Class Name: {1}, Event Name: {2}\n{3}", handler.Method.Name, handler.Method.ReflectedType?.FullName, ev.GetType().FullName, e));
                    }
                }
            }
        }

        public static void CustomInvoke(this SubnauticaPluginEventHandler ev)
        {
            if (ev != null)
            {
                foreach (SubnauticaPluginEventHandler handler in ev.GetInvocationList())
                {
                    try
                    {
                        handler.Invoke();
                    }
                    catch (Exception e)
                    {
                        Log.Error(string.Format("Method Name: {0}, Class Name: {1}, Event Name: {2}\n{3}", handler.Method.Name, handler.Method.ReflectedType?.FullName, ev.GetType().FullName, e));
                    }
                }
            }
        }

        public delegate void SubnauticaPluginEventHandler<TEventArgs>(TEventArgs ev);

        public delegate void SubnauticaPluginEventHandler();
    }
}