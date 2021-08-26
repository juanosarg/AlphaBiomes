using System;
using RimWorld.Planet;
using RimWorld;

namespace AlphaBiomes
{
    public class BiomeWorker_FeraliskInfestedJungle : BiomeWorker
    {
        public override float GetScore(Tile tile, int tileID)
        {
            if (!AlphaBiomes_Settings.AB_SpawnFeraliskInfestedJungle)
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
            if ((tile.rainfall < 2000f) || (tile.rainfall > 2100f))
            {
                return 0f;
            }
            return (29f + (tile.temperature - 20f) * 1.5f + (tile.rainfall - 600f) / 165f)*AlphaBiomes_Settings.feraliskInfestedJungleMultiplier;
        }
    }
}
