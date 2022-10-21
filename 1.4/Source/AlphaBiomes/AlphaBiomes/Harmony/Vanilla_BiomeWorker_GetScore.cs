using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;
using System.Collections.Generic;
using RimWorld.Planet;
using System.Linq;
using System;
using RimWorld.BaseGen;



namespace AlphaBiomes
{




    /* Postfixes to remove vanilla biome generation*/
    [HarmonyPatch(typeof(BiomeWorker_TemperateForest))]
    [HarmonyPatch("GetScore")]
    public static class BiomeWorker_TemperateForest_GetScore_Patch
    {
        [HarmonyPostfix]
        public static void RemoveForest(ref float __result)
        {
            if (AlphaBiomes_Settings.AB_RemoveVanillaBiomes) {  __result = -100f;}
        }
    }
    [HarmonyPatch(typeof(BiomeWorker_AridShrubland))]
    [HarmonyPatch("GetScore")]
    public static class BiomeWorker_AridShrubland_GetScore_Patch
    {
        [HarmonyPostfix]
        public static void RemoveShrub(ref float __result)
        {
            if (AlphaBiomes_Settings.AB_RemoveVanillaBiomes) { __result = -100f; }
        }
    }
    [HarmonyPatch(typeof(BiomeWorker_BorealForest))]
    [HarmonyPatch("GetScore")]
    public static class BiomeWorker_BorealForest_GetScore_Patch
    {
        [HarmonyPostfix]
        public static void RemoveBoreal(ref float __result)
        {
            if (AlphaBiomes_Settings.AB_RemoveVanillaBiomes) { __result = -100f; }
        }
    }
    [HarmonyPatch(typeof(BiomeWorker_ColdBog))]
    [HarmonyPatch("GetScore")]
    public static class BiomeWorker_ColdBog_GetScore_Patch
    {
        [HarmonyPostfix]
        public static void RemoveColdBog(ref float __result)
        {
            if (AlphaBiomes_Settings.AB_RemoveVanillaBiomes) { __result = -100f; }
        }
    }
    [HarmonyPatch(typeof(BiomeWorker_Desert))]
    [HarmonyPatch("GetScore")]
    public static class BiomeWorker_Desert_GetScore_Patch
    {
        [HarmonyPostfix]
        public static void RemoveDesert(ref float __result)
        {
            if (AlphaBiomes_Settings.AB_RemoveVanillaBiomes) { __result = -100f; }
        }
    }
    [HarmonyPatch(typeof(BiomeWorker_ExtremeDesert))]
    [HarmonyPatch("GetScore")]
    public static class BiomeWorker_ExtremeDesert_GetScore_Patch
    {
        [HarmonyPostfix]
        public static void RemoveExtDesert(ref float __result)
        {
            if (AlphaBiomes_Settings.AB_RemoveVanillaBiomes) { __result = -100f; }
        }
    }
    [HarmonyPatch(typeof(BiomeWorker_IceSheet))]
    [HarmonyPatch("GetScore")]
    public static class BiomeWorker_IceSheet_GetScore_Patch
    {
        [HarmonyPostfix]
        public static void RemoveIceSh(ref float __result)
        {
            if (AlphaBiomes_Settings.AB_RemoveVanillaBiomes) { __result = -100f; }
        }
    }
    [HarmonyPatch(typeof(BiomeWorker_SeaIce))]
    [HarmonyPatch("GetScore")]
    public static class BiomeWorker_SeaIce_GetScore_Patch
    {
        [HarmonyPostfix]
        public static void RemoveSeaIce(ref float __result)
        {
            if (AlphaBiomes_Settings.AB_RemoveVanillaBiomes) { __result = -100f; }
        }
    }
    [HarmonyPatch(typeof(BiomeWorker_TemperateSwamp))]
    [HarmonyPatch("GetScore")]
    public static class BiomeWorker_TemperateSwamp_GetScore_Patch
    {
        [HarmonyPostfix]
        public static void RemoveTempSw(ref float __result)
        {
            if (AlphaBiomes_Settings.AB_RemoveVanillaBiomes) { __result = -100f; }
        }
    }
    [HarmonyPatch(typeof(BiomeWorker_TropicalRainforest))]
    [HarmonyPatch("GetScore")]
    public static class BiomeWorker_TropicalRainforest_GetScore_Patch
    {
        [HarmonyPostfix]
        public static void RemoveTropRain(ref float __result)
        {
            if (AlphaBiomes_Settings.AB_RemoveVanillaBiomes) { __result = -100f; }
        }
    }
    [HarmonyPatch(typeof(BiomeWorker_TropicalSwamp))]
    [HarmonyPatch("GetScore")]
    public static class BiomeWorker_TropicalSwamp_GetScore_Patch
    {
        [HarmonyPostfix]
        public static void RemoveTropSw(ref float __result)
        {
            if (AlphaBiomes_Settings.AB_RemoveVanillaBiomes) { __result = -100f; }
        }
    }
    [HarmonyPatch(typeof(BiomeWorker_Tundra))]
    [HarmonyPatch("GetScore")]
    public static class BiomeWorker_Tundra_GetScore_Patch
    {
        [HarmonyPostfix]
        public static void RemoveTundra(ref float __result)
        {
            if (AlphaBiomes_Settings.AB_RemoveVanillaBiomes) { __result = -100f; }
        }
    }
}
