
using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Verse;


namespace AlphaBiomes
{
    class CompRedAlcyioniteSolarConverter : ThingComp
    {



        public CompProperties_AlcyioniteSolarConverter Props
        {
            get
            {
                return (CompProperties_AlcyioniteSolarConverter)this.props;
            }
        }

        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {



            if (this.parent.Map?.Biome == InternalDefOf.AB_OcularForest && this.parent.Faction == Faction.OfPlayerSilentFail)
            {
                Command_Action command_Action = new Command_Action();
                command_Action.defaultLabel = "AB_ConvertToAlcyonite".Translate();
                command_Action.defaultDesc = "AB_ConvertToAlcyoniteDesc".Translate();
                command_Action.icon = ContentFinder<Texture2D>.Get("UI/Commands/AB_RedSolarCells_Gizmo", true);
                command_Action.action = delegate
                {
                    ThingDef defToMake = null;
                    if(this.parent.def == ThingDefOf.SolarGenerator)
                    {
                        defToMake = InternalDefOf.AB_AlcyoniteSolar;
                    }
                    if (this.parent.def == InternalDefOf.VFE_AdvancedSolarGenerator)
                    {
                        defToMake = InternalDefOf.AB_AlcyoniteSolar_Advanced;
                    }

                    if (defToMake != null)
                    {
                        Thing panelToMake = GenSpawn.Spawn(ThingMaker.MakeThing(defToMake), parent.Position, parent.Map);

                        if (panelToMake.def.CanHaveFaction)
                        {
                            panelToMake.SetFaction(this.parent.Faction);
                        }

                        if (this.parent.Spawned)
                        {
                            this.parent.Destroy();
                        }

                    }
                    
                };
                yield return command_Action;

            }

        }
    }
}
