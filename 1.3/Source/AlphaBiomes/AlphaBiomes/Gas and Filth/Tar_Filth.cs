using System;
using Verse;
using RimWorld;

namespace AlphaBiomes
{
    public class Tar_Filth : Filth
    {
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<int>(ref this.spawnTick, "spawnTick", 0, false);
        }

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
          
            this.Map.terrainGrid.SetTerrain(this.Position,InternalDefOf.AB_ArtificialTar);
        }

     

        public override void TickRare()
        {
            spawnTick++;
            if (spawnTick > DryOutTime)
            {
                this.Map.terrainGrid.RemoveTopLayer(this.Position);

                this.Destroy(DestroyMode.Vanish);
            }
        }

      

        private int spawnTick;

        private const int DryOutTime = 50;
    }
}