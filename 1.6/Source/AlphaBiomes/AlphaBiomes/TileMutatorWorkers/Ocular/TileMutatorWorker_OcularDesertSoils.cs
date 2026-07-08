using System.Collections.Generic;
using System.Linq;
using AlphaBiomes;
using RimWorld;
using Verse;

namespace AlphaBiomes
{
    public class TileMutatorWorker_OcularDesertSoils : TileMutatorWorker
    {
        public TileMutatorWorker_OcularDesertSoils(TileMutatorDef def)
            : base(def)
        {
        }

        public override void GenerateCriticalStructures(Map map)
        {

            foreach (IntVec3 cell in map.AllCells)
            {
                if (cell.GetTerrain(map) == TerrainDefOf.Soil)
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.GU_AlienSand);
                }
                if (cell.GetTerrain(map) == TerrainDefOf.Sand)
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.GU_AlienSandFine);
                }
                if (cell.GetTerrain(map) == TerrainDefOf.SoftSand)
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.GU_AlienSoftSand);
                }
                if (cell.GetTerrain(map) == TerrainDefOf.Gravel)
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.GU_MossyRed);
                }
                if (cell.GetTerrain(map) == TerrainDefOf.SoilRich || cell.GetTerrain(map) == TerrainDefOf.Riverbank)
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.GU_RichAlienSand);
                }
                if (cell.GetTerrain(map) == TerrainDefOf.Mud)
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.GU_AlienSandFine);
                }
                if (cell.GetTerrain(map) == MapGenUtility.ShallowFreshWaterTerrainAt(cell, map))
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.GU_RedWaterShallow);
                }
                if (cell.GetTerrain(map) == MapGenUtility.ShallowMovingWaterTerrainAt(cell, map))
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.GU_RedWaterMovingShallow);
                }
                if (cell.GetTerrain(map) == MapGenUtility.DeepMovingWaterTerrainAt(cell, map))
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.GU_RedWaterMovingChestDeep);
                }
                if (cell.GetTerrain(map) == MapGenUtility.DeepFreshWaterTerrainAt(cell, map))
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.GU_RedWaterDeep);
                }
                if (cell.GetTerrain(map) == MapGenUtility.ShallowOceanWaterTerrainAt(cell, map))
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.GU_RedWaterOceanShallow);
                }
                if (cell.GetTerrain(map) == MapGenUtility.DeepOceanWaterTerrainAt(cell, map))
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.GU_RedWaterOceanDeep);
                }
               
            }

        }
    }
}