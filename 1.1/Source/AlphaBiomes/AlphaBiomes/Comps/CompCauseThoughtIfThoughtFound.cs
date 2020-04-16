using RimWorld;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Verse;
using Verse.Sound;
using UnityEngine;
using System.Collections;

namespace AlphaBiomes
{
    public class CompCauseThoughtIfThoughtFound : ThingComp
    {
        public int tickCounter = 0;
        public List<Pawn> pawnList = new List<Pawn>();
        public Building thisBuilding;


        public CompProperties_CauseThoughtIfThoughtFound Props
        {
            get
            {
                return (CompProperties_CauseThoughtIfThoughtFound)this.props;
            }
        }




        public override void CompTick()
        {
            base.CompTick();
            tickCounter++;
            if (tickCounter > Props.tickInterval)
            {
                thisBuilding = this.parent as Building;
                if (thisBuilding != null && thisBuilding.Map != null && thisBuilding.GetComp<CompPowerTrader>().PowerOn)
                {
                    List<Pawn> allPawnsSpawned = thisBuilding.Map.mapPawns.AllPawnsSpawned;

                    for (int k = 0; k < allPawnsSpawned.Count; k++)
                    {
                        if (allPawnsSpawned[k] != null && !allPawnsSpawned[k].AnimalOrWildMan() && allPawnsSpawned[k].RaceProps.IsFlesh)
                        {
                            pawnList.Add(allPawnsSpawned[k]);
                        }
                    }

                    if (pawnList.Count > 0)
                    {
                        IntVec3 thisPawnLocation = thisBuilding.Position;
                        List<Pawn> tempList = new List<Pawn>();
                        for (int k = 0; k < pawnList.Count; k++)
                        {
                            if (IntVec3Utility.ManhattanDistanceFlat(thisPawnLocation, pawnList[k].Position) < Props.radius)
                            {
                                tempList.Add(pawnList[k]);
                            }
                        }

                        if (tempList.Count > 0)
                        {
                            Pawn chosenOne = tempList.RandomElement();
                            if (chosenOne != null)
                            {

                                if (!chosenOne.Dead && !chosenOne.Downed)
                                {
                                    if (Props.showEffect)
                                    {
                                        Find.TickManager.slower.SignalForceNormalSpeedShort();
                                        SoundDefOf.PsychicPulseGlobal.PlayOneShot(new TargetInfo(this.parent.Position, this.parent.Map, false));
                                        MoteMaker.MakeAttachedOverlay(this.parent, ThingDef.Named("Mote_PsycastPsychicEffect"), Vector3.zero, 1f, -1f);
                                    }

                                    if (chosenOne.needs.mood.thoughts.memories.GetFirstMemoryOfDef(ThoughtDef.Named(Props.foundThoughtDef)) != null)
                                    {
                                        //Log.Message("Memoria encontrada");
                                        chosenOne.needs.mood.thoughts.memories.TryGainMemory(ThoughtDef.Named(Props.thoughtDef), null);
                                    }
                                    else { //Log.Message("Memoria no encontrada"); 
                                    }
                                        
                                }

                            }
                        }

                        tempList.Clear();


                    }


                }
                pawnList.Clear();
                tickCounter = 0;
            }
        }


    }
}

