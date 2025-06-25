
using RimWorld;
using System.Linq;
using Verse;
namespace AlphaBiomes
{
    public class CompAncientDeathPallVent : CompAncientVent
    {
        protected override bool AppliesEffectsToPawns => false;

        protected override void ToggleIndividualVent(bool on)
        {
            parent.GetComp<CompFleckEmitterLongTerm>().Enabled = on;
        }


    }
}
