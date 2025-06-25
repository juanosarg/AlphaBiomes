

using RimWorld;
using Verse;
using Verse.Sound;
using System;
using System.Linq;
using RimWorld.Planet;
using Verse.Noise;
using UnityEngine.Tilemaps;

namespace AlphaBiomes
{
    public class HediffComp_DetectHeightened : HediffComp
    {

        public bool remove = false;
        public HediffCompProperties_DetectHeightened Props
        {
            get
            {
                return (HediffCompProperties_DetectHeightened)this.props;
            }
        }

        public override void CompExposeData()
        {
            base.CompExposeData();
            Scribe_Values.Look(ref remove, "remove", false);
        }

        public override void CompPostMake()
        {
            base.CompPostMake();

            Map map = Pawn.Map;
            if (map != null) {
                for (int i = 0; i < map.TileInfo.Mutators.Count; i++)
                {
                    if (map.TileInfo.Mutators[i] == InternalDefOf.AB_PollinationFrenzy)
                    {
                        Pawn.health.AddHediff(InternalDefOf.AB_SporesAllergy_Heightened);
                        remove = true;
                        
                    }
                  
                }
            }
            

        }

        public override void CompPostTick(ref float severityAdjustment)
        {
            base.CompPostTick(ref severityAdjustment);
            if (remove) {
                this.parent.pawn.health.RemoveHediff(this.parent);
            }
        }

    }
}

