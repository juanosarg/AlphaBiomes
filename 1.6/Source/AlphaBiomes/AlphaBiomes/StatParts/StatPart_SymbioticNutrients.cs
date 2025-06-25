using RimWorld;
using Verse;
namespace AlphaBiomes
{
    public class StatPart_SymbioticNutrients : StatPart
    {
        private float multiplier = 0.9f;

        public override void TransformValue(StatRequest req, ref float val)
        {
            if (req.HasThing && ActiveFor(req.Thing))
            {
                val *= multiplier;
            }
        }

        public override string ExplanationPart(StatRequest req)
        {
            if (req.HasThing && ActiveFor(req.Thing))
            {
                return "StatsReport_MultiplierFor".Translate(InternalDefOf.AB_SymbioticNutrients.label) + (": x" + multiplier.ToStringPercent());
            }
            return null;
        }

        private bool ActiveFor(Thing t)
        {
            Pawn pawn = t as Pawn;
          
            return pawn != null && pawn.Map != null && pawn.Map.TileInfo.Mutators.Contains(InternalDefOf.AB_SymbioticNutrients);
        }
    }
}
