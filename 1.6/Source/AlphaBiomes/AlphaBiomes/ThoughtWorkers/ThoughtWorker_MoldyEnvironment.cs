
using RimWorld;
using Verse;

namespace AlphaBiomes
{
    public class ThoughtWorker_MoldyEnvironment : ThoughtWorker_GameCondition
    {
        protected override ThoughtState CurrentStateInternal(Pawn p)
        {
            if (base.CurrentStateInternal(p).Active && p.Map!=null) {
              
                return p.Position.UsesOutdoorTemperature(p.Map);
            }
           
            return false;
        }
    }
}