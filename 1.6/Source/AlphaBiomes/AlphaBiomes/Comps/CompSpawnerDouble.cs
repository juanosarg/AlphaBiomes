using System;
using System.Collections.Generic;
using Verse;
using RimWorld;

namespace AlphaBiomes
{
    public class CompSpawnerDouble : ThingComp
    {
        public CompProperties_SpawnerDouble PropsSpawner
        {
            get
            {
                return (CompProperties_SpawnerDouble)this.props;
            }
        }

        private bool PowerOn
        {
            get
            {
                CompPowerTrader comp = this.parent.GetComp<CompPowerTrader>();
                return comp != null && comp.PowerOn;
            }
        }

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            if (!respawningAfterLoad)
            {
                this.ResetCountdown();
            }
        }

        public override void CompTick()
        {
            this.TickInterval(1);
        }

        public override void CompTickRare()
        {
            this.TickInterval(250);
        }

        private void TickInterval(int interval)
        {
            if (!this.parent.Spawned)
            {
                return;
            }
            CompCanBeDormant comp = this.parent.GetComp<CompCanBeDormant>();
            if (comp != null)
            {
                if (!comp.Awake)
                {
                    return;
                }
            }
            else if (this.parent.Position.Fogged(this.parent.Map))
            {
                return;
            }
            if (this.PropsSpawner.requiresPower && !this.PowerOn)
            {
                return;
            }
            this.ticksUntilSpawn -= interval;
            this.CheckShouldSpawn();
        }

        private void CheckShouldSpawn()
        {
            if (this.ticksUntilSpawn <= 0)
            {
                this.TryDoSpawn();
                this.ResetCountdown();
            }
        }

