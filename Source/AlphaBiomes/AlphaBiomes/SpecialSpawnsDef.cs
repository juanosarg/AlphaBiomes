using RimWorld;
using System;
using Verse;
using System.Collections.Generic;


namespace AlphaBiomes
{
    public class SpecialSpawnsDef : Def
    {
        public ThingDef thingDef;
        public bool allowOnWater;
        public int numberToSpawn;
        public List<string> terrainValidationAllowed;
        public List<string> terrainValidationDisallowed;
        public List<string> forbiddenBiomes;
        public List<string> allowedBiomes;

    }
}
