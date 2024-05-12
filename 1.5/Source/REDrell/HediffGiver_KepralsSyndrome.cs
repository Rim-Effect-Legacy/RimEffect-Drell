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
                Hediff hypothermiaHediff = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.Hypothermia);
                if (Rand.Chance(hypothermiaHediff.Severity / 100))
                {
                    HealthUtility.AdjustSeverity(pawn, hediff, 0.008f);
                }
            }
        }
    }
}
