using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using RimWorld;
using Verse;

namespace AlphaBiomes
{
    [StaticConstructorOnStartup]
    public class CompAlcyoniteSolar: CompPowerPlantSolar
    {


        protected override float DesiredPowerOutput
        {
            get
            {
                return Mathf.Lerp(0f, 0f - base.Props.PowerConsumption, this.parent.Map.skyManager.CurSkyGlow) * this.RoofedPowerOutputFactor * this.WeatherFactor;
            }
        }

        private float RoofedPowerOutputFactor
        {
            get
            {
                int num = 0;
                int num2 = 0;
                foreach (IntVec3 c in this.parent.OccupiedRect())
                {
                    num++;
                    if (this.parent.Map.roofGrid.Roofed(c))
                    {
                        num2++;
                    }
                }
                return (float)(num - num2) / (float)num;
            }
        }

        private float WeatherFactor
        {
            get
            {
                if (parent.Map?.weatherManager.curWeather == InternalDefOf.AB_RedFog)
                {
                    return 1.5f;
                }
                else return 1;
               
            }
        }

        public override void PostDraw()
        {
          
            GenDraw.FillableBarRequest r = default(GenDraw.FillableBarRequest);
            r.center = this.parent.DrawPos + Vector3.up * 0.1f;
            r.size = BarSize;
          
            r.fillPercent = base.PowerOutput / ((0f - base.Props.PowerConsumption)*WeatherFactor);

           
            if (r.fillPercent > 1) { r.fillPercent = 1; }
            
            r.filledMat = PowerPlantSolarBarFilledMat;
            r.unfilledMat = PowerPlantSolarBarUnfilledMat;
            r.margin = 0.15f;
            Rot4 rotation = this.parent.Rotation;
            rotation.Rotate(RotationDirection.Clockwise);
            r.rotation = rotation;
            GenDraw.DrawFillableBar(r);
        }


        private static readonly Vector2 BarSize = new Vector2(2.3f, 0.14f);

        private static readonly Material PowerPlantSolarBarFilledMat = SolidColorMaterials.SimpleSolidColorMaterial(new Color(0.5f, 0.475f, 0.1f), false);

        private static readonly Material PowerPlantSolarBarUnfilledMat = SolidColorMaterials.SimpleSolidColorMaterial(new Color(0.15f, 0.15f, 0.15f), false);
    }
}
