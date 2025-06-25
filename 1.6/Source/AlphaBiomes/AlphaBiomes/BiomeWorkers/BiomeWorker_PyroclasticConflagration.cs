using System;
using RimWorld.Planet;
using RimWorld;
using UnityEngine;
using Verse;

namespace AlphaBiomes
{
    public class BiomeWorker_PyroclasticConflagration : BiomeWorker
    {
        public override float GetScore(BiomeDef biome, Tile tile, PlanetTile planetTile)
        {
           // return 0f;
            
            if (AlphaBiomes_Settings.pyroclasticConflagrationMultiplier==0)
            {
                return -100f;
            }
            if (tile.WaterCovered)
            {
                return -100f;
            }
            if (tile.temperature < 10f)
            {
                return 0f;
            }
           
           
            Vector3 tileCenter = Find.WorldGrid.GetTileCenter(planetTile);
            if (Find.World.GetComponent<WorldComponentExtender>() == null)
            {
                     WorldComponent item = (WorldComponent)Activator.CreateInstance(typeof(WorldComponentExtender), new object[]
                    {
                        Find.World
                    });
                    Find.World.components.Add(item);
             }
            
            float tileVolcanic = Find.World.GetComponent<WorldComponentExtender>().noiseVolcanic.GetValue(tileCenter);
           
            float calculatedInterval = 0;
            if (AlphaBiomes_Settings.pyroclasticConflagrationMultiplier != 1)
            {
                calculatedInterval = (AlphaBiomes_Settings.pyroclasticConflagrationMultiplier - 0.1f) * (0.9f / 1.9f) - 0.2f;
            }
            //Log.Message(tileWeirdness.ToString());
            if (tileVolcanic > (0.75f - calculatedInterval))
            {
                return 100f;
            }
            else return 0f;
            //return 15f + (tile.temperature - 7f) + (tile.rainfall - 600f) / 180f + tile.swampiness * 10f;
            
        }
    }
}
