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
            Log.Message(__result.defName);
            if ((__result == TerrainDefOf.Gravel)&&(map.Biome.defName== "AB_MechanoidIntrusion"))
            {
                Log.Message("Detectado e intentando cambiar");
                __result = TerrainDef.Named("AB_SoilOnCrackedMetal");
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
            else {
                Rand.PushState();
                Rand.Seed = tile;
                List<ThingDef> list = (from d in DefDatabase<ThingDef>.AllDefs
                                       where d.category == ThingCategory.Building && d.building.isNaturalRock && !d.building.isResourceRock && !d.IsSmoothed && d.defName!= "GU_RoseQuartz" && d.defName != "AB_Mudstone" && d.defName != "AB_SlimeStone" && d.defName != "GU_AncientMetals"
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
