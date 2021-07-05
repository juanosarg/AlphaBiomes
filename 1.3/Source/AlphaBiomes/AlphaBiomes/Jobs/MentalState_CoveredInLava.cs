using System;
using RimWorld;
using Verse.AI;

namespace AlphaBiomes
{
    public class MentalState_CoveredInLava : MentalState
    {
        public override RandomSocialMode SocialModeMax()
        {
            return RandomSocialMode.Off;
        }
    }
}