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

    [HarmonyPatch(typeof(SanguophageUtility))]
    [HarmonyPatch("InSunlight")]
    public static class AlphaBiomes_SanguophageUtility_InSunlight_Patch
    {
        [HarmonyPostfix]
        public static void VampiresAreFine(ref bool __result, Map map)

        {

            if (map.Biome == InternalDefOf.AB_RockyCrags)
            {
                __result=false;
            }

        }

    }


}
