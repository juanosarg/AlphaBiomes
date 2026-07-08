using System.Collections.Generic;
using System.Linq;
using AlphaBiomes;
using RimWorld;
using Verse;

namespace AlphaBiomes
{
    public class TileMutatorWorker_SugarChunks : TileMutatorWorker
    {
        public TileMutatorWorker_SugarChunks(TileMutatorDef def)
            : base(def)
        {
        }

        public override void GeneratePostFog(Map map)
        {

            foreach (IntVec3 cell in map.AllCells)
            {
                Thing thingToDestroy = null;
                List<Thing> thingsHere = cell.GetThingList(map);
                foreach (Thing thing in thingsHere) {
                    if (thing.HasThingCategory(ThingCategoryDefOf.StoneChunks))
                    {
                        thingToDestroy = thing;
                    }                
                }
                if (thingToDestroy != null) { 
                    thingToDestroy.Destroy();
                    GenSpawn.Spawn(InternalDefOf.AB_SugarChunk, cell, map);
                }
            }

        }
    }
}