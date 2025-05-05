using EFT;
using UnityEngine;
using System.Threading.Tasks;
using System.Collections;
using BDSM.Helpers;

namespace BDSM.Patches
{
    internal class TheMaid : MonoBehaviour
    {
        private static bool _MaidOnStandby = false;

        void Update()
        {
            if (!Ready() || !DJConfig.EnableClean.Value)
            {
                return;
            }

            if (!_MaidOnStandby)
            {
                StaticManager.Instance.StartCoroutine(StartClean());
                _MaidOnStandby = true;
            }
        }

        private static IEnumerator StartClean()
        {
            yield return new WaitForSeconds(DJConfig.TimeToClean.Value * 60f);

            if (Ready())
            {
                Task.Delay(10000);
                foreach (BotOwner bot in FindObjectsOfType<BotOwner>())
                {
                    if (!bot.HealthController.IsAlive && Vector3.Distance(Plugin.MyPlayer.Transform.position, bot.Transform.position) >= DJConfig.DistToClean.Value)
                    {
                        bot.gameObject.SetActive(false);
                    }
                }
            }

            else
            {
                _MaidOnStandby = false;
                yield break;
            }

            _MaidOnStandby = false;
            yield break;
        }

        internal static void RunMaidService()
        {
            if (Ready())
            {
                Task.Delay(10000);
                foreach (BotOwner bot in FindObjectsOfType<BotOwner>())
                {
                    if (!bot.HealthController.IsAlive && Vector3.Distance(Plugin.MyPlayer.Transform.position, bot.Transform.position) >= DJConfig.DistToClean.Value)
                    {
                        bot.gameObject.SetActive(false);
                    }
                }
            }
        }

        private static bool Ready() => Plugin.MyGameworld != null && Plugin.MyGameworld.AllAlivePlayersList != null && Plugin.MyGameworld.AllAlivePlayersList.Count > 0 && !(Plugin.MyPlayer is HideoutPlayer);
    }
}