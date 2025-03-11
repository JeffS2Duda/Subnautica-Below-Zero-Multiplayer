using Subnautica.API.Features.Helper;
using System.Collections.Generic;
using System.Linq;

namespace Subnautica.API.Features.NetworkUtility;

public class DataStorage
{
    public void SetProperty(string mainId, string key, object value)
    {
        List<GenericProperty> list;
        bool flag = this.Properties.TryGetValue(mainId, out list);
        if (flag)
        {
            GenericProperty genericProperty = list.FirstOrDefault((GenericProperty q) => q.Key == key);
            bool flag2 = genericProperty == null;
            if (flag2)
            {
                list.Add(new GenericProperty(key, value));
            }
            else
            {
                genericProperty.SetValue(value);
            }
        }
        else
        {
            this.Properties[mainId] = new List<GenericProperty>
                {
                    new GenericProperty(key, value)
                };
        }
    }

    public void RemoveProperty(string mainId)
    {
        this.Properties.Remove(mainId);
    }

    public T GetProperty<T>(string mainId, string key)
    {
        List<GenericProperty> list;
        bool flag = this.Properties.TryGetValue(mainId, out list);
        T t;
        if (flag)
        {
            GenericProperty genericProperty = list.FirstOrDefault((GenericProperty q) => q.Key == key);
            bool flag2 = genericProperty == null || genericProperty.Value == null;
            if (flag2)
            {
                t = default(T);
            }
            else
            {
                t = (T)((object)genericProperty.Value);
            }
        }
        else
        {
            t = default(T);
        }
        return t;
    }

    public void Dispose()
    {
        this.Properties.Clear();
    }

    private Dictionary<string, List<GenericProperty>> Properties = new Dictionary<string, List<GenericProperty>>();
}