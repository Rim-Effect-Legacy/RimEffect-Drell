using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using RimWorld;
using Verse;

namespace REDrell
{
    public class HediffGiver_KepralsSyndrome : HediffGiver
    {
        public override void OnIntervalPassed(Pawn pawn, Hediff cause)
        {
            if (pawn.health.hediffSet.HasHediff(HediffDefOf.Hypothermia))
            {
                string currentLabel = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.Hypothermia).CurStage.untranslatedLabel;
                if (currentLabel == "serious" || currentLabel == "extreme")
                {
                    if (Rand.Chance(0.1f))
                    {
                        HealthUtility.AdjustSeverity(pawn, hediff, 0.008f);
                    }
                }
            }
        }
    }
}
