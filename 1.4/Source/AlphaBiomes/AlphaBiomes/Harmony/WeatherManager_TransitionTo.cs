using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;
using RimWorld.BaseGen;
using System.Collections.Generic;


namespace AlphaBiomes
{
     [HarmonyPatch(typeof(WeatherManager))]
     [HarmonyPatch("TransitionTo")]

     public static class AlphaBiomes_WeatherManager_TransitionTo_Patch
    {
         [HarmonyPostfix]
         public static void DecideForsakenWeathers(WeatherDef newWeather, WeatherManager __instance)

         {
            if (AlphaBiomes_Settings.AB_BrighterCrags)
            {
                if (newWeather == InternalDefOf.AB_ForsakenNight)
                {
                    __instance.curWeather = InternalDefOf.AB_ForsakenNight_Alternate;
                }
                if (newWeather == InternalDefOf.AB_ForsakenRainyNight)
                {
                    __instance.curWeather = InternalDefOf.AB_ForsakenRainyNight_Alternate;
                }
                if (newWeather == InternalDefOf.AB_ForsakenThunderstorm)
                {
                    __instance.curWeather = InternalDefOf.AB_ForsakenThunderstorm_Alternate;
                }
            }
           




         }

     }


}