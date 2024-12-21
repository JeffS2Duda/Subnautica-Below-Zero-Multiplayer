namespace Subnautica.Events.Patches.Fixes.Items
{
    public class ItemMain
    {
        public static bool CheckOnHolster(global::PlayerTool __instance)
        {
            if (__instance.GetComponentInParent<global::PingInstance>())
            {
                return false;
            }

            return true;
        }
    }
}
