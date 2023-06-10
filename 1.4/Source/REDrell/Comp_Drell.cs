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
    public class Comp_Drell : ThingComp
    {
        private bool soulActive = true;
        private Texture2D soulIcon;
        private Texture2D bodyIcon;

        private float originalMood;

        public override void PostExposeData()
        {
            base.PostExposeData();

            Scribe_Values.Look(ref soulActive, "soulActive", true);
            Scribe_Values.Look(ref originalMood, "originalMood", 1f);
        }

        public bool SoulActive
        {
            get
            {
                return soulActive;
            }
        }

        public Texture2D SoulIcon
        {
            get
            {
                if(soulIcon == null)
                {
                    soulIcon = ContentFinder<Texture2D>.Get("Things/Gizmo/DrellMode_Soul", true);
                }
                return soulIcon;
            }
        }

        public Texture2D BodyIcon
        {
            get
            {
                if (bodyIcon == null)
                {
                    bodyIcon = ContentFinder<Texture2D>.Get("Things/Gizmo/DrellMode_Body", true);
                }
                return bodyIcon;
            }
        }

        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            foreach (var current in base.CompGetGizmosExtra())
            {
                yield return current;
            }

            if (soulActive)
            {
                yield return new Command_Action
                {
                    defaultLabel = "RE_DrellSoulModeLabel".Translate(),
                    defaultDesc = "RE_DrellSoulModeDesc".Translate(),
                    icon = SoulIcon,
                    action = delegate { 
                        soulActive = false;
                        Pawn pawn = parent as Pawn;
                        originalMood = pawn.needs.mood.curLevelInt;
                        pawn.needs.mood.curLevelInt = pawn.needs.mood.MaxLevel;
                    }
                };
            }
            else
            {
                yield return new Command_Action
                {
                    defaultLabel = "RE_DrellBodyModeLabel".Translate(),
                    defaultDesc = "RE_DrellBodyModeDesc".Translate(),
                    icon = BodyIcon,
                    action = delegate { 
                        soulActive = true;
                        Pawn pawn = parent as Pawn;
                        pawn.needs.mood.curLevelInt = originalMood;
                    }
                };
            }
        }
    }
}
