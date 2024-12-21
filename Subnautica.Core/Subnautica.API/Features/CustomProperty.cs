namespace Subnautica.API.Features
{
    using MessagePack;
    using System;
    using System.Globalization;

    [MessagePackObject]
    public class CustomProperty
    {
        [Key(0)]
        public byte Key { get; set; }

        [Key(1)]
        public string Value { get; set; }

        public CustomProperty(byte key, string value)
        {
            this.Key = key;
            this.Value = value;
        }

        public T GetKey<T>()
        {
            var type = typeof(T);
            if (type.IsEnum)
            {
                return (T)Enum.ToObject(type, this.Key);
            }

            return (T)Convert.ChangeType(this.Key, type);
        }

        public T GetValue<T>()
        {
            var type = typeof(T);
            if (type.IsEnum)
            {
                return (T)Enum.Parse(type, this.Value);
            }

            return (T)Convert.ChangeType(this.Value, type, CultureInfo.InvariantCulture);
        }
    }
}