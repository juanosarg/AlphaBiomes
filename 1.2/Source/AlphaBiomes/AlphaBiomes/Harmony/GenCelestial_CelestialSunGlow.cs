using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;
using System.Collections.Generic;
using RimWorld.Planet;
using System.Linq;
using System;
using RimWorld.BaseGen;

// So, let's comment this code, since it uses Harmony and has moderate complexity

namespace AlphaBiomes
{



    /*This Harmony Postfix allows us to reduce the amount of light that reaches the Forsaken Crags
    */

    [DefOf]
    public static class AB_DefOf
    {
        public static BiomeDef AB_RockyCrags;
    }

    [HarmonyPatch(typeof(GenCelestial))]
    [HarmonyPatch("CelestialSunGlow")]
    public static class AlphaBiomes_GenCelestial_CelestialSunGlow_Patch
    {
        private static Dictionary<Map, bool> isRockyCragsCache = new Dictionary<Map, bool>();
        [HarmonyPostfix]
        public static void AvoidTheLight(ref Map map, ref float __result)
        {
            if (!isRockyCragsCache.TryGetValue(map, out bool isRockyCrags))
            {
                isRockyCrags = map.Biome == AB_DefOf.AB_RockyCrags;
                isRockyCragsCache[map] = isRockyCrags;
            }
            if (isRockyCrags)
            {
                __result = __result * 0.4f;
                // Log.Message("I have detected the biome, and light should be "+ __result);
            }
        }
    }

}
