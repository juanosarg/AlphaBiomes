using System;
using RimWorld;
using Verse;
using System.Collections.Generic;

namespace AlphaBiomes
{
	[DefOf]
	public static class InternalDefOf
	{
		static InternalDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(InternalDefOf));
		}

		public static BiomeDef AB_FeraliskInfestedJungle;
		public static BiomeDef AB_RockyCrags;
		public static BiomeDef AB_GallatrossGraveyard;
		public static BiomeDef AB_GelatinousSuperorganism;
		public static BiomeDef AB_IdyllicMeadows;
		public static BiomeDef AB_MechanoidIntrusion;
		public static BiomeDef AB_MiasmicMangrove;
		public static BiomeDef AB_MycoticJungle;
		public static BiomeDef AB_OcularForest;
		public static BiomeDef AB_PropaneLakes;
		public static BiomeDef AB_PyroclasticConflagration;
		public static BiomeDef AB_TarPits;

		public static GameConditionDef AB_VolcanicHeatWave;


	}
}
