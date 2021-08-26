using HarmonyLib;
using RimWorld;
using System;
using Verse;
using RimWorld.BaseGen;



namespace AlphaBiomes
{

    /*This Harmony Postfix tries to remove exotic materials from ruins
*/
    [HarmonyPatch(typeof(BaseGenUtility))]
    [HarmonyPatch("RandomCheapWallStuff")]
    [HarmonyPatch(new Type[] { typeof(TechLevel), typeof(bool) })]


    public static class AlphaBiomes_BaseGenUtility_RandomCheapWallStuff_Patch
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