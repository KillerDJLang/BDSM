using BepInEx;
using UnityEngine;
using DrakiaXYZ.VersionChecker;
using System;
using BDSM.Patches;
using BDSM.Helpers;

namespace BDSM
{
    [BepInPlugin("DJ.BDSM", "BDSM", "1.3.0")]
    public class Plugin : BaseUnityPlugin
    {
        public const int TarkovVersion = 33420;

        internal static TheMaid Script;
        internal static GameObject Hook;

        void Awake()
        {
            if (!VersionChecker.CheckEftVersion(Logger, Info, Config))
            {
                throw new Exception("Invalid EFT Version");
            }

            // Bind the configs
            DJConfig.BindConfig(Config);

            Hook = new GameObject("IR Object");
            Script = Hook.AddComponent<TheMaid>();
            DontDestroyOnLoad(Hook);
            new OnDeadPatch().Enable();
        }
    }
}
