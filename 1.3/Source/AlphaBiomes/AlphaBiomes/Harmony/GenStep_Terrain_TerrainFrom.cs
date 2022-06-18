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
          
            if ((__result == TerrainDefOf.Gravel) && (map.Biome == InternalDefOf.AB_MechanoidIntrusion))
            {
              
                __result = InternalDefOf.AB_SoilOnCrackedMetal;
            }
            if ((__result == TerrainDefOf.Gravel) && (map.Biome == InternalDefOf.AB_PyroclasticConflagration))
            {
              
                __result = InternalDefOf.AB_HardenedGrass;
            }

        }

    }


}
