
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Noise;
namespace AlphaBiomes
{
    public class TileMutatorWorker_PropaneLakes : TileMutatorWorker_Lake
    {

        protected override float LakeRadius => 0.3f;

        public TileMutatorWorker_PropaneLakes(TileMutatorDef def)
         : base(def)
        {
        }

        protected override void ProcessCell(IntVec3 cell, Map map)
        {
            float val = GetValAt(cell, map);
            if (val > WaterThreshold)
            {
                map.terrainGrid.SetTerrain(cell, InternalDefOf.AB_PropaneLake);
            }
            else if (val > BeachThreshold)
            {
                map.terrainGrid.SetTerrain(cell, InternalDefOf.AB_SolidPropane);
            }
        }


    }
}