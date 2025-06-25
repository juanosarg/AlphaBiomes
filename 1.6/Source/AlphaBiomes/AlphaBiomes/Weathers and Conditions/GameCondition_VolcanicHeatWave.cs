using System;
using UnityEngine;
using Verse;
using RimWorld;

namespace AlphaBiomes
{
	public class GameCondition_VolcanicHeatWave : GameCondition
	{
		
		

		public override float TemperatureOffset()
		{
			return GameConditionUtility.LerpInOutValue(this, (float)this.TransitionTicks, this.MaxTempOffset);
		}

		

		public override bool AllowEnjoyableOutsideNow(Map map)
		{
			return false;
		}

		private float MaxTempOffset = 30f;

		
	}
}
