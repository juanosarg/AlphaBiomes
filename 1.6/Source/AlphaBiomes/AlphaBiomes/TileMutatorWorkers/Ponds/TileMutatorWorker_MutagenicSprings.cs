using RimWorld;
using Verse;
using Verse.Noise;
namespace AlphaBiomes
{
    public class TileMutatorWorker_MutagenicSprings : TileMutatorWorker
    {


        private ModuleBase springNoise;

        public TileMutatorWorker_MutagenicSprings(TileMutatorDef def)
            : base(def)
        {
        }

        public override void Init(Map map)
        {
            if (ModsConfig.OdysseyActive)
            {
                springNoise = new Perlin(0.05000000074505806, 1.5, 0.5, 4, Rand.Int, QualityMode.Medium);
                springNoise = new ScaleBias(0.5, 0.5, springNoise);
                ModuleBase baseShape = MapNoiseUtility.CreateFalloffRadius((float)map.Size.x * 0.35f, map.Center.ToVector2(), 0.05f);
                baseShape = MapNoiseUtility.AddDisplacementNoise(baseShape, 0.015f, 25f, 1);
                NoiseDebugUI.StoreNoiseRender(baseShape, "mutagenic springs area");
                springNoise = new Multiply(springNoise, baseShape);
                NoiseDebugUI.StoreNoiseRender(springNoise, "mutagenic springs");
            }
        }

        public override void GeneratePostElevationFertility(Map map)
        {
            MapGenFloatGrid elevation = MapGenerator.Elevation;
            foreach (IntVec3 allCell in map.AllCells)
            {
                if (springNoise.GetValue(allCell) > 0.65f)
                {
                    elevation[allCell] = 0f;
                }
            }
        }

        public override void GeneratePostTerrain(Map map)
        {
            foreach (IntVec3 allCell in map.AllCells)
            {
                float value = springNoise.GetValue(allCell);
                if (value > 0.85f)
                {
                    map.terrainGrid.SetTerrain(allCell, InternalDefOf.AB_MutagenicOcularWater);
                }
                else if (value > 0.65f)
                {
                    map.terrainGrid.SetTerrain(allCell, GenStep_RocksFromGrid.RockDefAt(allCell).building.naturalTerrain);
                }
            }
        }
    }
}