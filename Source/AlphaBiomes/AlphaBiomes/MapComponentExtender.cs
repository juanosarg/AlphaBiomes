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
       
        public MapComponentExtender(Map map) : base(map)
        {

        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look<bool>(ref this.verifyFirstTime, "verifyFirstTime", true, true);

          
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

            }
            
        }

        public void doMapSpawns()
        {

            IEnumerable<IntVec3> tmpTerrain = map.AllCells.InRandomOrder(); 

            if (map.Biome.defName.Contains("AB_")) {
                foreach (SpecialSpawnsDef element in DefDatabase<SpecialSpawnsDef>.AllDefs.Where(element => element.allowedBiome == map.Biome.defName))
                {
                    bool canSpawn = true;
                    if (spawnCounter == 0)
                    {
                        spawnCounter = element.numberToSpawn;
                    }
                    foreach (IntVec3 c in tmpTerrain)
                    {
                        
                        TerrainDef terrain = c.GetTerrain(map);

                            foreach (string allowed in element.terrainValidationAllowed)
                            {
                                if (terrain.defName == allowed)
                                {
                                    canSpawn = true;
                                    break;
                                }
                                canSpawn = false;
                            }
                            foreach (string notAllowed in element.terrainValidationDisallowed)
                            {
                                if (terrain.HasTag(notAllowed))
                                {
                                    canSpawn = false;
                                    break;
                                }
                            }

                            if (canSpawn)
                            {

                                Thing thing = (Thing)ThingMaker.MakeThing(element.thingDef, null);
                                CellRect occupiedRect = GenAdj.OccupiedRect(c, thing.Rotation, thing.def.Size);
                                if (occupiedRect.InBounds(map))
                                {
                                    GenSpawn.Spawn(thing, c, map);
                                    spawnCounter--;
                                }
                               
                            }
                        if (canSpawn && spawnCounter <= 0)
                        {
                            spawnCounter = 0;
                            break;
                        }
                    }
                    

                }



            }
            


            this.verifyFirstTime = false;

        }

    }
}
