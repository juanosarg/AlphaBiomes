using System;
using UnityEngine;
using Verse;
using RimWorld;

namespace AlphaBiomes
{
    [StaticConstructorOnStartup]
    public class SectionLayer_AsphaltBridges : SectionLayer
    {
        public override bool Visible
        {
            get
            {
                return DebugViewSettings.drawTerrain;
            }
        }

        public SectionLayer_AsphaltBridges(Section section) : base(section)
        {
            this.relevantChangeTypes = MapMeshFlag.Terrain;
        }

        public override void Regenerate()
        {
            base.ClearSubMeshes(MeshParts.All);
            Map map = base.Map;
            TerrainGrid terrainGrid = map.terrainGrid;
            CellRect cellRect = this.section.CellRect;
            float y = AltitudeLayer.TerrainScatter.AltitudeFor();
            foreach (IntVec3 intVec in cellRect)
            {
                if (this.ShouldDrawPropsBelow(intVec, terrainGrid))
                {
                    IntVec3 c = intVec;
                    c.x++;
                    Material material;
                    if (c.InBounds(map) && this.ShouldDrawPropsBelow(c, terrainGrid))
                    {
                        material = SectionLayer_AsphaltBridges.PropsLoopMat;
                    }
                    else
                    {
                        material = SectionLayer_AsphaltBridges.PropsRightMat;
                    }
                    LayerSubMesh subMesh = base.GetSubMesh(material);
                    int count = subMesh.verts.Count;
                    subMesh.verts.Add(new Vector3((float)intVec.x, y, (float)(intVec.z - 1)));
                    subMesh.verts.Add(new Vector3((float)intVec.x, y, (float)intVec.z));
                    subMesh.verts.Add(new Vector3((float)(intVec.x + 1), y, (float)intVec.z));
                    subMesh.verts.Add(new Vector3((float)(intVec.x + 1), y, (float)(intVec.z - 1)));
                    subMesh.uvs.Add(new Vector2(0f, 0f));
                    subMesh.uvs.Add(new Vector2(0f, 1f));
                    subMesh.uvs.Add(new Vector2(1f, 1f));
                    subMesh.uvs.Add(new Vector2(1f, 0f));
                    subMesh.tris.Add(count);
                    subMesh.tris.Add(count + 1);
                    subMesh.tris.Add(count + 2);
                    subMesh.tris.Add(count);
                    subMesh.tris.Add(count + 2);
                    subMesh.tris.Add(count + 3);
                }
            }
            base.FinalizeMesh(MeshParts.All);
        }

        private bool ShouldDrawPropsBelow(IntVec3 c, TerrainGrid terrGrid)
        {
            TerrainDef terrainDef = terrGrid.TerrainAt(c);
            if (terrainDef == null || terrainDef != InternalDefOf.AB_AsphaltBridge)
            {
                return false;
            }
            IntVec3 c2 = c;
            c2.z--;
            Map map = base.Map;
            if (!c2.InBounds(map))
            {
                return false;
            }
            TerrainDef terrainDef2 = terrGrid.TerrainAt(c2);
            return terrainDef2 != InternalDefOf.AB_AsphaltBridge && (terrainDef2.passability == Traversability.Impassable || c2.SupportsStructureType(map, InternalDefOf.AB_AsphaltBridge.terrainAffordanceNeeded));
        }

        private static readonly Material PropsLoopMat = MaterialPool.MatFrom("Terrain/Surfaces/AB_BridgeProps_Loop", ShaderDatabase.Transparent);

        private static readonly Material PropsRightMat = MaterialPool.MatFrom("Terrain/Surfaces/AB_BridgeProps_Right", ShaderDatabase.Transparent);
    }
}
