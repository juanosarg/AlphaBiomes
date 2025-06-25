using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Verse;

namespace AlphaBiomes
{
    public class PatchOperationModOption : PatchOperation
    {

        private PatchOperation match;

        private PatchOperation nomatch;

        protected override bool ApplyWorker(XmlDocument xml)
        {

            if (AlphaBiomes_Settings.AB_ShowBuildingsButton)
            {
                if (match != null)
                {
                    return match.Apply(xml);
                }
            }
            else if (nomatch != null)
            {
                return nomatch.Apply(xml);
            }
            return true;
        }


    }
}