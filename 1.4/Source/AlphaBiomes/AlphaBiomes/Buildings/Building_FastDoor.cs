// RimWorld.Building_Door
using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.AI;
using Verse.AI.Group;
using Verse.Sound;

namespace AlphaBiomes
{


	public class Building_FastDoor : Building_Door
	{


		private const float OpenTicks = 38f;



		public new int TicksToOpenNow
		{
			get
			{
				float num = OpenTicks / this.GetStatValue(StatDefOf.DoorOpenSpeed);
				if (DoorPowerOn)
				{
					num *= 0.25f;
				}
				return Mathf.RoundToInt(num);
			}
		}



	}
}
