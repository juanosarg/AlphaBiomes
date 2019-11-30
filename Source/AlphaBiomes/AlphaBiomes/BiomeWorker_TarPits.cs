using System;
using RimWorld.Planet;
using RimWorld;
using UnityEngine;
using Verse;

namespace AlphaBiomes
{
    public class BiomeWorker_TarPits : BiomeWorker
    {
        public override float GetScore(Tile tile, int tileID)
        {
            //return 0f;
           
            if (!AlphaBiomes_Settings.AB_SpawnTarPits)
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
            if (tile.temperature < 0f)
            {
                return 0f;
            }
            if (tile.rainfall < 100f || tile.rainfall >= 600f)
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

            float tileWeirdness = Find.World.GetComponent<WorldComponentExtender>().noiseWeirdness.GetValue(tileCenter);
            //Log.Message(tileWeirdness.ToString());
            if (tileWeirdness > 0.7f)
            {
                return 100f;
            }
            else return 0f;
        }
    }
}
