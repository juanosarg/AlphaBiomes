using System;
using System.Collections.Generic;
using System.Linq;
using Verse;
using Verse.AI;
using Verse.AI.Group;
using Verse.Sound;
using RimWorld;

namespace AlphaBiomes
{
    public class CompSpawnerPawnCustom : ThingComp
    {

        public int nextPawnSpawnTick = -1;

        public bool aggressive = true;

        public bool canSpawnPawns = true;



        private CompProperties_SpawnerPawnCustom Props
        {
            get
            {
                return (CompProperties_SpawnerPawnCustom)this.props;
            }
        }



        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);
            if (!respawningAfterLoad && this.nextPawnSpawnTick == -1)
            {
                this.SpawnInitialPawns();
            }
        }

        private void SpawnInitialPawns()
        {
            int num = 0;
            Pawn pawn;
            while (num < this.Props.initialPawnsCount && this.TrySpawnPawn(out pawn))
            {
                num++;
            }

            this.CalculateNextPawnSpawnTick();
        }


        private void CalculateNextPawnSpawnTick()
        {
            this.CalculateNextPawnSpawnTick(this.Props.pawnSpawnIntervalDays.RandomInRange * 60000f);
        }

        public void CalculateNextPawnSpawnTick(float delayTicks)
        {

            this.nextPawnSpawnTick = Find.TickManager.TicksGame + (int)(delayTicks / (1 * Find.Storyteller.difficulty.enemyReproductionRateFactor));
        }

        private bool TrySpawnPawn(out Pawn pawn) { 
      
            if (ModLister.HasActiveModWithName("Alpha Animals")) {
                if (!this.canSpawnPawns)
                {
                    pawn = null;
                    return false;
                }

                int totalCount = 0;

                IEnumerable<string> listUnique = Props.spawnablePawnKinds.Distinct();
                foreach (string pawnkindInList in listUnique)
                {
                    totalCount += this.parent.Map.listerThings.ThingsOfDef(ThingDef.Named(pawnkindInList)).Count;
                }


                if (totalCount < 25)
                {
                    PawnKindDef pawnkind = null;
                    pawnkind = DefDatabase<PawnKindDef>.GetNamed(this.Props.spawnablePawnKinds.RandomElement(), false);
                    if (pawnkind != null)
                    {
                        Pawn pawnToCreate = PawnGenerator.GeneratePawn(pawnkind, Faction.OfInsects);
                        GenSpawn.Spawn(pawnToCreate, CellFinder.RandomClosewalkCellNear(this.parent.Position, this.parent.Map, this.Props.pawnSpawnRadius, null), this.parent.Map, WipeMode.Vanish);

                        if (this.parent.Map != null)
                        {
                            Lord lord = null;
                            if (this.parent.Map.mapPawns.SpawnedPawnsInFaction(Faction.OfInsects).Any((Pawn p) => p != pawnToCreate))
                            {
                                lord = ((Pawn)GenClosest.ClosestThing_Global(this.parent.Position, this.parent.Map.mapPawns.SpawnedPawnsInFaction(Faction.OfInsects), 30f, (Thing p) => p != pawnToCreate && ((Pawn)p).GetLord() != null, null)).GetLord();
                            }
                            if (lord == null)
                            {
                                LordJob_DefendPoint lordJob = new LordJob_DefendPoint(this.parent.Position, 10f,10f, false, true);
                                lord = LordMaker.MakeNewLord(Faction.OfInsects, lordJob, this.parent.Map, null);


                            }
                            lord.AddPawn(pawnToCreate);
                        }



                        pawn = pawnToCreate;
                        if (this.Props.spawnSound != null)
                        {
                            this.Props.spawnSound.PlayOneShot(this.parent);
                        }

                        return true;
                    }
                    else
                    {
                        pawn = null;
                        return false;
                    }


                }
                else
                {
                    pawn = null;
                    return false;
                }



            }
            else
            {
                pawn = null;
                return false;
            }




        }








       

       

        public override void CompTick()
        {
            if (this.parent.Spawned && this.nextPawnSpawnTick == -1)
            {
                this.SpawnInitialPawns();
            }
            if (this.parent.Spawned)
            {
           
                if (Find.TickManager.TicksGame >= this.nextPawnSpawnTick)
                {
                    Pawn pawn;
                    if (this.TrySpawnPawn(out pawn) && pawn.caller != null)
                    {
                        pawn.caller.DoCall();
                    }
                    this.CalculateNextPawnSpawnTick();
                }
            }
        }

       

        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            if (Prefs.DevMode)
            {
                yield return new Command_Action
                {
                    defaultLabel = "DEBUG: Spawn pawn",
                    icon = TexCommand.ReleaseAnimals,
                    action = delegate ()
                    {
                        Pawn pawn;
                        this.TrySpawnPawn(out pawn);
                    }
                };
            }
            yield break;
        }

        public override string CompInspectStringExtra()
        {
            if (ModLister.HasActiveModWithName("Alpha Animals"))
            {
                return (this.Props.nextSpawnInspectStringKey ?? "AB_SpawningNextBumbledrone").Translate((this.nextPawnSpawnTick - Find.TickManager.TicksGame).ToStringTicksToDays("F1"));
            }
            else return "AB_AlphaAnimalsNotInstalled".Translate();
        }

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look<int>(ref this.nextPawnSpawnTick, "nextPawnSpawnTick", 0, false);
          
            Scribe_Values.Look<bool>(ref this.aggressive, "aggressive", false, false);
            Scribe_Values.Look<bool>(ref this.canSpawnPawns, "canSpawnPawns", true, false);
           
           
        }


    }
}
