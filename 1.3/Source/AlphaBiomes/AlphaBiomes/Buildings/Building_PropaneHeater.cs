using System;
using UnityEngine;
using Verse;
using RimWorld;

namespace AlphaBiomes
{
    public class Building_PropaneHeater : Building_TempControl
    {
        public int tickCounter = 0;
        public int ticksRare = 300;

        public override void Tick()
        {
            tickCounter++;
            if (tickCounter> ticksRare)
            {
                CompRefuelable compRefuelable = this.GetComp<CompRefuelable>();
                if (compRefuelable.HasFuel)
                {

                    float ambientTemperature = base.AmbientTemperature;
                    float num;
                    if (ambientTemperature < 20f)
                    {
                        num = 1f;
                    }
                    else if (ambientTemperature > 120f)
                    {
                        num = 0f;
                    }
                    else
                    {
                        num = Mathf.InverseLerp(120f, 20f, ambientTemperature);
                    }
                    float energyLimit = this.compTempControl.Props.energyPerSecond * num * 4.16666651f;
                    float num2 = GenTemperature.ControlTemperatureTempChange(base.Position, base.Map, energyLimit, this.compTempControl.targetTemperature);
                    bool flag = !Mathf.Approximately(num2, 0f);
                    //CompProperties_Power props = this.compPowerTrader.Props;
                    if (flag)
                    {
                        this.GetRoom().Temperature += num2;
                        //this.compPowerTrader.PowerOutput = -props.basePowerConsumption;
                    }
                    else
                    {
                        //this.compPowerTrader.PowerOutput = -props.basePowerConsumption * this.compTempControl.Props.lowPowerConsumptionFactor;
                    }
                    this.compTempControl.operatingAtHighPower = flag;
                }
                tickCounter = 0;


            }
            
        }

        private const float EfficiencyFalloffSpan = 100f;
    }
}
