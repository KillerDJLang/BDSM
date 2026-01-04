using System.Collections;
using System.Threading.Tasks;
using BDSM.Helpers;
using EFT;
using UnityEngine;

namespace BDSM.Patches
{
    internal class TheMaid : MonoBehaviour
    {
        private static bool _maidOnStandby = false;

        private void Update()
        {
            if (!Ready() || !DJConfig.EnableClean.Value)
            {
                return;
            }

            if (!_maidOnStandby)
            {
                StaticManager.Instance.StartCoroutine(StartClean());
                _maidOnStandby = true;
            }
        }

        private static IEnumerator StartClean()
        {
            yield return new WaitForSeconds(DJConfig.TimeToClean.Value * 60f);

            if (Ready())
            {
                Task.Delay(10000);
                foreach (var bot in FindObjectsOfType<BotOwner>())
                {
                    if (
                        !bot.HealthController.IsAlive
                        && Vector3.Distance(Plugin.MyPlayer.Transform.position, bot.Transform.position) >= DJConfig.DistToClean.Value
                    )
                    {
                        bot.gameObject.SetActive(false);
                    }
                }
            }

            _maidOnStandby = false;
            yield break;
        }

        internal static void RunMaidService()
        {
            if (Ready())
            {
                Task.Delay(10000);
                foreach (var bot in FindObjectsOfType<BotOwner>())
                {
                    if (
                        !bot.HealthController.IsAlive
                        && Vector3.Distance(Plugin.MyPlayer.Transform.position, bot.Transform.position) >= DJConfig.DistToClean.Value
                    )
                    {
                        bot.gameObject.SetActive(false);
                    }
                }
            }
        }

        private static bool Ready()
        {
            return Plugin.MyGameworld != null
                && Plugin.MyGameworld.AllAlivePlayersList != null
                && Plugin.MyGameworld.AllAlivePlayersList.Count > 0
                && !(Plugin.MyPlayer is HideoutPlayer);
        }
    }
}
