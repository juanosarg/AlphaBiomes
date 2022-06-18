
using Verse;

namespace AlphaBiomes
{
    public class CompProperties_CauseThoughtIfThoughtFound : CompProperties
    {


        public int radius = 1;
        public int tickInterval = 1000;
        public string thoughtDef = "AteWithoutTable";
        public string foundThoughtDef = "AteWithoutTable";
        public bool showEffect = false;



        public CompProperties_CauseThoughtIfThoughtFound()
        {
            this.compClass = typeof(CompCauseThoughtIfThoughtFound);
        }


    }
}
