using BepInEx;
using UnityEngine;
using DrakiaXYZ.VersionChecker;
using Comfort.Common;
using System;
using BDSM.Patches;
using BDSM.Helpers;
using EFT;

namespace BDSM
{
    [BepInPlugin("DJ.BDSM", "Body Disposal Service Maid", "1.4.1")]
    public class Plugin : BaseUnityPlugin
    {
        public const int TarkovVersion = 35392;

        internal static TheMaid Script;
        internal static GameObject Hook;

        internal static Player MyPlayer
        { get => MyGameworld.MainPlayer; }

        internal static GameWorld MyGameworld
        { get => Singleton<GameWorld>.Instance; }

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