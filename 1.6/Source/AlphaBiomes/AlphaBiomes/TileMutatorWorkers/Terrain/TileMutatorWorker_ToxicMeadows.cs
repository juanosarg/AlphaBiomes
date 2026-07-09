using System.Collections.Generic;
using System.Linq;
using AlphaBiomes;
using RimWorld;
using Verse;

namespace AlphaBiomes
{
    public class TileMutatorWorker_ToxicMeadows : TileMutatorWorker
    {
        public TileMutatorWorker_ToxicMeadows(TileMutatorDef def)
            : base(def)
        {
        }

        public override void GenerateCriticalStructures(Map map)
        {

            foreach (IntVec3 cell in map.AllCells)
            {
                if (cell.GetTerrain(map) == InternalDefOf.AB_GrassyFlowerySoil)
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.AB_ToxicFlowerySoil);
                }
                if (cell.GetTerrain(map) == InternalDefOf.AB_FertileGrassyFlowerySoil)
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.AB_ToxicFertileGrassyFlowerySoil);
                }
                if (cell.GetTerrain(map) == TerrainDefOf.SoilRich || cell.GetTerrain(map) == TerrainDefOf.Riverbank)
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.AB_ToxicFertileGrassyFlowerySoil);
                }

            }

        }
    }
}