using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace AlphaBiomes
{

   

    public class BiomeStructuresDef : Def
    {       
        public List<StructureToBiome> structureToBiomeList;
    }

    public class StructureToBiome
    {
        public BiomeDef biome;
        public TileMutatorDef mutator;
        public BuildableDef structure;
        public bool allBiomes = false;
        public string iconOverride;
        public string descOverride;
        public string labelOverride;
        public string notBuildableMessage;
        
    }
}