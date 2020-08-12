using System;
using Verse;
using Verse.Sound;
using RimWorld;
using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace AlphaBiomes
{
    public class Building_TarHole : Building
    {

        private TarSprayer steamSprayer;

        public Building harvester;

        private Sustainer spraySustainer;

        private int spraySustainerStartTick = -999;


        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            this.steamSprayer = new TarSprayer(this);
            this.steamSprayer.startSprayCallback = new Action(this.StartSpray);
            this.steamSprayer.endSprayCallback = new Action(this.EndSpray);
        }

        private void StartSpray()
        {
            SnowUtility.AddSnowRadial(this.OccupiedRect().RandomCell, base.Map, 4f, -0.06f);
            this.spraySustainer = SoundDefOf.GeyserSpray.TrySpawnSustainer(new TargetInfo(base.Position, base.Map, false));
            this.spraySustainerStartTick = Find.TickManager.TicksGame;
            int Count = 0;
            foreach (Thing thing in GenRadial.RadialDistinctThingsAround(this.Position, this.Map, 10, true))
            {
                
                if (thing.def.defName=="AB_TarPuddle")
                {
                    Count++;
                }
                
            }
            if (Count < 15)
            {
                for (int iteration = 0; iteration < 3; iteration++)
                {
                    IntVec3 intVec;
                    if (!CellFinder.TryRandomClosewalkCellNear(this.Position, this.Map, 7, out intVec))
                    {
                        break;
                    }
                    Plant plant = intVec.GetPlant(this.Map);
                    if (plant != null && plant.def.defName != "AB_TarPuddle")
                    {
                        plant.Destroy(DestroyMode.Vanish);
                        Thing plantToSpawn = GenSpawn.Spawn(ThingDef.Named("AB_TarPuddle"), intVec, this.Map, WipeMode.Vanish);
                    }else if (plant == null)
                    {
                        Thing plantToSpawn = GenSpawn.Spawn(ThingDef.Named("AB_TarPuddle"), intVec, this.Map, WipeMode.Vanish);
                    }
                        

                }
            }



        }

        private void EndSpray()
        {
            if (this.spraySustainer != null)
            {
                this.spraySustainer.End();
                this.spraySustainer = null;
            }
        }

        public override void Tick()
        {
            if (this.harvester == null)
            {
                this.steamSprayer.SteamSprayerTick();
            }
            if (this.spraySustainer != null && Find.TickManager.TicksGame > this.spraySustainerStartTick + 1000)
            {
                Log.Message("Geyser spray sustainer still playing after 1000 ticks. Force-ending.", false);
                this.spraySustainer.End();
                this.spraySustainer = null;
            }
        }

      


    }
}
