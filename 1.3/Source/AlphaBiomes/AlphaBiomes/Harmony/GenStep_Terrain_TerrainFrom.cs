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



    /*This Harmony Postfix allows us to remove gravel from a certain biome
    */
    [HarmonyPatch(typeof(GenStep_Terrain))]
    [HarmonyPatch("TerrainFrom")]
    public static class AlphaBiomes_GenStep_Terrain_TerrainFrom_Patch
    {
        [HarmonyPostfix]
        public static void RemoveGravel(Map map, ref TerrainDef __result)

        {
            //Log.Message(__result.defName);
            if ((__result == TerrainDefOf.Gravel) && (map.Biome.defName == "AB_MechanoidIntrusion"))
            {
                //Log.Message("Detectado e intentando cambiar");
                __result = TerrainDef.Named("AB_SoilOnCrackedMetal");
            }
            if ((__result == TerrainDefOf.Gravel) && (map.Biome.defName == "AB_PyroclasticConflagration"))
            {
                //Log.Message("Detectado e intentando cambiar");
                __result = TerrainDef.Named("AB_HardenedGrass");
            }

        }

    }


}
