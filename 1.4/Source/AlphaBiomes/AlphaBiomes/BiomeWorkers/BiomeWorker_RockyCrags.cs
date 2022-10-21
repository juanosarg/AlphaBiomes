using System;
using RimWorld.Planet;
using RimWorld;

namespace AlphaBiomes
{
    public class BiomeWorker_RockyCrags : BiomeWorker
    {
        public override float GetScore(Tile tile, int tileID)
        {
            if (!AlphaBiomes_Settings.AB_SpawnRockyCrags)
            {
                return -100f;
            }
            if (tile.WaterCovered)
            {
                return -100f;
            }
            if ((tile.rainfall <= 340f) || (tile.rainfall >= 480f))
            {
                return 0f;
            }
            return (tile.temperature * 2.7f - 13f - tile.rainfall * 0.14f + 20f)*AlphaBiomes_Settings.rockyCragsMultiplier;
        }
    }
}