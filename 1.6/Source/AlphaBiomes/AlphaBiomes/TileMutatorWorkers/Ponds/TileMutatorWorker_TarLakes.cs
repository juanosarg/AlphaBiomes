
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Noise;
namespace AlphaBiomes
{
    public class TileMutatorWorker_TarLakes : TileMutatorWorker_Lake
    {
        public TileMutatorWorker_TarLakes(TileMutatorDef def)
         : base(def)
        {
        }

        protected override void ProcessCell(IntVec3 cell, Map map)
        {
            float val = GetValAt(cell, map);
            if (val > WaterThreshold)
            {
                map.terrainGrid.SetTerrain(cell, InternalDefOf.AB_Tar);
            }
            else if (val > BeachThreshold && MapGenUtility.ShouldGenerateBeachSand(cell, map))
            {
                map.terrainGrid.SetTerrain(cell, InternalDefOf.AB_TarMud);
            }
        }

       
    }
}