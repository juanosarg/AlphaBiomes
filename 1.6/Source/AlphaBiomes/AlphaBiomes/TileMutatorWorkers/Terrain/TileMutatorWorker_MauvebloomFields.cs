using System.Collections.Generic;
using System.Linq;
using AlphaBiomes;
using RimWorld;
using Verse;

namespace AlphaBiomes
{
    public class TileMutatorWorker_MauvebloomFields : TileMutatorWorker
    {
        public TileMutatorWorker_MauvebloomFields(TileMutatorDef def)
            : base(def)
        {
        }

        public override void GenerateCriticalStructures(Map map)
        {

            foreach (IntVec3 cell in map.AllCells)
            {
                if (cell.GetTerrain(map) == TerrainDefOf.Soil)
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.AB_CeruleanSoil);
                }
               
                if (cell.GetTerrain(map) == TerrainDefOf.SoilRich || cell.GetTerrain(map) == TerrainDefOf.Riverbank)
                {
                    map.terrainGrid.SetTerrain(cell, InternalDefOf.AB_CeruleanSoilRich);
                }

            }

        }
    }
}