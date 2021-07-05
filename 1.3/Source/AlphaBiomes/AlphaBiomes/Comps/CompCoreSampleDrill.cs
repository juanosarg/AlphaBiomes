using System;
using UnityEngine;
using Verse;
using System.Collections.Generic;
using RimWorld;

namespace AlphaBiomes
{
    public class CompCoreSampleDrill : ThingComp
    {


        private CompPowerTrader powerComp;

        private float portionProgress;

        private float portionYieldPct;

        private int lastUsedTick = -99999;

        private const float WorkPerPortionBase = 6000f;

        public float ProgressToNextPortionPercent
        {
            get
            {
                return this.portionProgress / WorkPerPortionBase;
            }
        }

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            this.powerComp = this.parent.TryGetComp<CompPowerTrader>();
        }

        public override void PostExposeData()
        {
            Scribe_Values.Look<float>(ref this.portionProgress, "portionProgress", 0f, false);
            Scribe_Values.Look<float>(ref this.portionYieldPct, "portionYieldPct", 0f, false);
            Scribe_Values.Look<int>(ref this.lastUsedTick, "lastUsedTick", 0, false);
        }

        public void DrillWorkDone(Pawn driller)
        {
            float statValue = driller.GetStatValue(StatDefOf.MiningSpeed, true);
            this.portionProgress += statValue;
            this.portionYieldPct += statValue * driller.GetStatValue(StatDefOf.MiningYield, true) / WorkPerPortionBase;
            this.lastUsedTick = Find.TickManager.TicksGame;
            if (this.portionProgress > WorkPerPortionBase)
            {
                this.TryProducePortion(this.portionYieldPct);
                this.portionProgress = 0f;
                this.portionYieldPct = 0f;
            }
        }

        public override void PostDeSpawn(Map map)
        {
            this.portionProgress = 0f;
            this.portionYieldPct = 0f;
            this.lastUsedTick = -99999;
        }

        private void TryProducePortion(float yieldPct)
        {
            string[] vanillaRockTypes = { "ChunkMarble", "ChunkSandstone", "ChunkLimestone", "ChunkGranite", "ChunkSlate" };

            Thing thing = null;
            Building_CoreSampleDrill building = this.parent as Building_CoreSampleDrill;
            if (building.RockTypeToMine == "Random") {
                thing = ThingMaker.MakeThing(ThingDef.Named(vanillaRockTypes.RandomElement()), null);
            } else {
                thing = ThingMaker.MakeThing(ThingDef.Named(building.RockTypeToMine), null);
            }
            
           
            GenPlace.TryPlaceThing(thing, this.parent.InteractionCell, this.parent.Map, ThingPlaceMode.Near, null, null, default(Rot4));
           
        }

       
        public bool CanDrillNow()
        {
            return (this.powerComp == null || this.powerComp.PowerOn);
        }

      

        public bool UsedLastTick()
        {
            return this.lastUsedTick >= Find.TickManager.TicksGame - 1;
        }

        public override string CompInspectStringExtra()
        {
            if (!this.parent.Spawned)
            {
                return null;
            }
            Building_CoreSampleDrill building = this.parent as Building_CoreSampleDrill;
            if (building.RockTypeToMine == "Random")
            {
                return "AB_DrillingStoneTypeRandom".Translate() +"\n" + "AB_ProgressToNextPortion".Translate() + ": " + this.ProgressToNextPortionPercent.ToStringPercent("F0");
            }
            else
            {
                return "AB_DrillingStoneType".Translate() + ThingDef.Named(building.RockTypeToMine).LabelCap + "\n" + "AB_ProgressToNextPortion".Translate() + ": " + this.ProgressToNextPortionPercent.ToStringPercent("F0");
            }

        }

       
        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            foreach (Gizmo g in base.CompGetGizmosExtra())
            {
                yield return g;
            }
            Building_CoreSampleDrill building = this.parent as Building_CoreSampleDrill;
            yield return StoneTypeSettableUtility.SetStoneToMineCommand(building);
            


        }



    }
}
