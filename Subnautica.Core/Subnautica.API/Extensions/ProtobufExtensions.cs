namespace Subnautica.Client.Extensions
{
    using System;

    using Subnautica.API.Features;

    public static class ProtobufExtensions
    {
        private static Type CellMode = typeof(ProtobufClass_CellMode);

        private static Type ConstructionMode = typeof(ProtobufClass_ConstructionMode);

        private static Type EmptyIdMode = typeof(ProtobufClass_EmptyId);

        public static void SetCellModeActive(this global::ProtobufSerializer serializer, bool isActive)
        {
            if (isActive)
            {
                if (!serializer.IsCellModeActive())
                {
                    serializer.canSerializeCache.Add(CellMode, true);
                }
            }
            else
            {
                serializer.RemoveCellMode();
            }
        }

        public static void RemoveCellMode(this global::ProtobufSerializer serializer)
        {
            serializer.canSerializeCache.Remove(CellMode);
        }

        public static bool IsCellModeActive(this global::ProtobufSerializer serializer)
        {
            return serializer.canSerializeCache.ContainsKey(CellMode);
        }

        public static void SetConstructionModeActive(this global::ProtobufSerializer serializer, bool isActive)
        {
            if (isActive)
            {
                if (!serializer.IsConstructionModeActive())
                {
                    serializer.canSerializeCache.Add(ConstructionMode, true);
                }
            }
            else
            {
                serializer.RemoveConstructionMode();
            }
        }

        public static void RemoveConstructionMode(this global::ProtobufSerializer serializer)
        {
            serializer.canSerializeCache.Remove(ConstructionMode);
        }

        public static bool IsConstructionModeActive(this global::ProtobufSerializer serializer)
        {
            return serializer.canSerializeCache.ContainsKey(ConstructionMode);
        }

        public static void SetIdIgnoreModeActive(this global::ProtobufSerializer serializer, bool isActive)
        {
            if (isActive)
            {
                if (!serializer.IsIdIgnoreModeActive())
                {
                    serializer.canSerializeCache.Add(EmptyIdMode, true);
                }
            }
            else
            {
                serializer.RemoveIdIgnoreModeActive();
            }
        }

        public static void RemoveIdIgnoreModeActive(this global::ProtobufSerializer serializer)
        {
            serializer.canSerializeCache.Remove(EmptyIdMode);
        }

        public static bool IsIdIgnoreModeActive(this global::ProtobufSerializer serializer)
        {
            return serializer.canSerializeCache.ContainsKey(EmptyIdMode);
        }
    }

    public class ProtobufClass_CellMode
    {

    }

    public class ProtobufClass_ConstructionMode
    {

    }

    public class ProtobufClass_EmptyId
    {

    }
}
