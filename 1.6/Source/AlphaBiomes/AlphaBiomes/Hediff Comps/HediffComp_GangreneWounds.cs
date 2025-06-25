

using RimWorld;
using Verse;
using Verse.Sound;
using System;
using System.Linq;

namespace AlphaBiomes
{
    public class HediffComp_GangreneWounds : HediffComp
    {
        public int checkDownCounter = 0;

        public HediffCompProperties_GangreneWounds Props
        {
            get
            {
                return (HediffCompProperties_GangreneWounds)this.props;
            }
        }



        public override void CompPostTick(ref float severityAdjustment)
        {
            base.CompPostTick(ref severityAdjustment);

            checkDownCounter++;

            if (this.parent.Severity > Props.severityThirdStage)
            {
                if (checkDownCounter > Props.mtbWoundsThirdStage)
                {
                    if (Rand.Value < Props.chanceCutThirdStage)
                    {
                        BodyPartRecord bodypart = this.parent.pawn.health.hediffSet.GetNotMissingParts(BodyPartHeight.Undefined, BodyPartDepth.Undefined, null, null).
                                   FirstOrDefault((BodyPartRecord x) => x.def.defName=="Finger"|| x.def.defName == "Toe");
                        this.parent.pawn.TakeDamage(new DamageInfo(InternalDefOf.AB_Gangrene, 10, 0f, -1f, null, bodypart, null, DamageInfo.SourceCategory.ThingOrUnknown));

                    }
                    checkDownCounter = 0;
                }
            }
            else if (this.parent.Severity > Props.severitySecondStage)
            {
                if (checkDownCounter > Props.mtbWoundsSecondStage)
                {
                    if (Rand.Value < Props.chanceCutSecondStage)
                    {
                        BodyPartRecord bodypart = this.parent.pawn.health.hediffSet.GetNotMissingParts(BodyPartHeight.Undefined, BodyPartDepth.Undefined, null, null).
                                   FirstOrDefault((BodyPartRecord x) => x.def.defName == "Finger" || x.def.defName == "Toe");
                        this.parent.pawn.TakeDamage(new DamageInfo(InternalDefOf.AB_Gangrene, 10, 0f, -1f, null, bodypart, null, DamageInfo.SourceCategory.ThingOrUnknown));

                    }
                    checkDownCounter = 0;
                }
            }







        }
    }
}

