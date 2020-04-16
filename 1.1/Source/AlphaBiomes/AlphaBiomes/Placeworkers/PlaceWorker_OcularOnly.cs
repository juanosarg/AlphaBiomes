using RimWorld;
using Verse;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaBiomes
{
    public class PlaceWorker_OcularOnly : PlaceWorker
    {
        public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 loc, Rot4 rot, Map map, Thing thingToIgnore = null, Thing thing = null)
        {
            
                if (map.Biome.defName != "AB_OcularForest")
                {
                    return new AcceptanceReport("AB_OcularForestOnly".Translate());
                }
           
           
            return true;
        }
    }
}
