using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using UnityEngine.Diagnostics;
using Verse;

namespace AlphaBiomes
{
    public class Window_BiomeBuildings : Window
    {

        public static Rect InfoRectProps => new Rect(0f, (float)(UI.screenHeight - 35) - ((MainTabWindow_Architect)MainButtonDefOf.Architect.TabWindow).WinHeight - 270f, 200f, 270f);


        public static Color tooltipColour = Color.yellow;
        public override Vector2 InitialSize => new Vector2(620f, 500f);
        private Vector2 scrollPosition = new Vector2(0, 0);
        public int columnCount = 4;
        private static readonly Color borderColor = new Color(0.13f, 0.13f, 0.13f);
        private static readonly Color fillColor = new Color(0, 0, 0, 0.1f);
        private bool writeStuff;

        public Window_BiomeBuildings()
        {

            // closeOnClickedOutside = true;
            draggable = true;
            resizeable = true;
            preventCameraMotion = false;
        }

        public override void DoWindowContents(Rect inRect)
        {

            var outRect = new Rect(inRect);
            outRect.yMin += 40f;
            outRect.yMax -= 40f;
            outRect.width -= 16f;

            BiomeDef biome = Find.CurrentMap.Biome;

            Text.Font = GameFont.Medium;
            var IntroLabel = new Rect(0, 0, 500, 32f);
            Widgets.Label(IntroLabel, "AM_CurrentBiome".Translate() + biome.LabelCap.Colorize(tooltipColour));
            var SecondIntroLabel = new Rect(0, 40, 500, 32f);
            Widgets.Label(SecondIntroLabel, "AM_AvailableStructures".Translate());

            if (Widgets.ButtonImage(new Rect(outRect.xMax - 18f - 4f, 2f, 18f, 18f), TexButton.CloseXSmall))
            {
                Close();
            }

            List<StructureToBiome> applicableStructures = StaticCollections.structureToBiomes.Where(x => x.biome == biome || x.allBiomes || Find.CurrentMap.Tile.Tile.Mutators.Contains(x.mutator)).ToList();

            if (applicableStructures.Count == 0)
            {
                var EmptyLabel = new Rect(0, 80, 300, 32f);
                Widgets.Label(EmptyLabel, "AM_None".Translate());

            }
            Text.Font = GameFont.Small;
            var scrollRect = new Rect(0f, 74, outRect.width, outRect.height);

            var viewRect = new Rect(0f, 74, outRect.width - 16f, 104 * ((applicableStructures.Count / columnCount) + 1) + 200);
            Widgets.BeginScrollView(scrollRect, ref scrollPosition, viewRect);
            try
            {

                for (var i = 0; i < applicableStructures.Count; i++)
                {
                    StructureToBiome item = applicableStructures[i];
                    BuildableDef structure = item.structure;

                    Rect rectIcon = new Rect((128 * (i % columnCount)) + 12 * (i % columnCount), viewRect.y +  (128 * (i / columnCount) + 20 * ((i / columnCount) + 1)), 128, 128);

                    Widgets.DrawBoxSolidWithOutline(rectIcon, fillColor, borderColor, 2);
                    Rect rectIconInside = rectIcon.ContractedBy(2);

                    GUI.DrawTexture(rectIconInside, structure is null ? ContentFinder<Texture2D>.Get(item.iconOverride, true) : structure.uiIcon, ScaleMode.ScaleToFit, alphaBlend: true, 0f, structure is null ? Color.white : structure.uiIconColor, 0f, 0f);
                    if (structure != null && Mouse.IsOver(rectIconInside))
                    {
                        DoInfoBox(InfoRectProps, new Designator_Build(structure));
                    }

                    bool researchUnfinished = false;
                    string researchNeeded = "";
                    if (structure?.researchPrerequisites?.Count > 0)
                    {
                        foreach (ResearchProjectDef research in structure.researchPrerequisites)
                        {
                            if (Find.ResearchManager.GetProgress(research) < 1)
                            {
                                researchUnfinished = true;
                                researchNeeded = research.LabelCap;
                            }
                        }
                        if (researchUnfinished)
                        {
                            GUI.DrawTexture(rectIconInside, ContentFinder<Texture2D>.Get("UI/Commands/AB_Blocked", true), ScaleMode.ScaleAndCrop, alphaBlend: true, 0f, Color.white, 0f, 0f);
                        }

                    }
                    if (researchUnfinished)
                    {
                        TooltipHandler.TipRegion(rectIcon, "AB_ResearchNeeded".Translate(researchNeeded));
                    }
                    else
                    {
                        TooltipHandler.TipRegion(rectIcon, structure is null ? item.descOverride.Translate() : structure.LabelCap + ": " + structure.description);

                    }
                    Text.Font = GameFont.Tiny;
                    var categoryTextRect = new Rect((128 * (i % columnCount)) + 12 * (i % columnCount), viewRect.y + 128 + (128 * (i / columnCount) + 20 * ((i / columnCount) + 1)), 128, 20);
                    Widgets.Label(categoryTextRect, structure is null ? item.labelOverride.Translate() : structure.LabelCap);

                    if (Widgets.ButtonInvisible(rectIcon) && !researchUnfinished)
                    {

                        if (structure != null)
                        {
                            if (structure.stuffCategories != null)
                            {
                                CreateDropdown(structure);
                            }
                            else
                            {
                                CreateDesignator(structure, null);
                            }
                        }
                        else
                        {
                            Messages.Message(item.notBuildableMessage.Translate(), null, MessageTypeDefOf.RejectInput, true);


                        }





                    }

                }




            }
            finally
            {
                Widgets.EndScrollView();
            }
        }


