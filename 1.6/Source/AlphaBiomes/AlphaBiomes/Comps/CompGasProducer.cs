﻿
using RimWorld;
using Verse;


namespace AlphaBiomes
{
    class CompGasProducer : ThingComp
    {

        private int gasProgress = 0;
        private int gasTickMax = 512;


        public CompProperties_GasProducer Props
        {
            get
            {
                return (CompProperties_GasProducer)this.props;
            }
        }


        public override void CompTick()
        {
            if (this.parent.def != InternalDefOf.AB_AgariluxPrime||((this.parent.def== InternalDefOf.AB_AgariluxPrime) && (AlphaBiomes_Settings.AB_UseAgariluxParticles))) {

                this.gasProgress += 1;
                if (this.gasProgress > gasTickMax)
                {

                    if (this.parent.Map != null)
                    {

                        if (!Props.needsElectricity || (Props.needsElectricity && this.parent.GetComp<CompPowerTrader>().PowerOn)) {
                            int num = GenRadial.NumCellsInRadius(Props.radius);
                            for (int i = 0; i < num; i++)
                            {
                                IntVec3 current = this.parent.Position + GenRadial.RadialPattern[i];
                                if (current.InBounds(this.parent.Map) && Rand.Value < Props.rate)
                                {
                                    Thing thing = ThingMaker.MakeThing(ThingDef.Named(Props.gasType), null);

                                    GenSpawn.Spawn(thing, current, this.parent.Map);
                                }
                            }

                        }
    
                        this.gasProgress = 0;
                    }

                }

            }
            
        }
    }
}
