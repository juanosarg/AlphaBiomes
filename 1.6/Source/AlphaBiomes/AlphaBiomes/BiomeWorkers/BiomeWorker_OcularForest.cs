using System;
using RimWorld;
using RimWorld.Planet;
using UnityEngine;
using Verse;

namespace AlphaBiomes
{
    public class BiomeWorker_OcularForest : BiomeWorker
    {
        public override float GetScore(BiomeDef biome, Tile tile, PlanetTile planetTile)
        {
            if (AlphaBiomes_Settings.ocularForestMultiplier==0)
            {
                return -100f;
            }
            float result;
            if (tile.WaterCovered)
            {
                result = -100f;
            }
            else if (tile.temperature < 15f)
            {
                result = 0f;
            }
            else if (tile.rainfall < 2000f)
            {
                result = 0f;
            }
            else
            {
                Vector3 tileCenter = Find.WorldGrid.GetTileCenter(planetTile);
                if (Find.World.GetComponent<WorldComponentExtender>() == null)
                {
                    WorldComponent item = (WorldComponent)Activator.CreateInstance(typeof(WorldComponentExtender), new object[]
                   {
                        Find.World
                   });
                    Find.World.components.Add(item);
                }

                float tileWeirdness = Find.World.GetComponent<WorldComponentExtender>().noiseWeirdness.GetValue(tileCenter);
                //Log.Message(tileWeirdness.ToString());

                float calculatedInterval = 0;
                if (AlphaBiomes_Settings.ocularForestMultiplier != 1)
                {
                    calculatedInterval = (AlphaBiomes_Settings.ocularForestMultiplier - 0.1f) * (0.3f / 1.9f) - 0.1f;
                }
                


                if (tileWeirdness < (0.15f+ calculatedInterval))
                {
                    result = 100f;
                }
                else result = 0f;



                
            }
            return result;
        }
    }
}