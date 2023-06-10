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
    public class REDrell : Mod
    {
        public static Harmony harmonyInstance;

        public REDrell(ModContentPack content) : base(content)
        {
            REDrell.harmonyInstance = new Harmony("RimEffect.Drell");
        }
    }
}
