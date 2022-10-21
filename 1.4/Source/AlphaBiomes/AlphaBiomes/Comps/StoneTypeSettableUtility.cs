using RimWorld;
using Verse;

using UnityEngine;


namespace AlphaBiomes
{
    public static class StoneTypeSettableUtility
    {
        public static Command_SetStoneType SetStoneToMineCommand(Building_CoreSampleDrill passingbuilding)
        {
            return new Command_SetStoneType()
            {

                hotKey = KeyBindingDefOf.Misc1,
              
                building = passingbuilding

            };
        }


    }
}