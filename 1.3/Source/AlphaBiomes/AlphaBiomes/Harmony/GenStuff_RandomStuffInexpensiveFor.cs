using HarmonyLib;
using RimWorld;
using System;
using Verse;



namespace AlphaBiomes
{

    /*This Harmony Postfix tries to remove exotic materials from ruins
*/
    [HarmonyPatch(typeof(GenStuff))]
    [HarmonyPatch("RandomStuffInexpensiveFor")]
    [HarmonyPatch(new Type[] { typeof(ThingDef), typeof(TechLevel), typeof(Predicate<ThingDef>) })]


    public static class AlphaBiomes_GenStuff_RandomStuffInexpensiveFor_Patch
    {
        [HarmonyPostfix]
        public static void RemoveMaterial(ref ThingDef __result)

        {
            if (__result != null) {
                if (__result.defName.Contains("AB_") || __result.defName.Contains("GU_"))
                {

                    __result = ThingDefOf.BlocksGranite;
                }
            }
            


        }

    }


}