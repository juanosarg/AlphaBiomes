using System.Collections.Generic;
using System.Linq;
using AlphaBiomes;
using RimWorld;
using Verse;

namespace AlphaBiomes
{
    public class TileMutatorWorker_ReddishWaters : TileMutatorWorker
    {
        public TileMutatorWorker_ReddishWaters(TileMutatorDef def)
            : base(def)
        {
        }

        public override void GenerateCriticalStructures(Map map)
        {

            foreach (IntVec3 cell in map.AllCells)
            {
                if (cell.GetTerrain(map) == MapGenUtility.ShallowFreshWaterTerrainAt(cell, map))
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.AB_LightRedWaterShallow);
                }
                if (cell.GetTerrain(map) == MapGenUtility.ShallowMovingWaterTerrainAt(cell, map))
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.AB_LightRedWaterMovingShallow);
                }
                if (cell.GetTerrain(map) == MapGenUtility.DeepMovingWaterTerrainAt(cell, map))
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.AB_LightRedWaterMovingChestDeep);
                }
                if (cell.GetTerrain(map) == MapGenUtility.DeepFreshWaterTerrainAt(cell, map))
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.AB_LightRedWaterDeep);
                }
                if (cell.GetTerrain(map) == MapGenUtility.ShallowOceanWaterTerrainAt(cell, map))
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.AB_LightRedWaterOceanShallow);
                }
                if (cell.GetTerrain(map) == MapGenUtility.DeepOceanWaterTerrainAt(cell, map))
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.AB_LightRedWaterOceanDeep);
                }

            }

        }
    }
}