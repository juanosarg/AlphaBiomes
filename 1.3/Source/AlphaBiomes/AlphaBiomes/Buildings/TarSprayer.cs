using System;
using Verse;
using RimWorld;
using UnityEngine;

namespace AlphaBiomes
{
    public class TarSprayer
    {
        public TarSprayer(Thing parent)
        {
            this.parent = parent;
        }

        public void SteamSprayerTick()
        {
            if (this.sprayTicksLeft > 0)
            {
                this.sprayTicksLeft--;
                if (Rand.Value < 0.6f)
                {
                    ThrowAirPuffUp(this.parent.TrueCenter(), this.parent.Map);
                }

                if (this.sprayTicksLeft <= 0)
                {
                    if (this.endSprayCallback != null)
                    {
                        this.endSprayCallback();
                    }
                    this.ticksUntilSpray = Rand.RangeInclusive(500, 2000);
                    return;
                }
            }
            else
            {
                this.ticksUntilSpray--;
                if (this.ticksUntilSpray <= 0)
                {
                    if (this.startSprayCallback != null)
                    {
                        this.startSprayCallback();
                    }
                    this.sprayTicksLeft = Rand.RangeInclusive(200, 500);
                }
            }
        }

        private static MoteThrown NewBaseAirPuff()
        {
            MoteThrown moteThrown = (MoteThrown)ThingMaker.MakeThing(InternalDefOf.AB_TarMote, null);
            moteThrown.Scale = 1.5f;
            moteThrown.rotationRate = (float)Rand.RangeInclusive(-240, 240);
            return moteThrown;
        }

        public static void ThrowAirPuffUp(Vector3 loc, Map map)
        {
            if (!loc.ToIntVec3().ShouldSpawnMotesAt(map) || map.moteCounter.SaturatedLowPriority)
            {
                return;
            }
            MoteThrown moteThrown = NewBaseAirPuff();
            moteThrown.exactPosition = loc;
            moteThrown.exactPosition += new Vector3(Rand.Range(-0.02f, 0.02f), 0f, Rand.Range(-0.02f, 0.02f));
            moteThrown.SetVelocity((float)Rand.Range(-45, 45), Rand.Range(1.2f, 1.5f));
            GenSpawn.Spawn(moteThrown, loc.ToIntVec3(), map, WipeMode.Vanish);
        }

        private Thing parent;

        private int ticksUntilSpray = 500;

        private int sprayTicksLeft;

        public Action startSprayCallback;

        public Action endSprayCallback;

        private const int MinTicksBetweenSprays = 500;

        private const int MaxTicksBetweenSprays = 2000;

        private const int MinSprayDuration = 200;

        private const int MaxSprayDuration = 500;

        private const float SprayThickness = 0.6f;
    }
}
