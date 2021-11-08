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

    [HarmonyPatch(typeof(SkillRecord), "Learn")]
    public class Patch_SkillRecord_Learn
    {
        [HarmonyPrefix]
        public static bool Prefix(float xp, bool direct, SkillRecord __instance)
        {
            Pawn pawn = __instance?.pawn;
            if (pawn?.def == REDrellDefOf.RE_Drell)
            {
                if ((pawn.TryGetComp<Comp_Drell>()?.SoulActive ?? false) == false)
                {
                    xp *= 0f;
                }
                else if (xp < 0f)
                {
                    xp *= 0.15f;
                }
            }

            return true;
        }
    }
}
