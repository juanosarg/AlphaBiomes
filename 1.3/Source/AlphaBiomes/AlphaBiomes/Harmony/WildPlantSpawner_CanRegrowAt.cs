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


    /*This Harmony Postfix allows us to regrow plants in a cold biome
   */
    [HarmonyPatch(typeof(WildPlantSpawner))]
    [HarmonyPatch("CanRegrowAt")]
    public static class AlphaBiomes_WildPlantSpawner_CanRegrowAt_Patch
    {
        [HarmonyPostfix]
        public static void RegrowOnCold(IntVec3 c, Map ___map, ref bool __result, ref WildPlantSpawner __instance)

        {
           
            if (___map.Biome == InternalDefOf.AB_PropaneLakes)
            {
               
                __result = !c.Roofed(___map);
            }



        }

    }

}
