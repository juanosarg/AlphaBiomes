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
        public static void RegrowOnCold(IntVec3 c, ref bool __result, ref WildPlantSpawner __instance)

        {
            Map map = (Map)typeof(WildPlantSpawner).GetField("map", AccessTools.all).GetValue(__instance);
            //Log.Message(__result.defName);
            if (map.Biome.defName == "AB_PropaneLakes")
            {
                //Log.Message("Detectado e intentando cambiar");
                __result = !c.Roofed(map);
            }



        }

    }

}
