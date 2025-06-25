using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse.AI;
using Verse;
using System.Linq;



namespace AlphaBiomes
{
    [StaticConstructorOnStartup]
    public class Command_SetStoneType : Command
    {

       
        public Building_CoreSampleDrill building;



        public Command_SetStoneType()
        {

            defaultDesc = "AB_ChooseMineDesc".Translate();
            defaultLabel = "AB_ChooseMine".Translate();
            this.icon = ContentFinder<Texture2D>.Get("UI/Commands/AB_RandomChunks", true);
           
            


        }

        

        public override void ProcessInput(Event ev)
        {
            base.ProcessInput(ev);
            List<FloatMenuOption> list = new List<FloatMenuOption>();


            list.Add(new FloatMenuOption("AB_ChunkRandomMine".Translate(), delegate
            {
                building.RockTypeToMine = "Random";
              
            }, MenuOptionPriority.Default, null, null, 29f, null, null));
            list.Add(new FloatMenuOption("AB_ChunkMarbleMine".Translate(), delegate
            {
                building.RockTypeToMine = "ChunkMarble";

            }, MenuOptionPriority.Default, null, null, 29f, null, null));
            list.Add(new FloatMenuOption("AB_ChunkSandstoneMine".Translate(), delegate
            {
                building.RockTypeToMine = "ChunkSandstone";

            }, MenuOptionPriority.Default, null, null, 29f, null, null));
            list.Add(new FloatMenuOption("AB_ChunkLimestoneMine".Translate(), delegate
            {
                building.RockTypeToMine = "ChunkLimestone";

            }, MenuOptionPriority.Default, null, null, 29f, null, null));
            list.Add(new FloatMenuOption("AB_ChunkGraniteMine".Translate(), delegate
            {
                building.RockTypeToMine = "ChunkGranite";

            }, MenuOptionPriority.Default, null, null, 29f, null, null));
            list.Add(new FloatMenuOption("AB_ChunkSlateMine".Translate(), delegate
            {
                building.RockTypeToMine = "ChunkSlate";

            }, MenuOptionPriority.Default, null, null, 29f, null, null));

            Find.WindowStack.Add(new FloatMenu(list));
        }






    }


}


