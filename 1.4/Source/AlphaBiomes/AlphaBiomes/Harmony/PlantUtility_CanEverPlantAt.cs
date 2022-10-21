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


    /*This Harmony Postfix allows us to select a max fertility for plants
   */
    [HarmonyPatch(typeof(PlantUtility))]
    [HarmonyPatch("CanEverPlantAt")]
    [HarmonyPatch(new Type[] { typeof(ThingDef), typeof(IntVec3), typeof(Map), typeof(Thing), typeof(bool), typeof(bool) })]
    [HarmonyPatch(new Type[]
        {
            typeof(ThingDef),
            typeof(IntVec3),
            typeof(Map),
            typeof(Thing),
            typeof(bool),
            typeof(bool)
        }, new ArgumentType[]
        {
            0,
            0,
            0,
            ArgumentType.Out,
            0,
            0
        })]


    public static class AlphaBiomes_PlantUtility_CanEverPlantAt_Patch
    {
        [HarmonyPostfix]
        public static void DetectMaxFertility(ThingDef plantDef,IntVec3 c, Map map, ref AcceptanceReport __result, bool writeNoReason)

        {
            PlantPropertiesExtension extension = plantDef.GetModExtension<PlantPropertiesExtension>();
            if (extension != null)
            {

                if (map.fertilityGrid.FertilityAt(c) > extension.fertilityMax)
                {
                    if (!writeNoReason)
                    {
                        __result=  "AB_MessageWarningTooHighFertility".Translate();
                    }
                    __result = false;
                }

            }



        }

    }

}
