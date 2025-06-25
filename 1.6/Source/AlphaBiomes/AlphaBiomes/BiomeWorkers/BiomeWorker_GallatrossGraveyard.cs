using System;
using RimWorld.Planet;
using RimWorld;

namespace AlphaBiomes
{
    public class BiomeWorker_GallatrossGraveyard : BiomeWorker
    {
        public override float GetScore(BiomeDef biome, Tile tile, PlanetTile planetTile)
        {
            if (AlphaBiomes_Settings.gallatrossGraveyardMultiplier==0)
            {
                return -100f;
            }
            if (tile.WaterCovered)
            {
                return -100f;
            }
            if (tile.temperature < -10f)
            {
                return 0f;
            }
            if (tile.rainfall < 700f || tile.rainfall >= 2200f)
            {
                return 0f;
            }
            return (22.5f + (tile.temperature - 20f) * 2.2f + (tile.rainfall - 600f) / 100f)* AlphaBiomes_Settings.gallatrossGraveyardMultiplier;
        }
    }
}
