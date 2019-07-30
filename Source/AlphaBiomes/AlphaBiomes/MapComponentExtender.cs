using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
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
            }
            
        }

        public void doMapSpawns()
        {

            IEnumerable<IntVec3> tmpTerrain = map.AllCells.InRandomOrder(); //random so we can spawn plants and stuff in this step.

            foreach (IntVec3 c in tmpTerrain)
            {
                bool canSpawn = true;
                TerrainDef terrain = c.GetTerrain(map);
                foreach (SpecialSpawnsDef element in DefDatabase<SpecialSpawnsDef>.AllDefs)
                {
                    if (spawnCounter == 0)
                    {
                        spawnCounter = element.numberToSpawn;
                    }
        
                    foreach (string biome in element.forbiddenBiomes)
                    {
                        if (map.Biome.defName == biome)
                        {
                            canSpawn = false;
                            break;
                        }
                    }

                    foreach (string biome in element.allowedBiomes)
                    {
                        if (map.Biome.defName != biome)
                        {
                            canSpawn = false;
                            break;
                        }
                    }
                    if (!canSpawn)
                    {
                        continue;
                    }
                
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
                        GenSpawn.Spawn(thing, c, map);
                        spawnCounter--;
                    }  

                }
                if (canSpawn && spawnCounter<=0)
                {
                    break;
                }

            }

           
            this.verifyFirstTime = false;

        }

    }
}
