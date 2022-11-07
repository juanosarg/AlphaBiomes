using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;
using RimWorld.BaseGen;
using System.Collections.Generic;


namespace AlphaBiomes
{
	//I don't know why this is needed, but sometimes terrain values skyrocket to millions
   
   /* [HarmonyPatch(typeof(WealthWatcher))]
    [HarmonyPatch("CalculateWealthFloors")]
   
    public static class AlphaBiomes_ResetStaticData_Patch
    {
        [HarmonyPostfix]
        public static void Test(ref float __result, Map ___map, float[] ___cachedTerrainMarketValue)

        {
			TerrainDef[] topGrid = ___map.terrainGrid.topGrid;
			bool[] fogGrid = ___map.fogGrid.fogGrid;
			IntVec3 size = ___map.Size;
			float num = 0f;
			int i = 0;
			for (int num2 = size.x * size.z; i < num2; i++)
			{
				if (!fogGrid[i])
				{
					if (___cachedTerrainMarketValue[topGrid[i].index] >0 )
					{

						//Log.Message(___cachedTerrainMarketValue[topGrid[i].index].ToString());
					}
					if (___cachedTerrainMarketValue[topGrid[i].index] < 1000){
						
						num += ___cachedTerrainMarketValue[topGrid[i].index];
					}
				}
			}
            if (__result != num) { __result = num;}
			

		}

    }*/


}