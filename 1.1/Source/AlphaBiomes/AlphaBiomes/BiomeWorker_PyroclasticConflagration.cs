using System;
using RimWorld.Planet;
using RimWorld;
using UnityEngine;
using Verse;

namespace AlphaBiomes
{
    public class BiomeWorker_PyroclasticConflagration : BiomeWorker
    {
        public override float GetScore(Tile tile, int tileID)
        {
            //return 0f;
            
            if (!AlphaBiomes_Settings.AB_SpawnPyroclasticConflagration)
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
           
           
            Vector3 tileCenter = Find.WorldGrid.GetTileCenter(tileID);
            if (Find.World.GetComponent<WorldComponentExtender>() == null)
            {
                     WorldComponent item = (WorldComponent)Activator.CreateInstance(typeof(WorldComponentExtender), new object[]
                    {
                        Find.World
                    });
                    Find.World.components.Add(item);
             }
            
            float tileVolcanic = Find.World.GetComponent<WorldComponentExtender>().noiseVolcanic.GetValue(tileCenter);
            //Log.Message(tileWeirdness.ToString());
            if (tileVolcanic > 0.75f)
            {
                return 100f;
            }
            else return 0f;
            //return 15f + (tile.temperature - 7f) + (tile.rainfall - 600f) / 180f + tile.swampiness * 10f;
            
        }
    }
}
