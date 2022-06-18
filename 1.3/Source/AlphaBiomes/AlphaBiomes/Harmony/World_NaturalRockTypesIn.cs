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

 
   
    /*This Harmony Postfix allows us to create new rock types than only appear in certain biomes
    */
    [HarmonyPatch(typeof(World))]
    [HarmonyPatch("NaturalRockTypesIn")]
    public static class AlphaBiomes_World_NaturalRockTypesIn_Patch
    {

        public static string[] vanillaRockTypes = { "Marble", "Sandstone", "Limestone", "Granite", "Slate" };
        
        [HarmonyPostfix]
        public static void MakeRocksAccordingToBiome(int tile, ref World __instance, ref IEnumerable<ThingDef> __result)

        {
            if ((__instance.grid.tiles[tile].biome.defName=="BiomesIslands_Atoll")) {
                return;
            }

            if ((__instance.grid.tiles[tile].biome.defName == "RG_AnimaForest"))
            {
                return;
            }

            else if (__instance.grid.tiles[tile].biome == InternalDefOf.AB_OcularForest)
            {
                List<ThingDef> replacedList = new List<ThingDef>();
                ThingDef item = InternalDefOf.GU_RoseQuartz;
                replacedList.Add(item);
               
                __result = replacedList;
            } else if (__instance.grid.tiles[tile].biome == InternalDefOf.AB_GallatrossGraveyard)
            {
                List<ThingDef> replacedList = new List<ThingDef>();
                ThingDef item = InternalDefOf.AB_Mudstone;
                replacedList.Add(item);
                replacedList.Add(ThingDefOf.Sandstone);

                __result = replacedList;
            }
            else if (__instance.grid.tiles[tile].biome == InternalDefOf.AB_PyroclasticConflagration)
            {
                List<ThingDef> replacedList = new List<ThingDef>();
                ThingDef item = InternalDefOf.AB_Obsidianstone;
                replacedList.Add(item);
                replacedList.Add(DefDatabase<ThingDef>.GetNamed("Slate")); 

                __result = replacedList;
            }
            else if (__instance.grid.tiles[tile].biome == InternalDefOf.AB_GelatinousSuperorganism)
            {
                List<ThingDef> replacedList = new List<ThingDef>();
                ThingDef item = InternalDefOf.AB_SlimeStone;
                replacedList.Add(item);
               
                __result = replacedList;
            }
            else if (__instance.grid.tiles[tile].biome == InternalDefOf.AB_MechanoidIntrusion)
            {
                List<ThingDef> replacedList = new List<ThingDef>();
                ThingDef item = InternalDefOf.GU_AncientMetals;
                replacedList.Add(item);

                __result = replacedList;
            }
            else if (__instance.grid.tiles[tile].biome == InternalDefOf.AB_RockyCrags)
            {
                List<ThingDef> replacedList = new List<ThingDef>();
                ThingDef item = InternalDefOf.AB_Cragstone;
                replacedList.Add(item);

                __result = replacedList;
            }
            else {
                Rand.PushState();
                Rand.Seed = tile;
                List<ThingDef> list = (from d in DefDatabase<ThingDef>.AllDefs
                                       where d.category == ThingCategory.Building && d.building.isNaturalRock && !d.building.isResourceRock && 
                                       !d.IsSmoothed && d != InternalDefOf.GU_RoseQuartz && d != InternalDefOf.AB_Mudstone && d != InternalDefOf.AB_SlimeStone && 
                                       d != InternalDefOf.GU_AncientMetals && d != InternalDefOf.AB_Cragstone && d != InternalDefOf.AB_Obsidianstone && d.defName != "BiomesIslands_CoralRock"
                                       && d.defName != "RG_Jadeite"
                                       select d).ToList<ThingDef>();
                int num = Rand.RangeInclusive(2, 3);
                if (num > list.Count)
                {
                    num = list.Count;
                }
                List<ThingDef> list2 = new List<ThingDef>();
                for (int i = 0; i < num; i++)
                {
                    ThingDef item = list.RandomElement<ThingDef>();
                    list.Remove(item);
                    list2.Add(item);
                }
                Rand.PopState();
                __result = list2;
                

            }


        }
    }


}
