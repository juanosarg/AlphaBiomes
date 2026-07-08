using System.Collections.Generic;
using System.Linq;
using AlphaBiomes;
using RimWorld;
using Verse;

namespace AlphaBiomes
{
    public class TileMutatorWorker_SweetForest : TileMutatorWorker
    {
        public TileMutatorWorker_SweetForest(TileMutatorDef def)
            : base(def)
        {
        }

        public override void GenerateCriticalStructures(Map map)
        {

            foreach (IntVec3 cell in map.AllCells)
            {
                if (cell.GetTerrain(map) == TerrainDefOf.Soil)
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.AB_ChocolateSoil);
                }
                if (cell.GetTerrain(map) == TerrainDefOf.Sand)
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.AB_CandySand);
                }
                if (cell.GetTerrain(map) == TerrainDefOf.Gravel)
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.AB_Cream);
                }
                if (cell.GetTerrain(map) == TerrainDefOf.SoilRich || cell.GetTerrain(map) == TerrainDefOf.Riverbank)
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.AB_ChocolateSoilRich);
                }
                if (cell.GetTerrain(map) == TerrainDefOf.Mud)
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.AB_Strawberry);
                }
                if (cell.GetTerrain(map) == MapGenUtility.ShallowFreshWaterTerrainAt(cell, map))
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.AB_LiquidCream_Shallow);
                }
                if (cell.GetTerrain(map) == MapGenUtility.ShallowMovingWaterTerrainAt(cell, map))
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.AB_LiquidCream_MovingShallow);
                }
                if (cell.GetTerrain(map) == MapGenUtility.DeepMovingWaterTerrainAt(cell, map))
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.AB_LiquidCream_MovingChestDeep);
                }
                if (cell.GetTerrain(map) == MapGenUtility.DeepFreshWaterTerrainAt(cell, map))
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.AB_LiquidCream_Deep);
                }
                if (cell.GetTerrain(map) == MapGenUtility.ShallowOceanWaterTerrainAt(cell, map))
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.AB_LiquidCream_Shallow);
                }
                if (cell.GetTerrain(map) == MapGenUtility.DeepOceanWaterTerrainAt(cell, map))
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.AB_LiquidCream_Deep);
                }
                if (cell.GetTerrain(map).defName.Contains("_Rough"))
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.AB_HardenedChocolate);
                }
            }

        }
    }
}