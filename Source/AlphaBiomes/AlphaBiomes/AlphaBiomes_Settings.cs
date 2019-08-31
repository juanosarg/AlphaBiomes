using RimWorld;
using UnityEngine;
using Verse;


namespace AlphaBiomes
{
    public class AlphaBiomes_Settings : ModSettings

    {


        public static bool AB_UseAgariluxParticles = true;
      


        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(ref AB_UseAgariluxParticles, "AB_UseAgariluxParticles", true, true);
           

        }
        public static void DoWindowContents(Rect inRect)
        {
            Listing_Standard ls = new Listing_Standard();


            ls.Begin(inRect);
            ls.ColumnWidth = inRect.width / 2.05f;

            ls.CheckboxLabeled("AB_UseAgariluxParticles".Translate(), ref AB_UseAgariluxParticles, null);

           

            ls.End();

        }




    }










}
