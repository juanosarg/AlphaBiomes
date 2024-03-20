using RimWorld;
using UnityEngine;
using Verse;


namespace AlphaBiomes.Settings
{



   
    public class AlphaBiomes_Mod : Mod
    {
        public static AlphaBiomes_Settings settings;

        public AlphaBiomes_Mod(ModContentPack content) : base(content)
        {
            settings = GetSettings<AlphaBiomes_Settings>();
        }
        public override string SettingsCategory() => "Alpha Biomes";

        public override void DoSettingsWindowContents(Rect inRect)
        {
            settings.DoWindowContents(inRect);
        }




      
    }
}

