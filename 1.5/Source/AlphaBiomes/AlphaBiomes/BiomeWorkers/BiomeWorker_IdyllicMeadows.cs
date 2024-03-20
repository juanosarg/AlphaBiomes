using System;
using RimWorld.Planet;
using RimWorld;
using UnityEngine;
using Verse;

namespace AlphaBiomes
{
    public class BiomeWorker_IdyllicMeadows : BiomeWorker
    {
        public override float GetScore(Tile tile, int tileID)
        {
            if (!AlphaBiomes_Settings.AB_SpawnIdyllicMeadows)
            {
                return -100f;
            }
            if (tile.WaterCovered)
            {
                return -100f;
            }
            if (tile.hilliness != Hilliness.Flat)
            {
                return -100f;
            }
            if (tile.temperature < -10f)
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

            float tileIdyllicity = Find.World.GetComponent<WorldComponentExtender>().noiseIdyllicity.GetValue(tileCenter);


            float calculatedInterval = 0;
            if (AlphaBiomes_Settings.idyllicMeadowsMultiplier != 1)
            {
                calculatedInterval = (AlphaBiomes_Settings.idyllicMeadowsMultiplier - 0.1f) * (0.9f / 1.9f) - 0.2f;
            }


            if (tileIdyllicity > (0.9f - calculatedInterval))
            {
                return 105f + (tile.temperature - 7f) + (tile.rainfall - 600f) / 180f;

            }
            else return 0f;
        }
    }
}
