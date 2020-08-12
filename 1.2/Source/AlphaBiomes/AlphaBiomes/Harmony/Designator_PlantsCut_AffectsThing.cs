using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;
using System.Collections.Generic;
using RimWorld.Planet;
using System.Linq;
using System;
using RimWorld.BaseGen;

// So, let's comment this code, since it uses Harmony and has moderate complexity

namespace AlphaBiomes
{

    


    /*This Harmony Postfix tries to remove the plant cut gizmo from tar puddles
*/
    [HarmonyPatch(typeof(Designator_PlantsCut))]
    [HarmonyPatch("AffectsThing")]
    public static class AlphaBiomes_Designator_PlantsCut_AffectsThing_Patch
    {
        [HarmonyPostfix]
        public static void RemovePlantCutGizmo(ref bool __result, ref Thing t)

        {

            if (t.def.defName== "AB_TarPuddle") {
                __result = false;
            }


        }

    }


}
