
using RimWorld;
using RimWorld.Planet;

namespace AlphaBiomes
{
	public class BiomeWorker_MiasmicMangrove : BiomeWorker
	{
		public override float GetScore(BiomeDef biome, Tile tile, PlanetTile planetTile)
		{
			if (AlphaBiomes_Settings.miasmicMangroveMultiplier==0)
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
			if (tile.swampiness < 0.65f)
			{
				return 0f;
			}
			return (30f + (tile.temperature - 20f) * 1.5f + (tile.rainfall - 600f) / 165f + tile.swampiness * 3f)*AlphaBiomes_Settings.miasmicMangroveMultiplier;
		}
	}
}
