using System;
using System.Reflection;
using BDSM.Helpers;
using EFT;
using SPT.Reflection.Patching;

namespace BDSM.Patches
{
    internal class OnDeadPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(Player).GetMethod("OnDead", BindingFlags.Instance | BindingFlags.Public);
        }

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
