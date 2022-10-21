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
		public static GameConditionDef AB_AcidRainCondition;

		public static WeatherDef AB_ForsakenNight;
		public static WeatherDef AB_RedFog;
		public static WeatherDef AB_AcidRainWeather;

		public static ThingDef AB_TarPuddle;
		public static ThingDef AB_TarMote;
		public static ThingDef AB_AgariluxPrime;
		public static ThingDef AB_TarHole;
		public static ThingDef AB_SlagRubble;
		public static ThingDef GU_RoseQuartz;
		public static ThingDef AB_Mudstone;
		public static ThingDef AB_Obsidianstone;
		public static ThingDef AB_SlimeStone;
		public static ThingDef GU_AncientMetals;
		public static ThingDef AB_Cragstone;
		public static ThingDef AB_CoreSampleDrill;

		public static TerrainDef AB_ArtificialTar;
		public static TerrainDef AB_SoilOnCrackedMetal;
		public static TerrainDef AB_HardenedGrass;
		public static TerrainDef AB_SolidPropane;
		public static TerrainDef AB_TarMud;
		public static TerrainDef AB_AsphaltBridge;

		public static JobDef AB_OperateCoreSampleDrill;

		public static DamageDef AB_Gangrene;

	}
}
