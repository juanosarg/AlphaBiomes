
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Analytics;
using Verse;

namespace AlphaBiomes
{
    public class GameCondition_ExtremeTemperatureFluctuations : GameCondition
    {

        public bool isDay = true;

        public override void GameConditionTick()
        {
            base.GameConditionTick();

            if (Find.TickManager.TicksGame % 6000 == 0)
            {

                foreach (Map map in base.AffectedMaps)
                {
                    isDay = GenCelestial.IsDaytime(GenCelestial.CurCelestialSunGlow(map));
                   
                }

            }
        }

        public override float TemperatureOffset()
        {
            float adjustment = isDay ? 17f :-17f;
            return GameConditionUtility.LerpInOutValue(this, TransitionTicks, adjustment);
        }

    }
}
