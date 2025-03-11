using UnityEngine;

namespace Subnautica.API.MonoBehaviours;

public class ZeroVirtualIdentifier : MonoBehaviour
{
    private string UniqueId { get; set; }

    public void SetUniqueId(string uniqueId)
    {
        this.UniqueId = uniqueId;
    }

    public string GetUniqueId()
    {
        return this.UniqueId;
    }
}