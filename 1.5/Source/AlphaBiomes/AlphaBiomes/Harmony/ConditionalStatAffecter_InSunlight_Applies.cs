using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;
using System.Collections.Generic;
using RimWorld.Planet;
using System.Linq;
using System;
using RimWorld.BaseGen;


namespace AlphaBiomes
{

    /*Vampires are fine in the Forsaken Crags. Phytokin are fucked*/

    [HarmonyPatch(typeof(ConditionalStatAffecter_InSunlight))]
    [HarmonyPatch("Applies")]
    public static class AlphaBiomes_ConditionalStatAffecter_InSunlight_Applies_Patch
    {
        [HarmonyPostfix]
        public static void VampiresAreFine(ref bool __result, StatRequest req)

        {

            if (req.HasThing && req.Thing.Spawned && req.Thing.Map?.Biome == InternalDefOf.AB_RockyCrags)
            {
                __result=false;
            }

        }

    }


}
