
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Analytics;
using Verse;
namespace AlphaBiomes
{
    public class GameCondition_GelatinousMemoryEcho : GameCondition
    {

        public override void GameConditionTick()
        {
            base.GameConditionTick();

            if(Find.TickManager.TicksGame % 120000 == 0)
            {

                foreach (Map map in base.AffectedMaps)
                {
                    List<Pawn> pawns = map.mapPawns.FreeColonistsSpawned.Where(x => x.GetStatValue(StatDefOf.PsychicSensitivity)>0).ToList();
                    for (int i = 0; i < pawns.Count; i++)
                    {
                        if (Rand.Chance(0.33f))
                        {
                            InspirationDef inspiration = pawns[i].mindState.inspirationHandler.GetRandomAvailableInspirationDef();
                            if (inspiration != null)
                            {
                                pawns[i].mindState.inspirationHandler.TryStartInspiration(inspiration);

                            }
                            break;

                        }
                        else if(Rand.Chance(0.33f))
                        {
                            if (Rand.Chance(0.5f))
                            {
                                pawns[i].needs.mood.thoughts.memories.TryGainMemory(InternalDefOf.AB_GelatinousEchoes_Good);
                                break;
                            }
                            else
                            {
                                pawns[i].needs.mood.thoughts.memories.TryGainMemory(InternalDefOf.AB_GelatinousEchoes_Bad);
                                break;
                            }
                               
                        }
                        else if (Rand.Chance(0.33f))
                        {
            
                            MentalBreakDef mentalBreak;
                            if (ModLister.AnomalyInstalled)
                            {
                                mentalBreak = InternalDefOf.DarkVisions;
                            }
                            else
                            {
                                mentalBreak = InternalDefOf.Wander_Sad;
                            }
                            mentalBreak.Worker.TryStart(pawns[i], null, true);
                            break;
                        }
                    }
                }

            }
        }

    }
}
