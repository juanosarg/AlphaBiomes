
using System.Collections.Generic;
using RimWorld;
using RimWorld.Planet;
using Verse;
namespace AlphaBiomes
{
    public class TileMutatorWorker_OnlyGrass : TileMutatorWorker
    {

        public TileMutatorWorker_OnlyGrass(TileMutatorDef def)
            : base(def)
        {
        }

        public override float PlantCommonalityFactorFor(ThingDef plant, PlanetTile tile)
        {
            return (plant.label.ToLower().Contains("grass")) ? 1f : 0f;
        }


    }
}