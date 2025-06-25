
using Verse;
using System;
using RimWorld;
using System.Collections.Generic;
using System.Linq;


namespace AlphaBiomes
{
    [StaticConstructorOnStartup]
    public static class StaticCollections
    {
        static StaticCollections()
        {   
            foreach (BiomeStructuresDef structures in DefDatabase<BiomeStructuresDef>.AllDefsListForReading)
            {
                structureToBiomes.AddRange(structures.structureToBiomeList);
            }

            foreach (MutatorIconDef mutatorAndIconList in DefDatabase<MutatorIconDef>.AllDefsListForReading)
            {
                foreach (MutatorAndIcon mutatorAndIcon in mutatorAndIconList.mutatorIcons)
                {
                    tileMutatorIcons[mutatorAndIcon.mutator]=mutatorAndIcon.icon;

                }
                  
            }
        }
      
        public static HashSet<StructureToBiome> structureToBiomes = new HashSet<StructureToBiome>();
        public static Dictionary<TileMutatorDef,string> tileMutatorIcons = new Dictionary<TileMutatorDef, string>();

    }
}
