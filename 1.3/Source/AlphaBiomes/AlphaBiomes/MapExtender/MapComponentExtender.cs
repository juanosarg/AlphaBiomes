using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using UnityEngine;
using RimWorld.Planet;

namespace AlphaBiomes
{
    public class MapComponentExtender : MapComponent
    {

        public bool verifyFirstTime = true;
        public int spawnCounter = 0;
        public int tickInterval = 10000;
        public int tickCounter = 0;


        public MapComponentExtender(Map map) : base(map)
        {

        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look<bool>(ref this.verifyFirstTime, "verifyFirstTime", true, true);


        }

        public override void MapComponentTick()
        {
            tickCounter++;
            if (tickCounter > tickInterval)
            {
                if (map.Biome.defName == "AB_PyroclasticConflagration")
                {
                    if (!map.gameConditionManager.ConditionIsActive(GameConditionDef.Named("AB_VolcanicHeatWave")))
                    {
                        GameCondition gameCondition = GameConditionMaker.MakeCondition(GameConditionDef.Named("AB_VolcanicHeatWave"), -1);
                        gameCondition.Duration = gameCondition.TransitionTicks;
                        gameCondition.Permanent = true;
                        gameCondition.conditionCauser = null;
                        map.gameConditionManager.RegisterCondition(gameCondition);
                    }
                    


                }

                tickCounter = 0;
            }



            base.MapComponentTick();
        }

        public override void FinalizeInit()
        {

            base.FinalizeInit();

            if (verifyFirstTime)
            {
                this.doMapSpawns();
                if (map.Biome.defName == "AB_RockyCrags")
                {
                    map.weatherManager.curWeather = WeatherDef.Named("AB_ForsakenNight");
                    map.weatherManager.TransitionTo(WeatherDef.Named("AB_ForsakenNight"));


                }

                if (map.Biome.defName == "AB_PyroclasticConflagration")
                {
                    GameCondition gameCondition = GameConditionMaker.MakeCondition(GameConditionDef.Named("AB_VolcanicHeatWave"), -1);
                    gameCondition.Duration = gameCondition.TransitionTicks;
                    gameCondition.Permanent = true;
                    gameCondition.conditionCauser = null;
                    map.gameConditionManager.RegisterCondition(gameCondition);
                   

                }

            }

        }

        public void doMapSpawns()
        {



            if (map.Biome.defName.Contains("AB_"))
            {
                foreach (SpecialSpawnsDef element in DefDatabase<SpecialSpawnsDef>.AllDefs.Where(element => element.allowedBiome == map.Biome.defName))
                {
                    IEnumerable<IntVec3> tmpTerrain = map.AllCells.InRandomOrder();


                    int extraGeneration = 0;
                    foreach (string biome in element.biomesWithExtraGeneration)
                    {
                        if (map.Biome.defName == biome)
                        {
                            extraGeneration = element.extraGeneration;
                        }

                    }

                    bool canSpawn = true;
                    if (spawnCounter == 0)
                    {
                        spawnCounter = Rand.RangeInclusive(element.numberToSpawn.min, element.numberToSpawn.max) + extraGeneration;
                        //Log.Message(spawnCounter.ToString());
                    }
                    foreach (IntVec3 c in tmpTerrain)
                    {




                        TerrainDef terrain = c.GetTerrain(map);


                        bool flagAllowed = true;
                        foreach (string allowed in element.terrainValidationAllowed)
                        {
                            if (terrain.defName == allowed)
                            {
                                break;
                            } else flagAllowed = false;
                           
                        }
                        bool flagDisallowed = true;
                        foreach (string notAllowed in element.terrainValidationDisallowed)
                        {
                            if (terrain.HasTag(notAllowed))
                            {
                                flagDisallowed = false;
                                break;
                            }
                        }
                        bool flagWater = true;
                        if (!element.allowOnWater && terrain.IsWater)
                        {
                            flagWater = false;

                        }
                        bool flagCenter = true;
                        if (element.findCellsOutsideColony)
                        {
                            if (!OutOfCenter(c, map, 60))
                            {
                                flagCenter = false;

                            }

                        }
                        canSpawn = flagAllowed & flagDisallowed & flagWater & flagCenter;
                        if (canSpawn)
                        {
                           // Log.Message("Sucesful c was " + c.ToString());
                            Thing thing = (Thing)ThingMaker.MakeThing(element.thingDef, null);
                            CellRect occupiedRect = GenAdj.OccupiedRect(c, thing.Rotation, thing.def.Size);
                            if (occupiedRect.InBounds(map))
                            {
                               // Log.Message("Prior to " + element.defName + " .Spawncounter was " + spawnCounter);
                                GenSpawn.Spawn(thing, c, map);
                                spawnCounter--;
                               // Log.Message("Spawning " + element.defName + " .Spawncounter was " + spawnCounter);



                            }



                        }
                        if (canSpawn && spawnCounter <= 0)
                        {
                            //Log.Message("Spawn counter is " + spawnCounter + " So I'm getting out");
                            spawnCounter = 0;
                            break;
                        }
                    }


                }



            }



            this.verifyFirstTime = false;

        }

        public static bool OutOfCenter(IntVec3 c, Map map, int centerDist)
        {
            IntVec3 CenterPoint = map.Center;

            /*  Log.Message("Tamaño mapa :"+map.Size.ToString());
              Log.Message("Centro mapa :" + CenterPoint.ToString());
              Log.Message("Punto a evaluar :" + c.ToString());
              Log.Message((c.x < CenterPoint.x - centerDist || c.z < CenterPoint.z - centerDist || c.x >= CenterPoint.x + centerDist || c.z >= CenterPoint.z + centerDist).ToString());*/
            return c.x < CenterPoint.x - centerDist || c.z < CenterPoint.z - centerDist || c.x >= CenterPoint.x + centerDist || c.z >= CenterPoint.z + centerDist;
        }

    }


}
