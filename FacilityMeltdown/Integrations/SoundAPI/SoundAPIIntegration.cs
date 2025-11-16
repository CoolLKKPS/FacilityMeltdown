using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using FacilityMeltdown;
using loaforcsSoundAPI;

internal static class SoundAPIIntegration
{
	public static bool Enabled { get; private set; }

	[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
	private static void Initialize()
	{
		Enabled = true;
		try
		{
			Register();
		}
		catch (Exception)
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
