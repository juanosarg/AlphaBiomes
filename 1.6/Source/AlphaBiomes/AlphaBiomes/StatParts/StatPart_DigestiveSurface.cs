using RimWorld;
using Verse;
namespace AlphaBiomes
{
    public class StatPart_DigestiveSurface : StatPart
    {
        private float multiplier = 1f;

        public override void TransformValue(StatRequest req, ref float val)
        {
            if (ActiveFor(req.Thing))
            {
                val *= multiplier;
            }
        }

        public override string ExplanationPart(StatRequest req)
        {
            if (req.HasThing && ActiveFor(req.Thing))
            {
                return "StatsReport_MultiplierFor".Translate(InternalDefOf.AB_DigestiveSurface.label) + (": x" + multiplier.ToStringPercent());
            }
            return null;
        }

        private bool ActiveFor(Thing t)
        {
            return t != null && t.def.deteriorateFromEnvironmentalEffects && t.Map!=null&&t.MapHeld != null && t.Map.TileInfo.Mutators.Contains(InternalDefOf.AB_DigestiveSurface)&& t.Position.GetTerrain(t.Map).natural;
        }
    }
}
