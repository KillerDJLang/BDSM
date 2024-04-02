using System;
using EFT;
using System.Reflection;
using Aki.Reflection.Patching;
using BDSM.Helpers;

namespace BDSM.Patches
{
    public class OnDeadPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod() =>
            typeof(Player).GetMethod("OnDead", BindingFlags.Instance | BindingFlags.Public);

        [PatchPostfix]
        private static void PatchPostFix(ref Player __instance)
        {
            if (DJConfig.DropBackPack.Value && DJConfig.DropBackPackChance.Value > new Random().NextDouble())
            {
                __instance.DropBackpack();
            }
        }
    }
}