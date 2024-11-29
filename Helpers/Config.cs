using BepInEx.Configuration;

namespace BDSM.Helpers
{
    public static class DJConfig
    {
        public static ConfigEntry<bool> EnableClean;
        public static ConfigEntry<int> TimeToClean;
        public static ConfigEntry<int> DistToClean;

        public static ConfigEntry<bool> DropBackPack;
        public static ConfigEntry<float> DropBackPackChance;

        public static void BindConfig(ConfigFile cfg)
        {
            #region Body Clean Up

            EnableClean = cfg.Bind(
                "Body Cleanup Configs",
                "Enable Clean",
                true,
                new ConfigDescription("Enable body cleanup event.\nThis requires a restart to take effect after enabling or disabling!",
                null,
                new ConfigurationManagerAttributes { IsAdvanced = false, ShowRangeAsPercent = false, Order = 3 }));

            TimeToClean = cfg.Bind(
                "Body Cleanup Configs",
                "Time to Clean",
                15,
                new ConfigDescription("The time to clean bodies calculated in minutes.",
                new AcceptableValueRange<int>(1, 60),
                new ConfigurationManagerAttributes { IsAdvanced = false, ShowRangeAsPercent = false, Order = 2 }));

            DistToClean = cfg.Bind(
                "Body Cleanup Configs",
                "Distance to Clean",
                15,
                new ConfigDescription("How far away bodies should be for cleanup.",
                null,
                new ConfigurationManagerAttributes { IsAdvanced = false, ShowRangeAsPercent = false, Order = 1 }));

            #endregion

            #region Other

            DropBackPack = cfg.Bind(
                "Backpack Drop Configs",
                "Drop Backpack",
                true,
                new ConfigDescription("Enable the dropping of backpacks on death or cleanup.\nThis requires a restart to take effect after enabling or disabling!",
                null,
                new ConfigurationManagerAttributes { IsAdvanced = false, ShowRangeAsPercent = true, Order = 2 }));

            DropBackPackChance = cfg.Bind(
                "Backpack Drop Configs",
                "Backpack Drop Chance",
                0.3f,
                new ConfigDescription("Chance of dropping a backpack on kill or cleanup.",
                new AcceptableValueRange<float>(0f, 1f),
                new ConfigurationManagerAttributes { IsAdvanced = false, ShowRangeAsPercent = true, Order = 1 }));

            #endregion
        }
    }
}
