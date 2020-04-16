using RimWorld;
using Verse;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaBiomes
{
    public class PlaceWorker_PropanePump : PlaceWorker
    {
        public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 loc, Rot4 rot, Map map, Thing thingToIgnore = null, Thing thing = null)
        {
            foreach (IntVec3 c in GenAdj.CellsOccupiedBy(loc,rot,checkingDef.Size))
            {
                if (map.terrainGrid.TerrainAt(c).defName!= "AB_SolidPropane")
                {
                    return new AcceptanceReport("AB_TerrainCannotSupport_Propane".Translate());
                }
            }
           
            return true;
        }
    }
}
