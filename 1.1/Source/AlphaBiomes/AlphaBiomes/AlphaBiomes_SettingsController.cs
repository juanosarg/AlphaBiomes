using RimWorld;
using UnityEngine;
using Verse;


namespace AlphaBiomes.Settings
{



   
    public class AlphaBiomes_Mod : Mod
    {


        public AlphaBiomes_Mod(ModContentPack content) : base(content)
        {
            GetSettings<AlphaBiomes_Settings>();
        }
        public override string SettingsCategory() => "Alpha Biomes";

        public override void DoSettingsWindowContents(Rect inRect)
        {
            AlphaBiomes_Settings.DoWindowContents(inRect);


        }
    }
}

