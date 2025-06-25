using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Security.Cryptography;
using AlphaBiomes;

namespace AlphaBiomes
{

    [HarmonyPatch(typeof(Mineable))]
    [HarmonyPatch("TrySpawnYield")]
    [HarmonyPatch(new Type[] { typeof(Map), typeof(bool), typeof(Pawn) })]
    public static class AlphaBiomes_Mineable_TrySpawnYield_Patch
    {

        [HarmonyPostfix]
        public static void ExtraYields(Map map, bool moteOnWaste, Pawn pawn, Mineable __instance)
        {
            if (pawn != null && __instance.def == InternalDefOf.GU_RoseQuartz)
            {
                if (Rand.Chance(0.5f))
                {
                    Thing thing = ThingMaker.MakeThing(InternalDefOf.AB_AlcyoniteChunk);
                    thing.stackCount = new IntRange(10, 20).RandomInRange;
                    GenPlace.TryPlaceThing(thing, __instance.Position, map, ThingPlaceMode.Near);

                }
                
            }



        }




    }













}

