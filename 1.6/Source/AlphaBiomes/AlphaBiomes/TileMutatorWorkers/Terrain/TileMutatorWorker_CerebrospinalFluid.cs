using System.Collections.Generic;
using System.Linq;
using AlphaBiomes;
using RimWorld;
using Verse;

namespace AlphaBiomes
{
    public class TileMutatorWorker_CerebrospinalFluid : TileMutatorWorker
    {
        public TileMutatorWorker_CerebrospinalFluid(TileMutatorDef def)
            : base(def)
        {
        }

        public override void GenerateCriticalStructures(Map map)
        {

            foreach (IntVec3 cell in map.AllCells)
            {
                if (cell.GetTerrain(map) == MapGenUtility.ShallowFreshWaterTerrainAt(cell, map))
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.AB_CerebrospinalFluid_Shallow);
                }
                if (cell.GetTerrain(map) == MapGenUtility.ShallowMovingWaterTerrainAt(cell, map))
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.AB_CerebrospinalFluid_MovingShallow);
                }
                if (cell.GetTerrain(map) == MapGenUtility.DeepMovingWaterTerrainAt(cell, map))
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.AB_CerebrospinalFluid_MovingChestDeep);
                }
                if (cell.GetTerrain(map) == MapGenUtility.DeepFreshWaterTerrainAt(cell, map))
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.AB_CerebrospinalFluid_Deep);
                }
               

            }

        }
    }
}