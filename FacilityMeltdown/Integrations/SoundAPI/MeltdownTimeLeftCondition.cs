using System;
using System.Collections.Generic;
using FacilityMeltdown.MeltdownSequence.Behaviours;
using loaforcsSoundAPI.Core.Data;
using loaforcsSoundAPI.SoundPacks.Data.Conditions;

namespace FacilityMeltdown.Integrations.SoundAPI
{
	[SoundAPICondition("FacilityMeltdown:time_left", false, null)]
	public class MeltdownTimeLeftCondition : Condition
	{
		public string Value { get; private set; }

		public override bool Evaluate(IContext context)
		{
			return MeltdownHandler.Instance && base.EvaluateRangeOperator(MeltdownHandler.Instance.TimeLeftUntilMeltdown, this.Value);
		}

		public override List<IValidatable.ValidationResult> Validate()
		{
			IValidatable.ValidationResult result;
			if (base.ValidateRangeOperator(this.Value, ref result))
			{
				return new List<IValidatable.ValidationResult>(1) { result };
			}
			return new List<IValidatable.ValidationResult>();
		}
	}
}
