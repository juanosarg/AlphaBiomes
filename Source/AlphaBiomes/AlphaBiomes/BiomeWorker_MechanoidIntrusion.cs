using System;
using RimWorld.Planet;
using RimWorld;

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
            if ((tile.temperature < 20.5f)|| (tile.temperature > 21f))
            {
                return 0f;
            }
            if (tile.rainfall < 600f)
            {
                return 0f;
            }
            return 16f + (tile.temperature - 7f) + (tile.rainfall - 600f) / 180f;
        }
    }
}
