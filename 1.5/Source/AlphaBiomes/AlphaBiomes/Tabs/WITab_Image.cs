
using RimWorld;
using RimWorld.Planet;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using System.Collections.Generic;
using Verse;

namespace AlphaBiomes
{
    public class WITab_Image : WITab
    {
      
        private static readonly Vector2 WinSize = new Vector2(440f, 540f);

        public override bool IsVisible => (base.SelTileID >= 0) && base.SelTile.biome.defName.Contains("AB_");

        public WITab_Image()
        {
            size = WinSize;
            labelKey = "AB_TabImage";
           
        }

        protected override void FillTab()
        {
            Rect rect = new Rect(0f, 0f, WinSize.x, WinSize.y).ContractedBy(10f);
            Text.Font = GameFont.Medium;
            Widgets.Label(rect, base.SelTile.biome.LabelCap);
            Rect rect2 = rect;
            rect2.yMin += 35f;
            Text.Font = GameFont.Small;
            List<string> results = InternalDefOf.AB_BiomeImageDef.biomeImages.Where(x => x.biome == base.SelTile.biome)?.FirstOrFallback()?.images;
            int? count = results?.Count;
            if(count > 0) {
                string result = results[base.SelTileID % (int)count];

                GUI.DrawTexture(rect2, ContentFinder<Texture2D>.Get("UI/BiomeImages/" + result), ScaleMode.ScaleToFit, true);

            }
            
            

        }
    }
}