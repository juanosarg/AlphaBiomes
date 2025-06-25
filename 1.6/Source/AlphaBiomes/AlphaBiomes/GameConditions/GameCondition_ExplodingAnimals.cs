
using AlphaBiomes;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Analytics;
using Verse;

namespace AlphaBiomes
{
    public class GameCondition_ExplodingAnimals : GameCondition
    {
        public int tickCounter = 0;
        public const int tickCounterInterval = 6000;

        public override void GameConditionTick()
        {
            base.GameConditionTick();

            if (tickCounter-- < 0)
            {

                foreach (Map map in base.AffectedMaps)
                {
                    List<Pawn> pawns = map.mapPawns.AllPawns;
                    for (int i = 0; i < pawns.Count; i++)
                    {
                        if (pawns[i].IsAnimal)
                        {
                            Hediff hediff = pawns[i].health.hediffSet.GetFirstHediffOfDef(InternalDefOf.AB_Exploder);
                            if (hediff is null)
                            {
                                pawns[i].health.AddHediff(InternalDefOf.AB_Exploder);
                            }

                        }

                    }
                }
                tickCounter = tickCounterInterval;


            }
        }

    }
}
