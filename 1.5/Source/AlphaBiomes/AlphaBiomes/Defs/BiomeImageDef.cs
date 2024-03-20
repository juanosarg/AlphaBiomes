using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace AlphaBiomes
{
    public class BiomeImageDef : Def
    {       
        public List<BiomeImages> biomeImages;
    }

    public class BiomeImages
    {
        public BiomeDef biome;
        public List<string> images;
    }
}