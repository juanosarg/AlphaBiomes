using RimWorld;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;


namespace AlphaBiomes
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

    public class AlphaBiomes_Mod_Odyssey : Mod
    {
        public static AlphaBiomes_Settings_Odyssey settings;

        public AlphaBiomes_Mod_Odyssey(ModContentPack content) : base(content)
        {
            settings = GetSettings<AlphaBiomes_Settings_Odyssey>();
        }
        public override string SettingsCategory()
            {
                if (ModsConfig.OdysseyActive)
                {
                    return "Alpha Biomes - Odyssey";
                }
                else return null;
            }
         
        public override void DoSettingsWindowContents(Rect inRect)
        {
            List<TileMutatorDef> allMutators = DefDatabase<TileMutatorDef>.AllDefsListForReading.Where(x => x.chanceOnNonLandmarkTile> 0).ToList();
            if (settings.mutatorCommonalities is null) settings.mutatorCommonalities = new Dictionary<string, float>();

            List<string> mutatorsToRemove = new List<string>();
            foreach (KeyValuePair<string,float> mutatorToRemove in settings.mutatorCommonalities)
            {
                if(DefDatabase<TileMutatorDef>.GetNamedSilentFail(mutatorToRemove.Key) is null)
                {
                    mutatorsToRemove.Add(mutatorToRemove.Key);
                }
            }
            foreach (string mutatorToRemove in mutatorsToRemove)
            {
                settings.mutatorCommonalities.Remove(mutatorToRemove);
            }
            foreach (TileMutatorDef mutator in allMutators)
            {
                if (!settings.mutatorCommonalities.ContainsKey(mutator.defName))
                {
                    settings.mutatorCommonalities[mutator.defName] = mutator.chanceOnNonLandmarkTile;
                }
            }

           

            settings.DoWindowContents(inRect);
        }

    }
}

