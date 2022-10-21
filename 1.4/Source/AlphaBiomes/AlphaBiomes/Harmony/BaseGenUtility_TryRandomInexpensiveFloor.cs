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




    /*This Harmony Postfix tries to remove Alpha Biomes floors from map ruins generation
   */
    [HarmonyPatch(typeof(BaseGenUtility))]
    [HarmonyPatch("TryRandomInexpensiveFloor")]
    public static class AlphaBiomes_BaseGenUtility_TryRandomInexpensiveFloor_Patch
    {
        [HarmonyPostfix]
        public static void RemoveAlphaBiomesFloorsFromRuins(ref bool __result, ref TerrainDef floor)

        {
           
                if (floor != null && floor.defName.Contains("AB_")) {
                    List<TerrainDef> vanillaTiles = new List<TerrainDef>();
                    vanillaTiles.Add(TerrainDef.Named("TileSandstone"));
                    vanillaTiles.Add(TerrainDef.Named("TileGranite"));
                    vanillaTiles.Add(TerrainDef.Named("TileLimestone"));
                    vanillaTiles.Add(TerrainDef.Named("TileSlate"));
                    vanillaTiles.Add(TerrainDef.Named("TileMarble"));
                    floor = vanillaTiles.RandomElement();
                }
          

        }

    }

   
    
}
