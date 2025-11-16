using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using loaforcsSoundAPI;

namespace FacilityMeltdown.Integrations.SoundAPI
{
	internal static class SoundAPIIntegration
	{
		public static bool Enabled { get; private set; }

		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		private static void Initialize()
		{
			SoundAPIIntegration.Enabled = true;
			try
			{
				SoundAPIIntegration.Register();
			}
			catch (Exception exception)
			{
				MeltdownPlugin.logger.LogWarning("Failed to register SoundAPI conditions, probably v1 and not v2.");
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		private static void Register()
		{
			SoundAPI.RegisterAll(Assembly.GetExecutingAssembly());
		}
	}
}
