
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
namespace AlphaBiomes
{
    public class TileMutatorWorker_AgariluxPrime : TileMutatorWorker
    {
        public TileMutatorWorker_AgariluxPrime(TileMutatorDef def)
            : base(def)
        {
        }

        public override void GeneratePostFog(Map map)
        {
            List<CellRect> usedRects = MapGenerator.GetOrGenerateVar<List<CellRect>>("UsedRects");
            PrefabDef prefab = InternalDefOf.AB_AgariluxPrime_Prefab;
            if (!MapGenUtility.TryGetRandomClearRect(prefab.size.x, prefab.size.z, out var rect, -1, -1, Validator))
            {
                if (!CellFinder.TryFindRandomCell(map, (IntVec3 c) => Validator(CellRect.CenteredOn(c, prefab.size)), out var cell2))
                {
                    return;
                }
                rect = CellRect.CenteredOn(cell2, prefab.size);
            }
            foreach (IntVec3 cell in rect)
            {
                map.terrainGrid.SetTerrain(cell, TerrainDefOf.Soil);
            }
            PrefabUtility.SpawnPrefab(prefab, map, GetPrefabRoot(rect), Rot4.North);
            usedRects.Add(rect);
            bool Validator(CellRect r)
            {
                if (!r.InBounds(map))
                {
                    return false;
                }
                if (r.Cells.Any((IntVec3 c) => c.Fogged(map)))
                {
                    return false;
                }
                if (!PrefabUtility.CanSpawnPrefab(prefab, map, GetPrefabRoot(r), Rot4.North, canWipeEdifices: false))
                {
                    return false;
                }
                return !usedRects.Any((CellRect ur) => ur.Overlaps(r));
            }
        }

        private IntVec3 GetPrefabRoot(CellRect rect)
        {
            IntVec3 center = rect.CenterCell;
            if (rect.Width % 2 == 0)
            {
                center.x--;
            }
            if (rect.Height % 2 == 0)
            {
                center.z--;
            }
            return center;
        }
    }
}