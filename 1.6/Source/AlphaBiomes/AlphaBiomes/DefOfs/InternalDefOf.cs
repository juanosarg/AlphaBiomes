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
        public static WeatherDef AB_ForsakenNight_Alternate;
        public static WeatherDef AB_ForsakenRainyNight;
        public static WeatherDef AB_ForsakenRainyNight_Alternate;
        public static WeatherDef AB_ForsakenThunderstorm;
        public static WeatherDef AB_ForsakenThunderstorm_Alternate;
        public static WeatherDef AB_RedFog;
		public static WeatherDef AB_AcidRainWeather;

		public static ThingDef AB_TarPuddle;
		public static ThingDef AB_TarMote;
        public static ThingDef AB_MagmaMote;
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
		public static ThingDef AB_AlcyoniteSolar;
        public static ThingDef AB_AlcyoniteSolar_Advanced;
        public static ThingDef AB_AlcyoniteChunk;
        public static ThingDef AB_PropanePump;
        [MayRequire("VanillaExpanded.VFEPower")]
        public static ThingDef VFE_AdvancedSolarGenerator;
		[MayRequireOdyssey]
		public static ThingDef AB_AncientFreezingVent;
        [MayRequireOdyssey]
        public static ThingDef AB_AncientGreyPallVent;
        [MayRequireOdyssey]
        public static ThingDef AB_AncientBloodRainVent;
        [MayRequireOdyssey]
        public static ThingDef AB_AncientDeathPallVent;

        public static TerrainDef AB_ArtificialTar;
		public static TerrainDef AB_SoilOnCrackedMetal;
		public static TerrainDef AB_HardenedGrass;
		public static TerrainDef AB_SolidPropane;
        public static TerrainDef AB_PropaneLake;
        public static TerrainDef AB_TarMud;
		public static TerrainDef AB_Tar;
        public static TerrainDef AB_AsphaltBridge;
		public static TerrainDef AB_SolidifiedLava;
        [MayRequireOdyssey]
        public static TerrainDef AB_HealingSpringwater;
        [MayRequireOdyssey]
        public static TerrainDef AB_MutagenicOcularWater;
        [MayRequireOdyssey]
        public static TerrainDef AB_Quicksand;

        public static SoundDef AB_MagmaVent;

        public static JobDef AB_OperateCoreSampleDrill;

		public static DamageDef AB_Gangrene;
        [MayRequire("sarg.alphaanimals")]
        public static DamageDef AA_AcidRainDamage;

		[MayRequireOdyssey]
		public static PrefabDef AB_AgariluxPrime_Prefab;

		[MayRequireOdyssey]
		public static TileMutatorDef AB_PollinationFrenzy;
        [MayRequireOdyssey]
        public static TileMutatorDef AB_DigestiveSurface;
        [MayRequireOdyssey]
        public static TileMutatorDef AB_SymbioticNutrients;
        [MayRequireOdyssey]
        public static TileMutatorDef AB_QuiveringSurface;

        public static HediffDef AB_SporesAllergy_Heightened;
        public static HediffDef AB_SporesAllergy;
        [MayRequireOdyssey]
        public static HediffDef AB_Exploder;

        [MayRequireOdyssey]
        public static ThoughtDef AB_GelatinousEchoes_Good;
        [MayRequireOdyssey]
        public static ThoughtDef AB_GelatinousEchoes_Bad;

        [MayRequireAnomaly]
        public static MentalBreakDef DarkVisions;
        public static MentalBreakDef Wander_Sad;


    }
}
