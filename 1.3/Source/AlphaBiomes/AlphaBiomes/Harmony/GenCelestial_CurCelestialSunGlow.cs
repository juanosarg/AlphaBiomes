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



    /*This Harmony Postfix allows us to reduce the amount of light that reaches the Forsaken Crags
    */

   

    [HarmonyPatch(typeof(GenCelestial))]
    [HarmonyPatch("CurCelestialSunGlow")]
    [HarmonyPatch(new Type[] { typeof(Map) })]
    public static class AlphaBiomes_GenCelestial_CurCelestialSunGlow_Patch
    {
        private static Dictionary<Map, bool> isRockyCragsCache = new Dictionary<Map, bool>();
        [HarmonyPostfix]
        public static void AvoidTheLight(ref Map map, ref float __result)
        {
            if (!isRockyCragsCache.TryGetValue(map, out bool isRockyCrags))
            {
                isRockyCrags = map.Biome == InternalDefOf.AB_RockyCrags;
                isRockyCragsCache[map] = isRockyCrags;
            }
            if (isRockyCrags)
            {
                __result = __result * 0.34f;
             
            }
        }
    }

}
