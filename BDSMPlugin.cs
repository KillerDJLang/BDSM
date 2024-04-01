using BepInEx;
using UnityEngine;
using BepInEx.Configuration;
using DrakiaXYZ.VersionChecker;
using System;

namespace BDSM
{
    [BepInPlugin("DJ.BDSM", "BDSM", "1.1.0")]
    public class Plugin : BaseUnityPlugin
    {
        public const int TarkovVersion = 29197;

        internal static TheMaid Script;
        internal static GameObject Hook;

        internal static ConfigEntry<bool> DropBackPack;
        internal static ConfigEntry<bool> EnableClean;
        internal static ConfigEntry<float> TimeToClean;
        internal static ConfigEntry<int> DistToClean;
        internal static ConfigEntry<float> DropBackPackChance;

        void Awake()
        {
            if (!VersionChecker.CheckEftVersion(Logger, Info, Config))
            {
                throw new Exception("Invalid EFT Version");
            }

            EnableClean = Config.Bind("Clean", "Enable Clean.", true, "Enable Clean?");
            TimeToClean = Config.Bind("Clean", "Time to Clean", 120f, "Time to clean bodies.");
            DistToClean = Config.Bind("Clean", "Distance to Clean.", 30, "How far away should bodies be for cleanup.");

            DropBackPack = Config.Bind("Drop", "Drop Backpack", true, "Drop Backpack on death");
            DropBackPackChance = Config.Bind("Drop", "Backpack Drop Chance", 0.3f, "Chance of dropping a backpack on death");

            Hook = new GameObject("IR Object");
            Script = Hook.AddComponent<TheMaid>();
            DontDestroyOnLoad(Hook);
            new OnDeadPatch().Enable();
        }
    }
}