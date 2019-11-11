using System;
using RimWorld;
using RimWorld.Planet;

namespace AlphaBiomes
{
    public class BiomeWorker_MycoticJungle : BiomeWorker
    {
        public override float GetScore(Tile tile, int tileID)
        {
            if (!AlphaBiomes_Settings.AB_SpawnMycoticJungle)
            {
                return -100f;
            }
            if (tile.WaterCovered)
            {
                return -100f;
            }
            if (tile.temperature < 15f)
            {
                return 0f;
            }
            if (tile.rainfall < 2000f)
            {
                return 0f;
            }
            return 28f + (tile.temperature - 20f) * 1f + (tile.rainfall - 600f) / 165f +tile.swampiness/3 ;
        }
    }
}