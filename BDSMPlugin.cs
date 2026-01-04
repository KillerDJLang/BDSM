using System;
using BDSM.Helpers;
using BDSM.Patches;
using BepInEx;
using Comfort.Common;
using DrakiaXYZ.VersionChecker;
using EFT;
using UnityEngine;

namespace BDSM
{
    [BepInPlugin("nameless.bodydisposal", "Body Disposal Service Maid", "1.5.0")]
    public class Plugin : BaseUnityPlugin
    {
        public const int TarkovVersion = 40087;

        internal static TheMaid Script;
        private static GameObject _hook;

        internal static Player MyPlayer
        {
            get { return MyGameworld.MainPlayer; }
        }

        internal static GameWorld MyGameworld
        {
            get { return Singleton<GameWorld>.Instance; }
        }

        private void Awake()
        {
            if (!VersionChecker.CheckEftVersion(Logger, Info, Config))
            {
                throw new Exception("Invalid EFT Version");
            }

            // Bind the configs
            DJConfig.BindConfig(Config);

            _hook = new GameObject("IR Object");
            Script = _hook.AddComponent<TheMaid>();
            DontDestroyOnLoad(_hook);

            new OnDeadPatch().Enable();
        }
    }
}
