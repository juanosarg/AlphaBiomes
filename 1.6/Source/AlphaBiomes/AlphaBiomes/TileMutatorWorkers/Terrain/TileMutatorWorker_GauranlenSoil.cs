using System.Collections.Generic;
using System.Linq;
using AlphaBiomes;
using RimWorld;
using Verse;

namespace AlphaBiomes
{
    public class TileMutatorWorker_GauranlenSoil : TileMutatorWorker
    {
        public TileMutatorWorker_GauranlenSoil(TileMutatorDef def)
            : base(def)
        {
        }

        public override void GenerateCriticalStructures(Map map)
        {

            foreach (IntVec3 cell in map.AllCells)
            {
                if (cell.GetTerrain(map) == TerrainDefOf.Soil)
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.AB_GauranlenSoil);
                }

                if (cell.GetTerrain(map) == TerrainDefOf.SoilRich || cell.GetTerrain(map) == TerrainDefOf.Riverbank)
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.AB_GauranlenSoilRich);
                }

            }

        }
    }
}