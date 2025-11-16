using System;
using FacilityMeltdown.API;
using loaforcsSoundAPI.SoundPacks.Data.Conditions;

namespace FacilityMeltdown.Integrations.SoundAPI
{
	[SoundAPICondition("FacilityMeltdown:in_meltdown", false, null)]
	public class InMeltdownCondition : Condition
	{
		public bool? Value { get; private set; }

		public override bool Evaluate(IContext context)
		{
			return MeltdownAPI.MeltdownStarted == this.Value.GetValueOrDefault(true);
		}
	}
}
