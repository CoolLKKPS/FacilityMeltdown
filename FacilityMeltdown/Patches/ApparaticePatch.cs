using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacilityMeltdown.API;
using FacilityMeltdown.Behaviours;
using FacilityMeltdown.Integrations;
using FacilityMeltdown.MeltdownSequence.Behaviours;
using FacilityMeltdown.Util;
using GameNetcodeStuff;
using HarmonyLib;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FacilityMeltdown.Patches
{
    [HarmonyPatch(typeof(LungProp))]
    static class ApparaticePatch
    {
        [HarmonyPrefix, HarmonyPatch(nameof(LungProp.EquipItem)), HarmonyWrapSafe]
        internal static void BeginMeltdownSequence(LungProp __instance, ref bool ___isLungDocked)
        {
            if (!__instance.IsHost) return;
            if (!___isLungDocked) return;
            if (MeltdownAPI.MeltdownStarted) return;
            GameObject meltdown = GameObject.Instantiate(MeltdownPlugin.assets.meltdownHandlerPrefab);
            meltdown.GetComponent<MeltdownHandler>().causingLungProp = __instance;
            meltdown.GetComponent<NetworkObject>().Spawn();
        }

        [HarmonyFinalizer, HarmonyPatch(nameof(LungProp.EquipItem)), HarmonyWrapSafe]
        private static void CatchOtherModsBreakingRandomStuff(LungProp __instance, Exception __exception)
        {
            if (!__instance.IsHost)
            {
                return;
            }
            MeltdownPlugin.logger.LogDebug("finalizer is running..");
            if (!__instance.isLungDocked)
            {
                return;
            }
            MeltdownPlugin.logger.LogError("!!! isLungDocked is still true, things are broken! It'll try to salvage it to make sure nothing is very broken, but the meltdown sequence may not have started!");
            __instance.isLungDocked = false;
        }


        [HarmonyPrefix, HarmonyPatch(nameof(LungProp.Start)), HarmonyWrapSafe]
        internal static void AddRadiationSource(LungProp __instance)
        {
            if (!StartOfRound.Instance.inShipPhase)
            {
                MeltdownMoonMapper.EnsureMeltdownMoonMapper();
                MeltdownInteriorMapper.EnsureMeltdownInteriorMapper();
            }
            RadiationSource source = __instance.gameObject.AddComponent<RadiationSource>();
            source.radiationAmount = 80;
            source.radiationDistance = 60;

            if (MeltdownPlugin.config.OverrideApparatusValue)
            {
                __instance.scrapValue = Mathf.RoundToInt((float)MeltdownPlugin.config.ApparatusValue);
                try
                {
                    if (WeatherRegistryIntegration.Enabled)
                    {
                        __instance.scrapValue = Mathf.RoundToInt((float)__instance.scrapValue * WeatherRegistryIntegration.GetWeatherMultiplier());
                    }
                }
                catch (Exception exception)
                {
                    MeltdownPlugin.logger.LogWarning(exception.ToString());
                }
            }
            MeltdownPlugin.logger.LogDebug(__instance.scrapValue);
        }
    }
}
