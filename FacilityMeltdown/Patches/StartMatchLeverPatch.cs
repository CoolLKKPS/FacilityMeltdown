using System;
using FacilityMeltdown.MeltdownSequence.Behaviours;
using HarmonyLib;

namespace FacilityMeltdown.Patches
{
	[HarmonyPatch(typeof(StartMatchLever))]
	internal static class StartMatchLeverPatch
	{
		[HarmonyPostfix]
		[HarmonyPatch("EndGame")]
		private static void ShortenMeltdownTimer()
		{
			if (!MeltdownHandler.Instance)
			{
				return;
			}
			if (!MeltdownPlugin.config.ShortenMeltdownTimerOnShipLeave)
			{
				return;
			}
			MeltdownHandler.Instance.meltdownTimer = 3f;
		}
	}
}
