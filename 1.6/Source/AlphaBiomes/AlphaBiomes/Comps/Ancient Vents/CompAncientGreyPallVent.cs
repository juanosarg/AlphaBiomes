
using RimWorld;
using System.Linq;
using Verse;
namespace AlphaBiomes
{
    public class CompAncientGreyPallVent : CompAncientVent
    {
        protected override bool AppliesEffectsToPawns => false;

        protected override void ToggleIndividualVent(bool on)
        {
            parent.GetComp<CompFleckEmitterLongTerm>().Enabled = on;
        }

     
    }
}
