using System;
using Verse;
using RimWorld;

namespace AlphaBiomes
{
    public class CompProperties_SpawnerDouble : CompProperties
    {
        public CompProperties_SpawnerDouble()
        {
            this.compClass = typeof(CompSpawnerDouble);
        }

        public ThingDef thingToSpawn;

        public int spawnCount = 1;

        public ThingDef SecondaryThingToSpawn;

        public int secondarySpawnCount = 1;

        public IntRange spawnIntervalRange = new IntRange(100, 100);

        public int spawnMaxAdjacent = -1;

        public bool spawnForbidden;

        public bool requiresPower;

        public bool writeTimeLeftToSpawn;

        public bool showMessageIfOwned;

        public string saveKeysPrefix;

        public bool inheritFaction;
    }
}