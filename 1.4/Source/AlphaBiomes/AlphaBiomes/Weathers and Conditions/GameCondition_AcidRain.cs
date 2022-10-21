using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;

namespace AlphaBiomes
{
    public class GameCondition_AcidRain : GameCondition
    {
        public override int TransitionTicks
        {
            get
            {
                return 5000;
            }
        }

        public override void End()
        {
            List<Map> affectedMaps = base.AffectedMaps;

            for (int i = 0; i < affectedMaps.Count; i++)
            {
                affectedMaps[i].weatherManager.curWeather = WeatherDef.Named("Clear");
                affectedMaps[i].weatherManager.TransitionTo(WeatherDef.Named("Clear"));
            }
            base.End();
           
        }

        public override void Init()
        {
            LessonAutoActivator.TeachOpportunity(ConceptDefOf.ForbiddingDoors, OpportunityType.Critical);
            LessonAutoActivator.TeachOpportunity(ConceptDefOf.AllowedAreas, OpportunityType.Critical);
            List<Map> affectedMaps = base.AffectedMaps;
            for (int i = 0; i < affectedMaps.Count; i++)
            {
               
                affectedMaps[i].weatherManager.curWeather = InternalDefOf.AB_AcidRainWeather;
                affectedMaps[i].weatherManager.TransitionTo(InternalDefOf.AB_AcidRainWeather);
            }
        }

        public override void GameConditionTick()
        {
            List<Map> affectedMaps = base.AffectedMaps;
            if (Find.TickManager.TicksGame % WeatherCheckInterval == 0)
            {
                for (int i = 0; i < affectedMaps.Count; i++)
                {
                  
                    affectedMaps[i].weatherManager.curWeather = InternalDefOf.AB_AcidRainWeather;
                    affectedMaps[i].weatherManager.TransitionTo(InternalDefOf.AB_AcidRainWeather);
                }
            }
            if (Find.TickManager.TicksGame % CheckInterval == 0)
            {
                for (int i = 0; i < affectedMaps.Count; i++)
                {
                    this.DoPawnsToxicDamage(affectedMaps[i]);
                   
                }
            }
           
        }

        private void DoPawnsToxicDamage(Map map)
        {
            List<Pawn> allPawnsSpawned = map.mapPawns.AllPawnsSpawned;
            for (int i = 0; i < allPawnsSpawned.Count; i++)
            {
                DoPawnToxicDamage(allPawnsSpawned[i]);
            }
        }

        public static void DoPawnToxicDamage(Pawn p)
        {
            if (p.Spawned && p.Position.Roofed(p.Map))
            {
                return;
            }
            if (!p.RaceProps.IsFlesh)
            {
                return;
            }
            if (p.def.BaseFlammability==0)
            {
                return;
            }
            if (ModLister.HasActiveModWithName("Alpha Animals"))
            {
                p.TakeDamage(new DamageInfo(DefDatabase<DamageDef>.GetNamed("AA_AcidRainDamage", true), 1, 0f, -1f, null, null, null, DamageInfo.SourceCategory.ThingOrUnknown));

            }
            else {
                float num = 0.028758334f;
                num *= p.GetStatValue(StatDefOf.ToxicResistance, true);
                if (num != 0f)
                {
                    float num2 = Mathf.Lerp(0.85f, 1.15f, Rand.ValueSeeded(p.thingIDNumber ^ 74374237));
                    num *= num2;
                    HealthUtility.AdjustSeverity(p, HediffDefOf.ToxicBuildup, num);
                }

            }
            
        }

        public override void DoCellSteadyEffects(IntVec3 c, Map map)
        {
            if (!c.Roofed(map))
            {
                List<Thing> thingList = c.GetThingList(map);
                for (int i = 0; i < thingList.Count; i++)
                {
                    Thing thing = thingList[i];
                    if (thing is Plant)
                    {
                        if (thing.def.plant.dieFromToxicFallout && Rand.Value < 0.0065f)
                        {
                            thing.Kill(null, null);
                        }
                    }
                    else if (thing.def.category == ThingCategory.Item)
                    {
                        CompRottable compRottable = thing.TryGetComp<CompRottable>();
                        if (compRottable != null && compRottable.Stage < RotStage.Dessicated)
                        {
                            compRottable.RotProgress += 3000f;
                        }
                    }
                }
            }
        }

      

        public override float SkyTargetLerpFactor(Map map)
        {
            return GameConditionUtility.LerpInOutValue(this, (float)this.TransitionTicks, 0.5f);
        }

       

        public override float AnimalDensityFactor(Map map)
        {
            return 0f;
        }

        public override float PlantDensityFactor(Map map)
        {
            return 0f;
        }

        public override bool AllowEnjoyableOutsideNow(Map map)
        {
            return false;
        }

      

        private const float MaxSkyLerpFactor = 0.5f;

        private const float SkyGlow = 0.85f;

      

        public const int CheckInterval = 10000;

        public const int WeatherCheckInterval = 300;

        private const float ToxicPerDay = 0.5f;

        private const float PlantKillChance = 0.0065f;

        private const float CorpseRotProgressAdd = 3000f;
    }
}
