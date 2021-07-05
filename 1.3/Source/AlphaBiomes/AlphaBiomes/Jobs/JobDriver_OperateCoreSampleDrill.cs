using System;
using System.Collections.Generic;
using Verse;
using Verse.AI;
using RimWorld;

namespace AlphaBiomes
{
    public class JobDriver_OperateCoreSampleDrill : JobDriver
    {
        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return this.pawn.Reserve(this.job.targetA, this.job, 1, -1, null, errorOnFailed);
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOnDespawnedNullOrForbidden(TargetIndex.A);
            this.FailOnBurningImmobile(TargetIndex.A);
            this.FailOnThingHavingDesignation(TargetIndex.A, DesignationDefOf.Uninstall);
          
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.InteractionCell);
            Toil work = new Toil();
            work.tickAction = delegate ()
            {
                Pawn actor = work.actor;
                ((Building)actor.CurJob.targetA.Thing).GetComp<CompCoreSampleDrill>().DrillWorkDone(actor);
                actor.skills.Learn(SkillDefOf.Mining, 0.065f, false);
            };
            work.defaultCompleteMode = ToilCompleteMode.Never;
            work.WithEffect(EffecterDefOf.Drill, TargetIndex.A);
            work.FailOnCannotTouch(TargetIndex.A, PathEndMode.InteractionCell);
            work.activeSkill = (() => SkillDefOf.Mining);
            yield return work;
            yield break;
        }
    }
}
