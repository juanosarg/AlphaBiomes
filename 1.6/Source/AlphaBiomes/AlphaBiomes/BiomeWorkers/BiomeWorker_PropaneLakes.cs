using System;
using RimWorld.Planet;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Noise;

namespace AlphaBiomes
{
    public class BiomeWorker_PropaneLakes : BiomeWorker
    {
        public override float GetScore(BiomeDef biome, Tile tile, PlanetTile planetTile)
        {
            if (AlphaBiomes_Settings.propaneLakesMultiplier==0)
            {
                return -100f;
            }
            if (tile.temperature > -35f)
            {
                return 0f;
            }
            return BiomeWorker_PropaneLakes.PermaIceScore(tile);
        }

        public static float PermaIceScore(Tile tile)
        {
            return (-19f + -tile.temperature * 2f)*AlphaBiomes_Settings.propaneLakesMultiplier;
        }
    }
}