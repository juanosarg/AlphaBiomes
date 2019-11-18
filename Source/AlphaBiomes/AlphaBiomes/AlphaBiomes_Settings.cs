using RimWorld;
using UnityEngine;
using Verse;


namespace AlphaBiomes
{
    public class AlphaBiomes_Settings : ModSettings

    {


        public static bool AB_UseAgariluxParticles = true;

        public static bool AB_SpawnFeraliskInfestedJungle = true;
        public static bool AB_SpawnGallatrossGraveyard = true;
        public static bool AB_SpawnGelatinousSuperorganism = true;
        public static bool AB_SpawnMechanoidIntrusion = true;
        public static bool AB_SpawnMycoticJungle = true;
        public static bool AB_SpawnOcularForest = true;
        public static bool AB_SpawnPropaneLakes = true;
        public static bool AB_SpawnRockyCrags = true;

        public static bool AB_RemoveVanillaBiomes = false;







        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(ref AB_UseAgariluxParticles, "AB_UseAgariluxParticles", true, true);
            Scribe_Values.Look(ref AB_SpawnFeraliskInfestedJungle, "AB_SpawnFeraliskInfestedJungle", true, true);
            Scribe_Values.Look(ref AB_SpawnGallatrossGraveyard, "AB_SpawnGallatrossGraveyard", true, true);
            Scribe_Values.Look(ref AB_SpawnGelatinousSuperorganism, "AB_SpawnGelatinousSuperorganism", true, true);
            Scribe_Values.Look(ref AB_SpawnMechanoidIntrusion, "AB_SpawnMechanoidIntrusion", true, true);
            Scribe_Values.Look(ref AB_SpawnMycoticJungle, "AB_SpawnMycoticJungle", true, true);
            Scribe_Values.Look(ref AB_SpawnOcularForest, "AB_SpawnOcularForest", true, true);
            Scribe_Values.Look(ref AB_SpawnPropaneLakes, "AB_SpawnPropaneLakes", true, true);
            Scribe_Values.Look(ref AB_SpawnRockyCrags, "AB_SpawnRockyCrags", true, true);
            Scribe_Values.Look(ref AB_RemoveVanillaBiomes, "AB_RemoveVanillaBiomes", false, true);





        }
        public static void DoWindowContents(Rect inRect)
        {
            Listing_Standard ls = new Listing_Standard();


            ls.Begin(inRect);
            ls.ColumnWidth = inRect.width / 2.05f;

            ls.CheckboxLabeled("AB_UseAgariluxParticles".Translate(), ref AB_UseAgariluxParticles, null);
            ls.CheckboxLabeled("AB_SpawnFeraliskInfestedJungle".Translate(), ref AB_SpawnFeraliskInfestedJungle, null);
            ls.CheckboxLabeled("AB_SpawnGallatrossGraveyard".Translate(), ref AB_SpawnGallatrossGraveyard, null);
            ls.CheckboxLabeled("AB_SpawnGelatinousSuperorganism".Translate(), ref AB_SpawnGelatinousSuperorganism, null);
            ls.CheckboxLabeled("AB_SpawnMechanoidIntrusion".Translate(), ref AB_SpawnMechanoidIntrusion, null);
            ls.CheckboxLabeled("AB_SpawnMycoticJungle".Translate(), ref AB_SpawnMycoticJungle, null);
            ls.CheckboxLabeled("AB_SpawnOcularForest".Translate(), ref AB_SpawnOcularForest, null);
            ls.CheckboxLabeled("AB_SpawnPropaneLakes".Translate(), ref AB_SpawnPropaneLakes, null);
            ls.CheckboxLabeled("AB_SpawnRockyCrags".Translate(), ref AB_SpawnRockyCrags, null);
            ls.CheckboxLabeled("AB_RemoveVanillaBiomes".Translate(), ref AB_RemoveVanillaBiomes, null);



            ls.End();

        }




    }










}
