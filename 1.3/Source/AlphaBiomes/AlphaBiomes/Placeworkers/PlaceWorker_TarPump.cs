using RimWorld;
using Verse;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaBiomes
{
    public class PlaceWorker_TarPump : PlaceWorker
    {
        public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 loc, Rot4 rot, Map map, Thing thingToIgnore = null, Thing thing = null)
        {
            bool canPlaceTerrain = true;
            bool canPlaceHole = true;

            foreach (IntVec3 c in GenAdj.CellsOccupiedBy(loc, rot, checkingDef.Size))
            {
                if (map.terrainGrid.TerrainAt(c) != InternalDefOf.AB_TarMud)
                {
                    canPlaceTerrain = false;
                }
            }


            Thing thing2 = map.thingGrid.ThingAt(loc, InternalDefOf.AB_TarHole);
            if (thing2 == null || thing2.Position != loc)
            {
                canPlaceHole = false;
            }
            return canPlaceTerrain || canPlaceHole;
        }

        public override bool ForceAllowPlaceOver(BuildableDef otherDef)
        {
            return otherDef == InternalDefOf.AB_TarHole;
        }
    }
}
