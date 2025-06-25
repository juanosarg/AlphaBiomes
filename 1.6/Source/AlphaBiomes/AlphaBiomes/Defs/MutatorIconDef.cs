using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace AlphaBiomes
{



    public class MutatorIconDef : Def
    {
        public List<MutatorAndIcon> mutatorIcons;
    }

    public class MutatorAndIcon
    {
        public TileMutatorDef mutator;     
        public string icon;

    }
}