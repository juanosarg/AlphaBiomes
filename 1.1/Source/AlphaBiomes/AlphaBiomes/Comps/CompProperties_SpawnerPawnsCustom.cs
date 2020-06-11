using System;
using System.Collections.Generic;
using Verse;
using RimWorld;

namespace AlphaBiomes
{
    public class CompProperties_SpawnerPawnCustom : CompProperties
    {
        public CompProperties_SpawnerPawnCustom()
        {
            this.compClass = typeof(CompSpawnerPawnCustom);
        }

        public List<string> spawnablePawnKinds;

        public SoundDef spawnSound;

        public float defendRadius = 21f;

        public int initialPawnsCount;

        public FloatRange pawnSpawnIntervalDays = new FloatRange(0.85f, 1.15f);

        public int pawnSpawnRadius = 2;

        public string nextSpawnInspectStringKey;

        

    }
}