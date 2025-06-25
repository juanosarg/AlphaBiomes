
using RimWorld;
using Verse;
namespace AlphaBiomes
{
    public class TileMutatorWorker_AncientFreezingVent : TileMutatorWorker_AncientVent
    {
        protected override ThingDef AncientVentDef => InternalDefOf.AB_AncientFreezingVent;

        public TileMutatorWorker_AncientFreezingVent(TileMutatorDef def)
            : base(def)
        {
        }
    }
}
