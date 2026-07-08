
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using Verse.Noise;
namespace AlphaBiomes
{
    public class TileMutatorWorker_MagmaticQuagmire : TileMutatorWorker
    {
        private const float RidgedFreq = 0.03f;

        private const float RidgedLac = 2f;

        private const int RidgedOctaves = 2;

        private const float MudThreshold = 0.015f;

        private const float WaterThreshold = 0.35f;

        private ModuleBase quagmireNoise;

        public TileMutatorWorker_MagmaticQuagmire(TileMutatorDef def)
            : base(def)
        {
        }

        public override void Init(Map map)
        {
            quagmireNoise = new RidgedMultifractal(RidgedFreq, RidgedLac, RidgedOctaves, Rand.Int, QualityMode.High);
            quagmireNoise = new Clamp(0.0, 1.0, quagmireNoise);
            NoiseDebugUI.StoreNoiseRender(quagmireNoise, "magmaticquagmire");
        }

        public override void GeneratePostTerrain(Map map)
        {
            foreach (IntVec3 cell in map.AllCells)
            {
                if (cell.GetEdifice(map) == null)
                {
                    TerrainDef existingTerrain = cell.GetTerrain(map);
                    if (!existingTerrain.IsWater)
                    {
                        float val = quagmireNoise.GetValue(cell);
                        if (val > WaterThreshold)
                        {
                            if (existingTerrain.IsRock)
                            {
                                map.terrainGrid.SetTerrain(cell, InternalDefOf.AB_SolidifiedLava);
                            }
                            else
                            {
                                map.terrainGrid.SetTerrain(cell, TerrainDefOf.LavaDeep);
                            }
                        }
                        else if (val > MudThreshold && MapGenUtility.ShouldGenerateBeachSand(cell, map))
                        {
                            map.terrainGrid.SetTerrain(cell, InternalDefOf.AB_SolidifiedLava);
                        }
                    }
                }
            }
        }
    }
}