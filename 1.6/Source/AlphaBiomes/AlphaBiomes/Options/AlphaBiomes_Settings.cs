using RimWorld;
using UnityEngine;
using Verse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using Verse.Noise;
using static System.Collections.Specialized.BitVector32;


namespace AlphaBiomes
{
    public class AlphaBiomes_Settings : ModSettings
    {
        public static bool AB_UseAgariluxParticles = true;
        public static bool AB_BrighterCrags = true;
        public static bool AB_ShowBuildingsButton = true;
        public static bool AB_RemoveVanillaBiomes = false;
        private static Vector2 scrollPosition = Vector2.zero;
        public const float biomeMultiplierBase = 1;
        public static float feraliskInfestedJungleMultiplier = biomeMultiplierBase;
        public static float gallatrossGraveyardMultiplier = biomeMultiplierBase;
        public static float gelatinousSuperorganismMultiplier = biomeMultiplierBase;
        public static float idyllicMeadowsMultiplier = biomeMultiplierBase;
        public static float mechanoidIntrusionMultiplier = biomeMultiplierBase;
        public static float miasmicMangroveMultiplier = biomeMultiplierBase;
        public static float mycoticJungleMultiplier = biomeMultiplierBase;
        public static float ocularForestMultiplier = biomeMultiplierBase;
        public static float propaneLakesMultiplier = biomeMultiplierBase;
        public static float rockyCragsMultiplier = biomeMultiplierBase;
        public static float pyroclasticConflagrationMultiplier = biomeMultiplierBase;
        public static float tarPitsMultiplier = biomeMultiplierBase;

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(ref AB_UseAgariluxParticles, "AB_UseAgariluxParticles", true, true);
            Scribe_Values.Look(ref AB_BrighterCrags, "AB_BrighterCrags", false, false);
            Scribe_Values.Look(ref AB_ShowBuildingsButton, "AB_ShowBuildingsButton", true, true);
            Scribe_Values.Look<float>(ref feraliskInfestedJungleMultiplier, "feraliskInfestedJungleMultiplier", 1, true);
            Scribe_Values.Look<float>(ref gallatrossGraveyardMultiplier, "gallatrossGraveyardMultiplier", 1, true);
            Scribe_Values.Look<float>(ref gelatinousSuperorganismMultiplier, "gelatinousSuperorganismMultiplier", 1, true);
            Scribe_Values.Look<float>(ref idyllicMeadowsMultiplier, "idyllicMeadowsMultiplier", 1, true);
            Scribe_Values.Look<float>(ref mechanoidIntrusionMultiplier, "mechanoidIntrusionMultiplier", 1, true);
            Scribe_Values.Look<float>(ref miasmicMangroveMultiplier, "miasmicMangroveMultiplier", 1, true);
            Scribe_Values.Look<float>(ref mycoticJungleMultiplier, "mycoticJungleMultiplier", 1, true);
            Scribe_Values.Look<float>(ref ocularForestMultiplier, "ocularForestMultiplier", 1, true);
            Scribe_Values.Look<float>(ref propaneLakesMultiplier, "propaneLakesMultiplier", 1, true);
            Scribe_Values.Look<float>(ref rockyCragsMultiplier, "rockyCragsMultiplier", 1, true);
            Scribe_Values.Look<float>(ref pyroclasticConflagrationMultiplier, "pyroclasticConflagrationMultiplier", 1, true);
            Scribe_Values.Look<float>(ref tarPitsMultiplier, "tarPitsMultiplier", 1, true);
            Scribe_Values.Look(ref AB_RemoveVanillaBiomes, "AB_RemoveVanillaBiomes", false, true);


        }
        public void DoWindowContents(Rect inRect)
        {
            Listing_Standard ls = new Listing_Standard();

            var scrollContainer = inRect.ContractedBy(10);
            scrollContainer.height -= ls.CurHeight;
            scrollContainer.y += ls.CurHeight;
            Widgets.DrawBoxSolid(scrollContainer, Color.grey);
            var innerContainer = scrollContainer.ContractedBy(1);
            Widgets.DrawBoxSolid(innerContainer, new ColorInt(42, 43, 44).ToColor);
            var frameRect = innerContainer.ContractedBy(5);
            frameRect.y += 15;
            frameRect.height -= 15;
            var contentRect = frameRect;
            contentRect.x = 0;
            contentRect.y = 0;
            contentRect.width -= 20;


            int numberBiomes = (from k in DefDatabase<BiomeDef>.AllDefsListForReading
                                where k.defName.Contains("AB_")
                                select k).Count();

            contentRect.height = numberBiomes * 100f + 150f;


            Listing_Standard ls2 = new Listing_Standard();

            Widgets.BeginScrollView(frameRect, ref scrollPosition, contentRect, true);
            ls2.Begin(contentRect.AtZero());

            ls2.CheckboxLabeled("AB_UseAgariluxParticles".Translate(), ref AB_UseAgariluxParticles, "AB_UseAgariluxParticlesDesc".Translate());
            ls2.CheckboxLabeled("AB_BrighterCrags".Translate(), ref AB_BrighterCrags, "AB_BrighterCragsDescription".Translate());
            ls2.CheckboxLabeled("AB_ShowBuildingsButton".Translate(), ref AB_ShowBuildingsButton, "AB_ShowBuildingsButtonDescription".Translate());
            ls2.CheckboxLabeled("AB_RemoveVanillaBiomes".Translate(), ref AB_RemoveVanillaBiomes, "AB_RemoveVanillaBiomesDesc".Translate());


            ls2.GapLine();
            Text.Font = GameFont.Medium;
            ls2.Label("AB_BiomeCommonalities".Translate());
            Text.Font = GameFont.Small;
            ls2.Gap();

            var ferJungleLabel = ls2.LabelPlusButton("AB_BiomeMultiplier".Translate(InternalDefOf.AB_FeraliskInfestedJungle.LabelCap) + ": " + feraliskInfestedJungleMultiplier, "AB_MultiplierTooltip".Translate(InternalDefOf.AB_FeraliskInfestedJungle.LabelCap));
            feraliskInfestedJungleMultiplier = (float)Math.Round(ls2.Slider(feraliskInfestedJungleMultiplier, 0f, 2f), 2);

            if (ls2.Settings_Button("AB_Reset".Translate(InternalDefOf.AB_FeraliskInfestedJungle.LabelCap), new Rect(0f, ferJungleLabel.position.y + 35, 250f, 29f)))
            {
                feraliskInfestedJungleMultiplier = biomeMultiplierBase;
            }

            var forCragsLabel = ls2.LabelPlusButton("AB_BiomeMultiplier".Translate(InternalDefOf.AB_RockyCrags.LabelCap) + ": " + rockyCragsMultiplier, "AB_MultiplierTooltip".Translate(InternalDefOf.AB_RockyCrags.LabelCap));
            rockyCragsMultiplier = (float)Math.Round(ls2.Slider(rockyCragsMultiplier, 0f, 2f), 2);

            if (ls2.Settings_Button("AB_Reset".Translate(InternalDefOf.AB_RockyCrags.LabelCap), new Rect(0f, forCragsLabel.position.y + 35, 250f, 29f)))
            {
                rockyCragsMultiplier = biomeMultiplierBase;
            }

            var galGraveLabel = ls2.LabelPlusButton("AB_BiomeMultiplier".Translate(InternalDefOf.AB_GallatrossGraveyard.LabelCap) + ": " + gallatrossGraveyardMultiplier, "AB_MultiplierTooltip".Translate(InternalDefOf.AB_GallatrossGraveyard.LabelCap));
            gallatrossGraveyardMultiplier = (float)Math.Round(ls2.Slider(gallatrossGraveyardMultiplier, 0f, 2f), 2);

            if (ls2.Settings_Button("AB_Reset".Translate(InternalDefOf.AB_GallatrossGraveyard.LabelCap), new Rect(0f, galGraveLabel.position.y + 35, 250f, 29f)))
            {
                gallatrossGraveyardMultiplier = biomeMultiplierBase;
            }

            var gelSuperLabel = ls2.LabelPlusButton("AB_BiomeMultiplier".Translate(InternalDefOf.AB_GelatinousSuperorganism.LabelCap) + ": " + gelatinousSuperorganismMultiplier, "AB_MultiplierTooltip".Translate(InternalDefOf.AB_GelatinousSuperorganism.LabelCap));
            gelatinousSuperorganismMultiplier = (float)Math.Round(ls2.Slider(gelatinousSuperorganismMultiplier, 0f, 2f), 2);

            if (ls2.Settings_Button("AB_Reset".Translate(InternalDefOf.AB_GelatinousSuperorganism.LabelCap), new Rect(0f, gelSuperLabel.position.y + 35, 250f, 29f)))
            {
                gelatinousSuperorganismMultiplier = biomeMultiplierBase;
            }

            var idyllicMeadowsLabel = ls2.LabelPlusButton("AB_BiomeMultiplier".Translate(InternalDefOf.AB_IdyllicMeadows.LabelCap) + ": " + idyllicMeadowsMultiplier, "AB_MultiplierTooltip".Translate(InternalDefOf.AB_IdyllicMeadows.LabelCap));
            idyllicMeadowsMultiplier = (float)Math.Round(ls2.Slider(idyllicMeadowsMultiplier, 0f, 2f), 2);

            if (ls2.Settings_Button("AB_Reset".Translate(InternalDefOf.AB_IdyllicMeadows.LabelCap), new Rect(0f, idyllicMeadowsLabel.position.y + 35, 250f, 29f)))
            {
                idyllicMeadowsMultiplier = biomeMultiplierBase;
            }

            var mechInvasionLabel = ls2.LabelPlusButton("AB_BiomeMultiplier".Translate(InternalDefOf.AB_MechanoidIntrusion.LabelCap) + ": " + mechanoidIntrusionMultiplier, "AB_MultiplierTooltip".Translate(InternalDefOf.AB_MechanoidIntrusion.LabelCap));
            mechanoidIntrusionMultiplier = (float)Math.Round(ls2.Slider(mechanoidIntrusionMultiplier, 0f, 2f), 2);

            if (ls2.Settings_Button("AB_Reset".Translate(InternalDefOf.AB_MechanoidIntrusion.LabelCap), new Rect(0f, mechInvasionLabel.position.y + 35, 250f, 29f)))
            {
                mechanoidIntrusionMultiplier = biomeMultiplierBase;
            }

            var miasmicMangroveLabel = ls2.LabelPlusButton("AB_BiomeMultiplier".Translate(InternalDefOf.AB_MiasmicMangrove.LabelCap) + ": " + miasmicMangroveMultiplier, "AB_MultiplierTooltip".Translate(InternalDefOf.AB_MiasmicMangrove.LabelCap));
            miasmicMangroveMultiplier = (float)Math.Round(ls2.Slider(miasmicMangroveMultiplier, 0f, 2f), 2);

            if (ls2.Settings_Button("AB_Reset".Translate(InternalDefOf.AB_MiasmicMangrove.LabelCap), new Rect(0f, miasmicMangroveLabel.position.y + 35, 250f, 29f)))
            {
                miasmicMangroveMultiplier = biomeMultiplierBase;
            }

            var mycJungleLabel = ls2.LabelPlusButton("AB_BiomeMultiplier".Translate(InternalDefOf.AB_MycoticJungle.LabelCap) + ": " + mycoticJungleMultiplier, "AB_MultiplierTooltip".Translate(InternalDefOf.AB_MycoticJungle.LabelCap));
            mycoticJungleMultiplier = (float)Math.Round(ls2.Slider(mycoticJungleMultiplier, 0f, 2f), 2);

            if (ls2.Settings_Button("AB_Reset".Translate(InternalDefOf.AB_MycoticJungle.LabelCap), new Rect(0f, mycJungleLabel.position.y + 35, 250f, 29f)))
            {
                mycoticJungleMultiplier = biomeMultiplierBase;
            }

            var ocForestLabel = ls2.LabelPlusButton("AB_BiomeMultiplier".Translate(InternalDefOf.AB_OcularForest.LabelCap) + ": " + ocularForestMultiplier, "AB_MultiplierTooltip".Translate(InternalDefOf.AB_OcularForest.LabelCap));
            ocularForestMultiplier = (float)Math.Round(ls2.Slider(ocularForestMultiplier, 0f, 2f), 2);

            if (ls2.Settings_Button("AB_Reset".Translate(InternalDefOf.AB_OcularForest.LabelCap), new Rect(0f, ocForestLabel.position.y + 35, 250f, 29f)))
            {
                ocularForestMultiplier = biomeMultiplierBase;
            }

            var propLakesLabel = ls2.LabelPlusButton("AB_BiomeMultiplier".Translate(InternalDefOf.AB_PropaneLakes.LabelCap) + ": " + propaneLakesMultiplier, "AB_MultiplierTooltip".Translate(InternalDefOf.AB_PropaneLakes.LabelCap));
            propaneLakesMultiplier = (float)Math.Round(ls2.Slider(propaneLakesMultiplier, 0f, 2f), 2);

            if (ls2.Settings_Button("AB_Reset".Translate(InternalDefOf.AB_PropaneLakes.LabelCap), new Rect(0f, propLakesLabel.position.y + 35, 250f, 29f)))
            {
                propaneLakesMultiplier = biomeMultiplierBase;
            }

            var pyroConfLabel = ls2.LabelPlusButton("AB_BiomeMultiplier".Translate(InternalDefOf.AB_PyroclasticConflagration.LabelCap) + ": " + pyroclasticConflagrationMultiplier, "AB_MultiplierTooltip".Translate(InternalDefOf.AB_PyroclasticConflagration.LabelCap));
            pyroclasticConflagrationMultiplier = (float)Math.Round(ls2.Slider(pyroclasticConflagrationMultiplier, 0f, 2f), 2);

            if (ls2.Settings_Button("AB_Reset".Translate(InternalDefOf.AB_PyroclasticConflagration.LabelCap), new Rect(0f, pyroConfLabel.position.y + 35, 250f, 29f)))
            {
                pyroclasticConflagrationMultiplier = biomeMultiplierBase;
            }

            var tarPitsLabel = ls2.LabelPlusButton("AB_BiomeMultiplier".Translate(InternalDefOf.AB_TarPits.LabelCap) + ": " + tarPitsMultiplier, "AB_MultiplierTooltip".Translate(InternalDefOf.AB_TarPits.LabelCap));
            tarPitsMultiplier = (float)Math.Round(ls2.Slider(tarPitsMultiplier, 0f, 2f), 2);

            if (ls2.Settings_Button("AB_Reset".Translate(InternalDefOf.AB_TarPits.LabelCap), new Rect(0f, tarPitsLabel.position.y + 35, 250f, 29f)))
            {
                tarPitsMultiplier = biomeMultiplierBase;
            }

            ls2.End();
            Widgets.EndScrollView();
            base.Write();
        }
    }

    public class AlphaBiomes_Settings_Odyssey : ModSettings

    {

        public static bool AB_RemoveOdysseyBiomes = false;
        public const float mutatorMultiplierBase = 1;
        public static float mutatorMultiplier = mutatorMultiplierBase;
        public const float landmarkMutatorMultiplierBase = 1;
        public static float landmarkMutatorMultiplier = landmarkMutatorMultiplierBase;
        private static Vector2 scrollPosition = Vector2.zero;
        public Dictionary<string, float> mutatorCommonalities = new Dictionary<string, float>();
        private List<string> mutatorKeys;
        private List<float> mutatorValues;
     

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref AB_RemoveOdysseyBiomes, "AB_RemoveOdysseyBiomes", false, true);
            Scribe_Values.Look<float>(ref mutatorMultiplier, "mutatorMultiplier", mutatorMultiplierBase, true);
            Scribe_Values.Look<float>(ref landmarkMutatorMultiplier, "landmarkMutatorMultiplier", landmarkMutatorMultiplierBase, true);
            Scribe_Collections.Look(ref mutatorCommonalities, "mutatorCommonalities", LookMode.Value, LookMode.Value, ref mutatorKeys, ref mutatorValues);           
        }

        public void DoWindowContents(Rect inRect)
        {
            Listing_Standard ls = new Listing_Standard();

            var scrollContainer = inRect.ContractedBy(10);
            scrollContainer.height -= ls.CurHeight;
            scrollContainer.y += ls.CurHeight;
            Widgets.DrawBoxSolid(scrollContainer, Color.grey);
            var innerContainer = scrollContainer.ContractedBy(1);
            Widgets.DrawBoxSolid(innerContainer, new ColorInt(42, 43, 44).ToColor);
            var frameRect = innerContainer.ContractedBy(5);
            frameRect.y += 15;
            frameRect.height -= 15;
            var contentRect = frameRect;
            contentRect.x = 0;
            contentRect.y = 0;
            contentRect.width -= 20;

            int numberOfMutators =  (from k in DefDatabase<TileMutatorDef>.AllDefsListForReading
                                                                                                where (k.chanceOnNonLandmarkTile>0)
                                                                                                select k).Count(); ;
            contentRect.height = numberOfMutators * 24f +350;

            Listing_Standard ls2 = new Listing_Standard();

            Widgets.BeginScrollView(frameRect, ref scrollPosition, contentRect, true);
            ls2.Begin(contentRect.AtZero());

            ls2.CheckboxLabeled("AB_RemoveOdysseyBiomes".Translate(), ref AB_RemoveOdysseyBiomes, "AB_RemoveOdysseyBiomesDesc".Translate());
            var mutatorLabel = ls2.LabelPlusButton("AB_MutatorMultiplier".Translate() + ": " + mutatorMultiplier + "x", "AB_MutatorMultiplierTooltip".Translate());
            mutatorMultiplier = (float)Math.Round(ls2.Slider(mutatorMultiplier, 0f, 20f), 2);

            if (ls2.Settings_Button("AB_Reset_Plain".Translate(), new Rect(0f, mutatorLabel.position.y + 35, 250f, 29f)))
            {
                mutatorMultiplier = mutatorMultiplierBase;
            }
            var landmarkMutatorLabel = ls2.LabelPlusButton("AB_LandmarkMutatorMultiplier".Translate() + ": " + landmarkMutatorMultiplier + "x", "AB_LandmarkMutatorMultiplierTooltip".Translate());
            landmarkMutatorMultiplier = (float)Math.Round(ls2.Slider(landmarkMutatorMultiplier, 0f, 20f), 2);

            if (ls2.Settings_Button("AB_Reset_Plain".Translate(), new Rect(0f, landmarkMutatorLabel.position.y + 35, 250f, 29f)))
            {
                landmarkMutatorMultiplier = landmarkMutatorMultiplierBase;
            }

            ls2.GapLine();
            Text.Font = GameFont.Medium;
            ls2.Label("AB_SpecificMutatorCommonalities".Translate());
            Text.Font = GameFont.Small;
            ls2.Gap();

            List<string> mutatorKeys = mutatorCommonalities.Keys.ToList().OrderBy(x => DefDatabase<TileMutatorDef>.GetNamedSilentFail(x).label).ToList();

            if (mutatorKeys.Count > 0)
            {             
                for (int num = 0; num < mutatorKeys.Count; num++)
                {
                    if (mutatorKeys[num]!="" && DefDatabase<TileMutatorDef>.GetNamedSilentFail(mutatorKeys[num]) !=null)
                    {
                        TileMutatorDef mutator = DefDatabase<TileMutatorDef>.GetNamedSilentFail(mutatorKeys[num]);
                        var labelRect = new Rect(0, (num + 1) * 24+250, 250, 24);
                        Widgets.Label(labelRect, mutator.LabelCap + ": "+ mutatorCommonalities[mutatorKeys[num]].ToStringPercent("F1"));
                        TooltipHandler.TipRegion(labelRect, mutator.description);
                        var sliderRect = new Rect(350, (num + 1) * 24+250, inRect.width - 450f, 24);
                        mutatorCommonalities[mutatorKeys[num]] = (float)Math.Round(Widgets.HorizontalSlider(sliderRect, mutatorCommonalities[mutatorKeys[num]], 0f, 1f), 3);
                    }
                    else
                    {
                        mutatorCommonalities.Remove(mutatorKeys[num]);
                    }
                }
            }

            if (ls2.Settings_Button("AB_Reset_Plain".Translate(), new Rect(0, (mutatorKeys.Count + 1) * 24 + 250, 250, 24)))
            {
                for (int num = 0; num < mutatorKeys.Count; num++)
                {
                    mutatorCommonalities[mutatorKeys[num]] = DefDatabase<TileMutatorDef>.AllDefsListForReading.Where(x => x.defName == mutatorKeys[num]).Select(x => x.chanceOnNonLandmarkTile).First();

                }
            }

            ls2.End();
            Widgets.EndScrollView();
            base.Write();
        }
    }
}
