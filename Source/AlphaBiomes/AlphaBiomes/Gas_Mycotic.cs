using RimWorld;
using Verse;
using System;
using System.Collections.Generic;

namespace AlphaBiomes
{
    public class Gas_Mycotic : Gas
    {
        private int tickerInterval = 60;




        public override void Tick()
        {
            base.Tick();
            try
            {
                if (tickerInterval >= 30)
                {

                    HashSet<Thing> hashSet = new HashSet<Thing>(this.Position.GetThingList(this.Map));
                    if (hashSet != null)
                    {
                        foreach (Thing current in hashSet)
                        {
                            Pawn pawn = current as Pawn;
                            bool flag = (pawn != null);
                            if (flag && pawn.def.defName != "AA_Wildpod" && pawn.def.defName != "AA_Wildpawn" && pawn.def.defName != "AB_Agaripod" && pawn.def.defName != "AB_MycoidColossus")
                            {
                                pawn.TakeDamage(new DamageInfo(DamageDefOf.Cut, 1, 0f, -1f, null, null, null, DamageInfo.SourceCategory.ThingOrUnknown));
                                HealthUtility.AdjustSeverity(pawn, HediffDefOf.ToxicBuildup, (float)0.050);
                                

                            }

                            Plant plant = current as Plant;
                            flag = (plant != null);
                            if (flag && plant.def.defName != "AB_AgariluxPrime")
                            {
                                plant.TakeDamage(new DamageInfo(DamageDefOf.Cut, 50, 0f, -1f, null, null, null, DamageInfo.SourceCategory.ThingOrUnknown));
                               


                            }
                        }

                    }
                    tickerInterval = 0;



                }
                tickerInterval++;
            }
            catch (NullReferenceException e)
            {
                //A weird error is produced sometimes when GetThingList returns a NullReferenceException. I did a try-catch which is inellegant, but it works
            }

        }


    }
}
