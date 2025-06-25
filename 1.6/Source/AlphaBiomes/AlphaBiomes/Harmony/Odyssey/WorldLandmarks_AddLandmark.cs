using HarmonyLib;
using Mono.Cecil.Cil;
using RimWorld;
using RimWorld.QuestGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using RimWorld.Planet;



namespace AlphaBiomes
{
      [HarmonyPatch(typeof(WorldLandmarks), "AddLandmark")]
      public static class AlphaBiomes_WorldLandmarks_AddLandmark_Patch
    {
          public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> codeInstructions)
          {
              var codes = codeInstructions.ToList();
              var check = AccessTools.Method(typeof(AlphaBiomes_WorldLandmarks_AddLandmark_Patch), "GetNumber");
              var fieldLoaded = AccessTools.Field(typeof(MutatorChance), "chance");


              for (var i = 0; i < codes.Count; i++)
              {

                  if (codes[i].opcode == OpCodes.Ldfld && codes[i].OperandIs(fieldLoaded))
                  {

                      yield return new CodeInstruction(OpCodes.Call, check);

                  }else yield return codes[i];
              }
          }


          public static float GetNumber(MutatorChance mutatorChance)
          {
              return mutatorChance.chance * AlphaBiomes_Settings_Odyssey.landmarkMutatorMultiplier;
          }





      }

     
    
}