        public bool TryDoSpawn()
        {
            if (!this.parent.Spawned)
            {
                return false;
            }
            if (this.PropsSpawner.spawnMaxAdjacent >= 0)
            {
                int num = 0;
                for (int i = 0; i < 9; i++)
                {
                    IntVec3 c = this.parent.Position + GenAdj.AdjacentCellsAndInside[i];
                    if (c.InBounds(this.parent.Map))
                    {
                        List<Thing> thingList = c.GetThingList(this.parent.Map);
                        for (int j = 0; j < thingList.Count; j++)
                        {
                            if (thingList[j].def == this.PropsSpawner.thingToSpawn)
                            {
                                num += thingList[j].stackCount;
                                if (num >= this.PropsSpawner.spawnMaxAdjacent)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            IntVec3 center;
            if (CompSpawnerDouble.TryFindSpawnCell(this.parent, this.PropsSpawner.thingToSpawn, this.PropsSpawner.spawnCount, out center))
            {
                Thing thing = ThingMaker.MakeThing(this.PropsSpawner.thingToSpawn, null);
               
                Thing thing2 = ThingMaker.MakeThing(this.PropsSpawner.SecondaryThingToSpawn, null);
                Thing thingHole = this.parent.Map.thingGrid.ThingAt(this.parent.Position, InternalDefOf.AB_TarHole);
                if (thingHole == null || thingHole.Position != this.parent.Position)
                {
                    thing.stackCount = (int)(this.PropsSpawner.spawnCount / 3);
                    thing2.stackCount = (int)(this.PropsSpawner.secondarySpawnCount / 3);
                } else
                {
                    thing.stackCount = this.PropsSpawner.spawnCount;
                    thing2.stackCount = this.PropsSpawner.secondarySpawnCount;

                }

                
                if (thing == null || thing2 == null)
                {
                    Log.Error("Could not spawn anything for " + this.parent);
                }
                if (this.PropsSpawner.inheritFaction && thing.Faction != this.parent.Faction)
                {
                    thing.SetFaction(this.parent.Faction, null);
                }
                if (this.PropsSpawner.inheritFaction && thing2.Faction != this.parent.Faction)
                {
                    thing2.SetFaction(this.parent.Faction, null);
                }
                Thing t;
                GenPlace.TryPlaceThing(thing, center, this.parent.Map, ThingPlaceMode.Direct, out t, null, null, default(Rot4));
                Thing t2;
                GenPlace.TryPlaceThing(thing2, this.parent.InteractionCell, this.parent.Map, ThingPlaceMode.Direct, out t2, null, null, default(Rot4));

                if (this.PropsSpawner.spawnForbidden)
                {
                    t.SetForbidden(true, true);
                    t2.SetForbidden(true, true);
                }
                if (this.PropsSpawner.showMessageIfOwned && this.parent.Faction == Faction.OfPlayer)
                {
                    Messages.Message("MessageCompSpawnerSpawnedItem".Translate(this.PropsSpawner.thingToSpawn.LabelCap), thing, MessageTypeDefOf.PositiveEvent, true);
                }
                return true;
            }
            return false;
        }

        public static bool TryFindSpawnCell(Thing parent, ThingDef thingToSpawn, int spawnCount, out IntVec3 result)
        {
            foreach (IntVec3 intVec in GenAdj.CellsAdjacent8Way(parent).InRandomOrder(null))
            {
                if (intVec.Walkable(parent.Map))
                {
                    Building edifice = intVec.GetEdifice(parent.Map);
                    if (edifice == null || !thingToSpawn.IsEdifice())
                    {
                        Building_Door building_Door = edifice as Building_Door;
                        if ((building_Door == null || building_Door.FreePassage) && (parent.def.passability == Traversability.Impassable || GenSight.LineOfSight(parent.Position, intVec, parent.Map, false, null, 0, 0)))
                        {
                            bool flag = false;
                            List<Thing> thingList = intVec.GetThingList(parent.Map);
                            for (int i = 0; i < thingList.Count; i++)
                            {
                                Thing thing = thingList[i];
                                if (thing.def.category == ThingCategory.Item && (thing.def != thingToSpawn || thing.stackCount > thingToSpawn.stackLimit - spawnCount))
                                {
                                    flag = true;
                                    break;
                                }
                            }
                            if (!flag)
                            {
                                result = intVec;
                                return true;
                            }
                        }
                    }
                }
            }
            result = IntVec3.Invalid;
            return false;
        }

        private void ResetCountdown()
        {
            this.ticksUntilSpawn = this.PropsSpawner.spawnIntervalRange.RandomInRange;
        }

        public override void PostExposeData()
        {
            string str = this.PropsSpawner.saveKeysPrefix.NullOrEmpty() ? null : (this.PropsSpawner.saveKeysPrefix + "_");
            Scribe_Values.Look<int>(ref this.ticksUntilSpawn, str + "ticksUntilSpawn", 0, false);
        }

        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            if (Prefs.DevMode)
            {
                yield return new Command_Action
                {
                    defaultLabel = "DEBUG: Spawn " + this.PropsSpawner.thingToSpawn.label,
                    icon = TexCommand.DesirePower,
                    action = delegate ()
                    {
                        this.TryDoSpawn();
                        this.ResetCountdown();
                    }
                };
            }
            yield break;
        }

        public override string CompInspectStringExtra()
        {
            string txt = "";
            bool holeHere = true;
            Thing thingHole = this.parent.Map.thingGrid.ThingAt(this.parent.Position, ThingDef.Named("AB_TarHole"));
            if (thingHole == null || thingHole.Position != this.parent.Position)
            {
                txt += "AB_WarningLowEfficiency".Translate();
                holeHere = false;
            }

            if (this.PropsSpawner.writeTimeLeftToSpawn && (!this.PropsSpawner.requiresPower || this.PowerOn))
            {
                if (!holeHere) {
                    txt += "\n"+ "NextSpawnedItemIn".Translate(GenLabel.ThingLabel(this.PropsSpawner.thingToSpawn, null, (int)(this.PropsSpawner.spawnCount/3))) + ": " + this.ticksUntilSpawn.ToStringTicksToPeriod(true, false, true, true) +
                   "\n" + "NextSpawnedItemIn".Translate(GenLabel.ThingLabel(this.PropsSpawner.SecondaryThingToSpawn, null, (int)(this.PropsSpawner.secondarySpawnCount / 3))) + ": " + this.ticksUntilSpawn.ToStringTicksToPeriod(true, false, true, true);

                } else
                {
                    txt += "NextSpawnedItemIn".Translate(GenLabel.ThingLabel(this.PropsSpawner.thingToSpawn, null, this.PropsSpawner.spawnCount)) + ": " + this.ticksUntilSpawn.ToStringTicksToPeriod(true, false, true, true) +
"\n" + "NextSpawnedItemIn".Translate(GenLabel.ThingLabel(this.PropsSpawner.SecondaryThingToSpawn, null, this.PropsSpawner.secondarySpawnCount)) + ": " + this.ticksUntilSpawn.ToStringTicksToPeriod(true, false, true, true);
                }
               
            }
            if (txt != "") { return txt; }else return null;
        }

        private int ticksUntilSpawn;
    }
}
