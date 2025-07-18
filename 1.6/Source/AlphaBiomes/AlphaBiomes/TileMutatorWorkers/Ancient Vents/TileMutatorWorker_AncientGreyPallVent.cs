
using RimWorld;
using Verse;
namespace AlphaBiomes
{
    public class TileMutatorWorker_AncientGreyPallVent : TileMutatorWorker_AncientVent
    {
        protected override ThingDef AncientVentDef => ThingDef.Named("AB_AncientGreyPallVent");

        public TileMutatorWorker_AncientGreyPallVent(TileMutatorDef def)
            : base(def)
        {
        }
    }
}
