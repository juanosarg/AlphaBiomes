
using RimWorld;
using Verse;
namespace AlphaBiomes
{
    public class TileMutatorWorker_AncientBloodRainVent : TileMutatorWorker_AncientVent
    {
        protected override ThingDef AncientVentDef => ThingDef.Named("AB_AncientBloodRainVent");

        public TileMutatorWorker_AncientBloodRainVent(TileMutatorDef def)
            : base(def)
        {
        }
    }
}
