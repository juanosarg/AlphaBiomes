using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;
using RimWorld.BaseGen;
using System.Collections.Generic;
using RimWorld.Planet;
using static UnityEngine.GridBrushBase;
using Verse.Sound;
using Verse.Steam;
using System.Linq;
using UnityEngine.Tilemaps;


namespace AlphaBiomes
{
    [HarmonyPatch(typeof(WorldInterface))]
    [HarmonyPatch("CheckOpenOrCloseInspectPane")]

    public static class AlphaBiomes_WorldInterface_CheckOpenOrCloseInspectPane_Patch
    {

        public static Rect InfoRectProps = new Rect((UI.screenWidth - 400), 0, 400, 270f);
        public static int columnCount = 4;
        public static int iconSize = 80;
        private static readonly Color borderColor = new Color(0.13f, 0.13f, 0.13f);
        private static readonly Color fillColor = new Color(0, 0, 0, 0.1f);

        [HarmonyPostfix]
        public static void PopUpPanel(WorldInterface __instance)

        {
            if (ModsConfig.OdysseyActive && __instance?.selector?.AnyObjectOrTileSelected==true && __instance?.selector?.SelectedTile.Tile?.Mutators?.Count>0 && WorldRendererUtility.WorldSelected && (Current.ProgramState != ProgramState.Playing || Find.MainTabsRoot.OpenTab == null))
            {
                Find.WindowStack.ImmediateWindow(32520, InfoRectProps, WindowLayer.GameUI, delegate
            {

                Rect rect = InfoRectProps.AtZero().ContractedBy(7f);
              
                Text.Anchor = TextAnchor.UpperCenter;
                Text.Font = GameFont.Medium;
                Widgets.Label(rect, "AB_FeaturesInThisTile".Translate());
                Text.Anchor = TextAnchor.UpperLeft;
                Text.Font = GameFont.Small;

                Rect allIconsRect = new Rect(20, 60, 300, 200f);
                RimWorld.Planet.Tile tile = __instance?.selector?.SelectedTile.Tile;
                if (tile != null) {
                    List<TileMutatorDef> mutators = tile.Mutators?.ToList();
                    
                    InfoRectProps.height = (((mutators.Count-1) / 4) +1)*100 +70;
                    
                    for (int i = 0; i < mutators.Count; i++)
                    {
                        Rect rectIcon = new Rect((allIconsRect.x + iconSize * (i % columnCount)) + 12 * (i % columnCount), allIconsRect.y + (iconSize * (i / columnCount) + 20 * ((i / columnCount) + 1)), iconSize, iconSize);
                        Widgets.DrawBoxSolidWithOutline(rectIcon, fillColor, borderColor, 2);
                        Rect rectIconInside = rectIcon.ContractedBy(2);
                        string imageSrc;
                        if (StaticCollections.tileMutatorIcons.ContainsKey(mutators[i]))
                        {
                            imageSrc = StaticCollections.tileMutatorIcons[mutators[i]];
                        }
                        else
                        {
                            imageSrc = "UI/Icons/AB_MutatorIcons/AB_MutatorIcon_Empty";
                        }

                        GUI.DrawTexture(rectIconInside, ContentFinder<Texture2D>.Get(imageSrc, true), ScaleMode.ScaleToFit, alphaBlend: true, 0f, Color.white, 0f, 0f);
                        TooltipHandler.TipRegion(rectIcon, mutators[i].Label(tile.tile).Colorize(ColoredText.TipSectionTitleColor).CapitalizeFirst() + "\n" + mutators[i].Description(tile.tile));


                    }
                }

                
        });
            }




        }

    }


}