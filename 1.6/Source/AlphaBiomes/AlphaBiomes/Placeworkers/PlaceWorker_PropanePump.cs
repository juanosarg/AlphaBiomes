using RimWorld;
using Verse;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine.Analytics;

namespace AlphaBiomes
{
    public class PlaceWorker_PropanePump : PlaceWorker
    {
        public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 loc, Rot4 rot, Map map, Thing thingToIgnore = null, Thing thing = null)
        {
            foreach (IntVec3 c in GenAdj.CellsOccupiedBy(loc,rot,checkingDef.Size))
            {
                if (map.terrainGrid.TerrainAt(c)!= InternalDefOf.AB_SolidPropane)
                {
                    return new AcceptanceReport("AB_TerrainCannotSupport_Propane".Translate());
                }
            }

            foreach (IntVec3 c in GenAdj.OccupiedRect(loc, rot, checkingDef.Size).ExpandedBy(12))
            {
                List<Thing> list = map.thingGrid.ThingsListAt(c);
                for (int i = 0; i < list.Count; i++)
                {
                    Thing thing2 = list[i];
                    if (thing2 != thingToIgnore && ((thing2.def.category == ThingCategory.Building && thing2.def == InternalDefOf.AB_PropanePump) || ((thing2.def.IsBlueprint || thing2.def.IsFrame) && thing2.def.entityDefToBuild is ThingDef && ((ThingDef)thing2.def.entityDefToBuild) == InternalDefOf.AB_PropanePump)))
                    {
                        return "AB_Distance_Propane".Translate();
                    }
                }
            }


            return true;
        }
    }
}
