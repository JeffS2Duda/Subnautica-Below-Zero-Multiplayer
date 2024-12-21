namespace Subnautica.API.Features.Helper
{
    public class GenericProperty
    {
        public string Key { get; set; }

        public object Value { get; set; }

        public GenericProperty(string key, object value)
        {
            this.Key = key;
            this.Value = value;
        }

        public void SetValue(object value)
        {
            this.Value = value;
        }

        public string GetKey()
        {
            return this.Key;
        }

        public T GetValue<T>()
        {
            if (this.Value == null)
            {
                return default(T);
            }

            return (T)this.Value;
        }
    }
}
