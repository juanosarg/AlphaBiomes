using Harmony;
using RimWorld;
using System.Reflection;
using Verse;
using System.Collections.Generic;
using RimWorld.Planet;
using System.Linq;
using System;

// So, let's comment this code, since it uses Harmony and has moderate complexity

namespace AlphaBiomes
{

    

    //Setting the Harmony instance
    [StaticConstructorOnStartup]
    public class Main
    {
        static Main()
        {
            var harmony = HarmonyInstance.Create("com.alphabiomes");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }


    }


    /*This Harmony Postfix allows us to remove gravel from a certain biome
    */
    [HarmonyPatch(typeof(GenStep_Terrain))]
    [HarmonyPatch("TerrainFrom")]
    public static class GenStep_Terrain_TerrainFrom_Patch
    {
        [HarmonyPostfix]
        public static void RemoveGravel(Map map,ref TerrainDef __result)

        {
            //Log.Message(__result.defName);
            if ((__result == TerrainDefOf.Gravel)&&(map.Biome.defName== "AB_MechanoidIntrusion"))
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


    /*This Harmony Postfix allows us to regrow plants in a cold biome
*/
    [HarmonyPatch(typeof(WildPlantSpawner))]
    [HarmonyPatch("CanRegrowAt")]
    public static class WildPlantSpawner_CanRegrowAt_Patch
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

    /*This Harmony Postfix allows us to reduce the amount of light that reaches the Forsaken Crags
*/
    [HarmonyPatch(typeof(GenCelestial))]
    [HarmonyPatch("CelestialSunGlow")]
    public static class GenCelestial_CelestialSunGlow_Patch
    {
        [HarmonyPostfix]
        public static void AvoidTheLight(ref Map map, ref float __result)

        {
           
            if (map.Biome.defName == "AB_RockyCrags")
            {


               // Log.Message("I have detected the biome, and light should be "+ __result);
               
                __result= __result*0.4f;
            }



        }

    }

    /*This Harmony Prefix allows us to remove rock filth
 */
    [HarmonyPatch(typeof(GenStep_RockChunks))]
    [HarmonyPatch("GrowLowRockFormationFrom")]
    public static class GenStep_RockChunks_GrowLowRockFormationFrom_Patch
    {
        [HarmonyPrefix]
        public static bool RemoveRockFilth(ref IntVec3 root, ref Map map)

        {
            if ((map.Biome.defName == "AB_MechanoidIntrusion"))
            {
                ThingDef mineableThing = Find.World.NaturalRockTypesIn(map.Tile).RandomElement<ThingDef>().building.mineableThing;
                Rot4 random = Rot4.Random;
                MapGenFloatGrid elevation = MapGenerator.Elevation;
                IntVec3 intVec = root;
                for (; ; )
                {
                    Rot4 random2 = Rot4.Random;
                    if (!(random2 == random))
                    {
                        intVec += random2.FacingCell;
                        if (!intVec.InBounds(map) || intVec.GetEdifice(map) != null || intVec.GetFirstItem(map) != null)
                        {
                            break;
                        }
                        if (elevation[intVec] > 0.55f)
                        {
                            return false;
                        }
                        if (!map.terrainGrid.TerrainAt(intVec).affordances.Contains(TerrainAffordanceDefOf.Heavy))
                        {
                            return false;
                        }
                        GenSpawn.Spawn(mineableThing, intVec, map, WipeMode.Vanish);
                        foreach (IntVec3 b in GenAdj.AdjacentCellsAndInside)
                        {
                            if (Rand.Value < 0.5f)
                            {
                                IntVec3 c = intVec + b;
                                if (c.InBounds(map))
                                {
                                    bool flag = false;
                                    List<Thing> thingList = c.GetThingList(map);
                                    for (int j = 0; j < thingList.Count; j++)
                                    {
                                        Thing thing = thingList[j];
                                        if (thing.def.category != ThingCategory.Plant && thing.def.category != ThingCategory.Item && thing.def.category != ThingCategory.Pawn)
                                        {
                                            flag = true;
                                            break;
                                        }
                                    }
                                    if (!flag)
                                    {
                                        FilthMaker.MakeFilth(c, map, DefDatabase<ThingDef>.GetNamed("AB_SlagRubble"), 1);
                                    }
                                }
                            }
                        }
                    }
                }
                return false;
            }
            else return true;

        }

    }

    /*This Harmony Postfix allows us to create new rock types than only appear in certain biomes
    */
    [HarmonyPatch(typeof(World))]
    [HarmonyPatch("NaturalRockTypesIn")]
    public static class World_NaturalRockTypesIn_Patch
    {

        public static string[] vanillaRockTypes = { "Marble", "Sandstone", "Limestone", "Granite", "Slate" };
        
        [HarmonyPostfix]
        public static void MakeRocksAccordingToBiome(int tile, ref World __instance, ref IEnumerable<ThingDef> __result)

        {

            if (__instance.grid.tiles[tile].biome.defName == "AB_OcularForest")
            {
                List<ThingDef> replacedList = new List<ThingDef>();
                ThingDef item = DefDatabase<ThingDef>.GetNamed("GU_RoseQuartz");
                replacedList.Add(item);
               
                __result = replacedList;
            } else if (__instance.grid.tiles[tile].biome.defName == "AB_GallatrossGraveyard")
            {
                List<ThingDef> replacedList = new List<ThingDef>();
                ThingDef item = DefDatabase<ThingDef>.GetNamed("AB_Mudstone");
                replacedList.Add(item);
                replacedList.Add(ThingDefOf.Sandstone);

                __result = replacedList;
            }
            else if (__instance.grid.tiles[tile].biome.defName == "AB_PyroclasticConflagration")
            {
                List<ThingDef> replacedList = new List<ThingDef>();
                ThingDef item = DefDatabase<ThingDef>.GetNamed("AB_Obsidianstone");
                replacedList.Add(item);
                replacedList.Add(DefDatabase<ThingDef>.GetNamed("Slate"));

                __result = replacedList;
            }
            else if (__instance.grid.tiles[tile].biome.defName == "AB_GelatinousSuperorganism")
            {
                List<ThingDef> replacedList = new List<ThingDef>();
                ThingDef item = DefDatabase<ThingDef>.GetNamed("AB_SlimeStone");
                replacedList.Add(item);
               
                __result = replacedList;
            }
            else if (__instance.grid.tiles[tile].biome.defName == "AB_MechanoidIntrusion")
            {
                List<ThingDef> replacedList = new List<ThingDef>();
                ThingDef item = DefDatabase<ThingDef>.GetNamed("GU_AncientMetals");
                replacedList.Add(item);

                __result = replacedList;
            }
            else if (__instance.grid.tiles[tile].biome.defName == "AB_RockyCrags")
            {
                List<ThingDef> replacedList = new List<ThingDef>();
                ThingDef item = DefDatabase<ThingDef>.GetNamed("AB_Cragstone");
                replacedList.Add(item);

                __result = replacedList;
            }
            else {
                Rand.PushState();
                Rand.Seed = tile;
                List<ThingDef> list = (from d in DefDatabase<ThingDef>.AllDefs
                                       where d.category == ThingCategory.Building && d.building.isNaturalRock && !d.building.isResourceRock && 
                                       !d.IsSmoothed && d.defName!= "GU_RoseQuartz" && d.defName != "AB_Mudstone" && d.defName != "AB_SlimeStone" && 
                                       d.defName != "GU_AncientMetals" && d.defName != "AB_Cragstone" && d.defName != "AB_Obsidianstone"
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
