using BepInEx;
using UnityEngine;
using BepInEx.Configuration;
using BDSM;

namespace BDSM
{
    [BepInPlugin("com.DJ.bdsm", "BDSM", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        internal static TheMaid Script;
        internal static GameObject Hook;

        internal static ConfigEntry<bool> DropBackPack;
        internal static ConfigEntry<bool> EnableClean;
        internal static ConfigEntry<float> TimeToClean;
        internal static ConfigEntry<int> DistToClean;
        internal static ConfigEntry<float> DropBackPackChance;

        void Awake()
        {
            EnableClean = Config.Bind("Clean", "Enable Clean.", true, "Enable Clean?");
            TimeToClean = Config.Bind("Clean", "Time to Clean", 120f, "Time to clean bodies.");
            DistToClean = Config.Bind("Clean", "Distance to Clean.", 30, "How far away should bodies be for cleanup.");

            DropBackPack = Config.Bind("Drop", "Drop Backpack", true, "Drop Backpack");
            DropBackPackChance = Config.Bind("Drop", "Backpack Drop Chance", 0.3f, "Chance of dropping a backpack");

            Hook = new GameObject("IR Object");
            Script = Hook.AddComponent<TheMaid>();
            DontDestroyOnLoad(Hook);
            new OnDeadPatch().Enable();
        }
    }
}