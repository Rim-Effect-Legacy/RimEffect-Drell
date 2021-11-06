using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using RimWorld;
using Verse;

using HarmonyLib;

namespace REDrell
{
    [HarmonyPatch(typeof(MemoryThoughtHandler), "TryGainMemory", new Type[] { typeof(Thought_Memory), typeof(Pawn) })]
    public static class Patch_MemoryThoughtHandler
    {
        [HarmonyPostfix]
        public static void Postfix(Thought_Memory newThought, Pawn otherPawn)
        {
            if(newThought.def == ThoughtDefOf.GotSomeLovin && otherPawn?.def == REDrellDefOf.RE_Drell)
            {
                Thought_Memory drellLovin = (Thought_Memory)ThoughtMaker.MakeThought(REDrellDefOf.RE_DrellLovin);
                otherPawn.needs.mood.thoughts.memories.TryGainMemory(drellLovin, otherPawn);
            }
        }
    }
}
