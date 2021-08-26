using System;
using RimWorld.Planet;
using RimWorld;
using UnityEngine;
using Verse;

namespace AlphaBiomes
{
    public class BiomeWorker_MechanoidIntrusion : BiomeWorker
    {
        public override float GetScore(Tile tile, int tileID)
        {
            if (!AlphaBiomes_Settings.AB_SpawnMechanoidIntrusion)
            {
                return -100f;
            }
            if (tile.WaterCovered)
            {
                return -100f;
            }
            if ((tile.temperature < 17f)|| (tile.temperature > 25f))
            {
                return 0f;
            }
            if (tile.rainfall < 600f)
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

            float tileRadiation = Find.World.GetComponent<WorldComponentExtender>().noiseRadiation.GetValue(tileCenter);
            // Log.Message(tileRadiation.ToString());

            
            float calculatedInterval = 0;
            if (AlphaBiomes_Settings.mechanoidIntrusionMultiplier != 1)
            {
                calculatedInterval = (AlphaBiomes_Settings.mechanoidIntrusionMultiplier - 0.1f) * (0.9f / 1.9f) - 0.2f;
            }

            if (tileRadiation > (0.75f-calculatedInterval))
            {
                return 101f;
            }
            else return 0f;
            //return 16f + (tile.temperature - 7f) + (tile.rainfall - 600f) / 180f;
        }
    }
}
