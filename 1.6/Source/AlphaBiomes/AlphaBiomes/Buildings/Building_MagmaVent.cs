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
    public class Building_MagmaVent : Building
    {

        private MagmaSprayer steamSprayer;

        private Sustainer spraySustainer;

        private int spraySustainerStartTick = -999;


        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            this.steamSprayer = new MagmaSprayer(this);
            this.steamSprayer.startSprayCallback = new Action(this.StartSpray);
            this.steamSprayer.endSprayCallback = new Action(this.EndSpray);
        }

        private void StartSpray()
        {
            WeatherBuildupUtility.AddSnowRadial(this.OccupiedRect().RandomCell, base.Map, 4f, -0.06f);
            this.spraySustainer = InternalDefOf.AB_MagmaVent.TrySpawnSustainer(new TargetInfo(base.Position, base.Map, false));
            this.spraySustainerStartTick = Find.TickManager.TicksGame;

            for (int iteration = 0; iteration < 15; iteration++)
            {
                IntVec3 intVec;
                if (!CellFinder.TryFindRandomCellNear(this.Position, this.Map, 7, (IntVec3 c) => true, out intVec))
                {
                    break;
                }
                if (intVec.InBounds(this.Map))
                {
                    this.Map.terrainGrid.SetTempTerrain(intVec, TerrainDefOf.LavaShallow);
                    base.Map.tempTerrain.QueueRemoveTerrain(intVec, Find.TickManager.TicksGame + 6000);
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

        protected override void Tick()
        {

            this.steamSprayer?.SteamSprayerTick();

            if (Find.TickManager.TicksGame > this.spraySustainerStartTick + 5000)
            {
                Log.Message("Geyser spray sustainer still playing after 1000 ticks. Force-ending.");
                this.spraySustainer?.End();
                this.spraySustainer = null;
            }
        }




    }
}
