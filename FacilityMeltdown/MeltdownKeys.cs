using System;
using Dawn;

namespace FacilityMeltdown
{
	public static class MeltdownKeys
	{
		public static readonly NamespacedKey<DawnItemInfo> GeigerCounter = NamespacedKey<DawnItemInfo>.From("facility_meltdown", "geiger_counter");
	}
}
