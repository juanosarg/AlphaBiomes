
using RimWorld;
using System.Linq;
using Verse;
namespace AlphaBiomes
{
    public class CompAncientFreezingVent : CompAncientVent
    {
        protected override bool AppliesEffectsToPawns => true;

        protected override void ToggleIndividualVent(bool on)
        {
            parent.GetComp<CompFleckEmitterLongTerm>().Enabled = on;
        }

        protected override void ApplyPawnEffect(Pawn pawn, int delta)
        {
            if (this.parent.IsHashIntervalTick(200))
            {
                pawn.RaceProps.body.AllPartsVulnerableToFrostbite.Where((BodyPartRecord x) => !pawn.health.hediffSet.PartIsMissing(x)).TryRandomElementByWeight((BodyPartRecord x) => x.def.frostbiteVulnerability, out var part);
                DamageInfo dam = new DamageInfo(DamageDefOf.Frostbite, 10, 0f, -1f, null, part);
                pawn.TakeDamage(dam);
            }
        }
    }
}
