
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Analytics;
using Verse;
namespace AlphaBiomes
{
    public class GameCondition_AmbientRadiation : GameCondition
    {

        public override void GameConditionTick()
        {
            base.GameConditionTick();

            if (Find.TickManager.TicksGame % 900000 == 0)
            {

                foreach (Map map in base.AffectedMaps)
                {
                    List<Pawn> pawns = map.mapPawns.FreeColonistsSpawned;
                    if (pawns.Any())
                    {
                        Pawn pawnAffected = pawns.RandomElement();
                        GeneDef gene = DefDatabase<GeneDef>.AllDefs.Where((GeneDef x) => !x.defName.Contains("AnimalSum") && x.exclusionTags?.Contains("AG_OnlyOnCharacterCreation") == false &&
                        x.prerequisite == null && x.biostatArc == 0 && x.modContentPack?.PackageId != "vanillaracesexpanded.insector").RandomElement();

                        pawnAffected.genes?.AddGene(gene, true);
                        Find.LetterStack.ReceiveLetter("AB_AmbientRadiationLabel".Translate(pawnAffected.NameShortColored), "AB_AmbientRadiationDesc".Translate(pawnAffected.NameShortColored, gene.LabelCap), LetterDefOf.NeutralEvent, (TargetInfo)pawnAffected);

                    }
                }

            }
        }

    }
}
