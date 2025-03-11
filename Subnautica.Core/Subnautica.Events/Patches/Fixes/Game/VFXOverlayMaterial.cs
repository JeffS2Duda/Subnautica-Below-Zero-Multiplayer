namespace Subnautica.Events.Patches.Fixes.Game;

using HarmonyLib;
using System.Linq;

[HarmonyPatch(typeof(WBOIT), "RebuildCommandBuffer")]
public static class VFXOverlayMaterial
{
    private static void Postfix()
    {
        if (WBOIT.overlays.Any<global::VFXOverlayMaterial>())
        {
            foreach (global::VFXOverlayMaterial vfxoverlayMaterial in WBOIT.overlays.ToList<global::VFXOverlayMaterial>())
            {
                if (vfxoverlayMaterial == null)
                {
                    WBOIT.overlays.Remove(vfxoverlayMaterial);
                }
            }
        }
    }
}