        public static void DoInfoBox(Rect infoRect, Designator_Build designator)
        {



            Find.WindowStack.ImmediateWindow(32519, infoRect, WindowLayer.GameUI, delegate
            {
                if (designator != null)
                {
                    Rect rect = infoRect.AtZero().ContractedBy(7f);
                    Widgets.BeginGroup(rect);
                    Rect rect2 = new Rect(0f, 0f, rect.width - designator.PanelReadoutTitleExtraRightMargin, 999f);
                    Text.Font = GameFont.Small;
                    Widgets.Label(rect2, designator.LabelCap);
                    float curY = Mathf.Max(24f, Text.CalcHeight(designator.LabelCap, rect2.width));
                    designator.DrawPanelReadout(ref curY, rect.width);
                    Rect rect3 = new Rect(0f, curY, rect.width, rect.height - curY);
                    string desc = designator.Desc;
                    GenText.SetTextSizeToFit(desc, rect3);
                    desc = desc.TruncateHeight(rect3.width, rect3.height);
                    Widgets.Label(rect3, desc);
                    Widgets.EndGroup();
                }

            });
        }

        public void CreateDesignator(BuildableDef thingdef, ThingDef stuff)
        {
            Designator_Build designator = new Designator_Build(thingdef);
            if (stuff != null)
            {
                designator.SetStuffDef(stuff);
            }
            Find.DesignatorManager.Select(designator);
        }

        public void CreateDropdown(BuildableDef thingDef)
        {

            if (thingDef == null || !thingDef.MadeFromStuff)
            {
                CreateDesignator(thingDef, null);
                return;
            }
            List<FloatMenuOption> list = new List<FloatMenuOption>();
            foreach (ThingDef item in from d in Find.CurrentMap.resourceCounter.AllCountedAmounts.Keys
                                      orderby d.stuffProps?.commonality ?? float.PositiveInfinity descending, d.BaseMarketValue
                                      select d)
            {
                if (item.IsStuff && item.stuffProps.CanMake(thingDef) && (DebugSettings.godMode || Find.CurrentMap.listerThings.ThingsOfDef(item).Count > 0))
                {
                    ThingDef localStuffDef = item;
                    string str = GenLabel.ThingLabel(thingDef, localStuffDef);
                    str = str.CapitalizeFirst();
                    FloatMenuOption floatMenuOption = new FloatMenuOption(str, delegate
                    {
                        CreateDesignator(thingDef, item);
                    }, item);
                    floatMenuOption.tutorTag = "SelectStuff-" + thingDef.defName + "-" + localStuffDef.defName;
                    list.Add(floatMenuOption);
                }
            }
            if (list.Count == 0)
            {
                Messages.Message("NoStuffsToBuildWith".Translate(), MessageTypeDefOf.RejectInput, historical: false);
                return;
            }
            FloatMenu floatMenu = new FloatMenu(list);
            floatMenu.onCloseCallback = delegate
            {
                writeStuff = true;
            };
            Find.WindowStack.Add(floatMenu);

        }
    }
}