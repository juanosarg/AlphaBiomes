
using RimWorld;
using Verse;
namespace AlphaBiomes
{
    public class TileMutatorWorker_AncientDeathPallVent : TileMutatorWorker_AncientVent
    {
        protected override ThingDef AncientVentDef => ThingDef.Named("AB_AncientDeathPallVent");

        public TileMutatorWorker_AncientDeathPallVent(TileMutatorDef def)
            : base(def)
        {
        }
    }
}
