using RimWorld;
using System.Text;
using UnityEngine;
using Verse;

namespace AlphaBiomes
{
    class Plant_Cold : Plant
    {
        
        public float GrowthRateFactor_ColdTemperature
        {
            get
            {
                float num;
                if (!GenTemperature.TryGetTemperatureForCell(base.Position, base.Map, out num))
                {
                    return 1f;
                }
                if (num < -50f)
                {
                    return Mathf.InverseLerp(-65f, -50f, num);
                }
                if (num > -5f)
                {
                    return Mathf.InverseLerp(10f, -5f, num);
                }
                return 1f;
            }
        }

        public override float GrowthRate
        {
            get
            {
                if (this.Blighted)
                {
                    return 0f;
                }

                return this.GrowthRateFactor_Fertility * this.GrowthRateFactor_ColdTemperature * this.GrowthRateFactor_Light;
            }
        }

        public override string GetInspectString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (this.LifeStage == PlantLifeStage.Growing)
            {
                stringBuilder.AppendLine("PercentGrowth".Translate(this.GrowthPercentString));
                stringBuilder.AppendLine("GrowthRate".Translate() + ": " + this.GrowthRate.ToStringPercent());
                if (!this.Blighted)
                {
                    if (this.Resting)
                    {
                        stringBuilder.AppendLine("PlantResting".Translate());
                    }
                    if (!this.HasEnoughLightToGrow)
                    {
                        stringBuilder.AppendLine("PlantNeedsLightLevel".Translate() + ": " + this.def.plant.growMinGlow.ToStringPercent());
                    }
                    float growthRateFactor_Temperature = this.GrowthRateFactor_ColdTemperature;
                    if (growthRateFactor_Temperature < 0.99f)
                    {
                        if (growthRateFactor_Temperature < 0.01f)
                        {
                            stringBuilder.AppendLine("OutOfIdealTemperatureRangeNotGrowing".Translate());
                        }
                        else
                        {
                            stringBuilder.AppendLine("OutOfIdealTemperatureRange".Translate(Mathf.RoundToInt(growthRateFactor_Temperature * 100f).ToString()));
                        }
                    }
                }
            }
            else if (this.LifeStage == PlantLifeStage.Mature)
            {
                if (this.HarvestableNow)
                {
                    stringBuilder.AppendLine("ReadyToHarvest".Translate());
                }
                else
                {
                    stringBuilder.AppendLine("Mature".Translate());
                }
            }
            if (this.DyingBecauseExposedToLight)
            {
                stringBuilder.AppendLine("DyingBecauseExposedToLight".Translate());
            }
            if (this.Blighted)
            {
                stringBuilder.AppendLine("Blighted".Translate() + " (" + this.Blight.Severity.ToStringPercent() + ")");
            }
            return stringBuilder.ToString().TrimEndNewlines();
        }


        public override void TickLong()
        {
            CheckMakeLeafless();
            if (base.Destroyed)
            {
                return;
            }
            
                float num = this.growthInt;
                bool flag = this.LifeStage == PlantLifeStage.Mature;
                this.growthInt += this.GrowthPerTick * 2000f;
                if (this.growthInt > 1f)
                {
                    this.growthInt = 1f;
                }
                if (((!flag && this.LifeStage == PlantLifeStage.Mature) || (int)(num * 10f) != (int)(this.growthInt * 10f)) && this.CurrentlyCultivated())
                {
                    base.Map.mapDrawer.MapMeshDirty(base.Position, MapMeshFlag.Things);
                }
            
            if (!this.HasEnoughLightToGrow)
            {
                this.unlitTicks += 2000;
            }
            else
            {
                this.unlitTicks = 0;
            }
            this.ageInt += 2000;
            if (this.Dying)
            {
                Map map = base.Map;
                bool isCrop = this.IsCrop;
                bool harvestableNow = this.HarvestableNow;
                bool dyingBecauseExposedToLight = this.DyingBecauseExposedToLight;
                int num2 = Mathf.CeilToInt(this.CurrentDyingDamagePerTick * 2000f);
                base.TakeDamage(new DamageInfo(DamageDefOf.Rotting, (float)num2, 0f, -1f, null, null, null, DamageInfo.SourceCategory.ThingOrUnknown, null));
                if (base.Destroyed)
                {
                    if (isCrop && this.def.plant.Harvestable && MessagesRepeatAvoider.MessageShowAllowed("MessagePlantDiedOfRot-" + this.def.defName, 240f))
                    {
                        string key;
                        if (harvestableNow)
                        {
                            key = "MessagePlantDiedOfRot_LeftUnharvested";
                        }
                        else if (dyingBecauseExposedToLight)
                        {
                            key = "MessagePlantDiedOfRot_ExposedToLight";
                        }
                        else
                        {
                            key = "MessagePlantDiedOfRot";
                        }
                        Messages.Message(key.Translate(this.GetCustomLabelNoCount(false)).CapitalizeFirst(), new TargetInfo(base.Position, map, false), MessageTypeDefOf.NegativeEvent, true);
                    }
                    return;
                }
            }
           
        }
    }
}