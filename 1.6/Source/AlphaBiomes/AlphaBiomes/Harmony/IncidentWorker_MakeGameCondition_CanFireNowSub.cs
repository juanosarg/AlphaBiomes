﻿using HarmonyLib;
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


   
    /*This Harmony Postfix disables the eclipse event in Forsaken Crags. It also disables the Acid Rain event everywhere except in the Pyroclastic Conflagration
*/
    [HarmonyPatch(typeof(IncidentWorker_MakeGameCondition))]
    [HarmonyPatch("CanFireNowSub")]
    public static class AlphaBiomes_IncidentWorker_MakeGameCondition_CanFireNowSub_Patch
    {
        [HarmonyPostfix]
        public static void DisableEclipseAndAcidRain(ref bool __result, ref IncidentWorker_MakeGameCondition __instance, IncidentParms parms)

        {
            Map map = Find.AnyPlayerHomeMap;
           
            if (map.Biome == InternalDefOf.AB_RockyCrags && __instance.def.gameCondition == GameConditionDefOf.Eclipse)
            {
              
                __result = false;
            }

            if (map.Biome != InternalDefOf.AB_PyroclasticConflagration && __instance.def.gameCondition==InternalDefOf.AB_AcidRainCondition)
            {

                __result = false;
            }



        }

    }

   
}
