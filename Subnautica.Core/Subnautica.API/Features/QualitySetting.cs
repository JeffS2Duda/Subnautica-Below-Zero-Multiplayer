namespace Subnautica.API.Features
{
    using UnityEngine;

    public class QualitySetting
    {
        private static int OldFrameRate  = 0;

        private static bool OldVsync = false;

        public static void EnableFastMode()
        {
            if (OldFrameRate != 501)
            {
                OldFrameRate = GraphicsUtil.GetFrameRate();
                OldVsync     = GraphicsUtil.GetVSyncEnabled();
            }

            Application.targetFrameRate = 501;
            UnityEngine.QualitySettings.vSyncCount = 0;
        }

        public static void DisableFastMode()
        {
            Reset();
        }

        public static void Reset()
        {
            if (OldFrameRate != 0)
            {
                Application.targetFrameRate = Mathf.Min(OldFrameRate, 500);
                UnityEngine.QualitySettings.vSyncCount = OldVsync ? 1 : 0;

                OldFrameRate = 0;
                OldVsync     = false;
            }
        }
    }
}
