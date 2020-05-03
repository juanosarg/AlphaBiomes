using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse.AI;
using Verse;
using System.Linq;

namespace AlphaBiomes
{
    public class Building_CoreSampleDrill : Building
    {

        public string RockTypeToMine = "Random";

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<string>(ref this.RockTypeToMine, "RockTypeToMine", "Random", false);


        }
    }
}
