using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;
using Verse.AI;
using System.Collections.Generic;

using UnityEngine;

// So, let's comment this code, since it uses Harmony and has moderate complexity

namespace AlphaBiomes
{


    [HarmonyPatch(typeof(Pawn_PathFollower), "SetupMoveIntoNextCell")]
    public static class AlphaBiomes_Pawn_PathFollower_SetupMoveIntoNextCell_Patch
    {
        [HarmonyPostfix]
        public static void SetupMoveIntoNextCell_Postfix(Pawn_PathFollower __instance, Pawn ___pawn)
        {
            if (___pawn != null)
            {
                TerrainDef lava = ___pawn.Map.terrainGrid.TerrainAt(__instance.nextCell);
                if (lava.defName == "AB_SolidifiedLava")
                {

                    if (___pawn.CanEverAttachFire())
                    {
                        if (!___pawn.HasAttachment(ThingDefOf.Fire))
                        {
                           
                            TryAttachFire(___pawn, 1f);
                        }
                       
                    }



                }
            }
        }


        public static void TryAttachFire(Pawn t, float fireSize)
        {
            
            Fire fire = (Fire)ThingMaker.MakeThing(ThingDefOf.Fire, null);
            fire.fireSize = fireSize;
            fire.AttachTo(t);
            GenSpawn.Spawn(fire, t.Position, t.Map, Rot4.North, WipeMode.Vanish, false);
           // t.jobs.StopAll(false, true);
            t.records.Increment(RecordDefOf.TimesOnFire);
            //t.mindState.mentalStateHandler.TryStartMentalState(DefDatabase<MentalStateDef>.GetNamed("AB_LavaPanicFlee", true), null, true, false, null, false);
           

        }
    }




}
