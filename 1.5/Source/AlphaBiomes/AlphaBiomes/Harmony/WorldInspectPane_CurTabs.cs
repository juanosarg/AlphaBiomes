using HarmonyLib;
using RimWorld;
using Verse;
using UnityEngine;
using System.Collections.Generic;
using RimWorld.Planet;
using System.Linq;



namespace AlphaBiomes
{
    [HarmonyPatch(typeof(WorldInspectPane))]
    [HarmonyPatch("CurTabs", MethodType.Getter)]
    public static class AlphaBiomes_WorldInspectPane_CurTabs_Patch
    {

        private static readonly WITab[] NewTab = new WITab[]
        {
            new WITab_Image()
        };

        public static IEnumerable<InspectTabBase> Postfix(IEnumerable<InspectTabBase> values, WorldInspectPane __instance)
        {
            if (values!=null)
            {
                List<InspectTabBase> resultingList = values.ToList();

                if (Find.WorldSelector.NumSelectedObjects == 0 && Find.WorldSelector.selectedTile >= 0)
                {

                    resultingList.AddRange(NewTab);
                }


                return resultingList;
            }
            else return values;






        }
    }
}
