using System;
using System.Collections.Generic;
using System.Linq;

using RimWorld;
using UnityEngine;
using Verse;

namespace AlphaBiomes
{
    internal class Designator_AlphaBiomes : Designator_Cells
    {


        

        public Designator_AlphaBiomes()
        {
            defaultLabel = "AB_BiomeBuildings".Translate();
            defaultDesc = "AB_BiomeBuildingsDesc".Translate();
            icon = ContentFinder<Texture2D>.Get("UI/Commands/AB_BiomeGizmo", true);
            soundDragSustain = SoundDefOf.Designate_DragStandard;
            soundDragChanged = SoundDefOf.Designate_DragStandard_Changed;
            useMouseIcon = true;
        }

        public override AcceptanceReport CanDesignateCell(IntVec3 c)
        {
            if (!c.InBounds(Map))
            {
                return false;
            }
            return true;
        }

        public override void ProcessInput(Event ev)
        {
            if (!CheckCanInteract())
            {
                return;
            }
            Window_BiomeBuildings biomeBuildingsWindow = new Window_BiomeBuildings();
            Find.WindowStack.Add(biomeBuildingsWindow);

        }

      


    }
}