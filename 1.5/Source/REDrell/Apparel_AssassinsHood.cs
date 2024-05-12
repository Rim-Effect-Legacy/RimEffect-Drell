using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Reflection.Emit;

using UnityEngine;
using RimWorld;
using Verse;

namespace REDrell
{
    public class Apparel_AssassinsHood : Apparel
	{
		public List<Verb> extendedVerbs = new List<Verb>();

		public override void PostMake()
		{
			this.CheckAllVerbsAndTryExtend();
			base.PostMake();
		}

        public override void Notify_Equipped(Pawn pawn)
        {
			this.CheckAllVerbsAndTryExtend();
            base.Notify_Equipped(pawn);
        }

        public override void ExposeData()
        {
            base.ExposeData();
			this.CheckAllVerbsAndTryExtend();
		}


        private void CheckAllVerbsAndTryExtend()
        {
            Pawn pawn = this.Wearer;

            if (pawn?.apparel?.WornApparel != null)
            {
                foreach (Apparel item in pawn.apparel.WornApparel)
                {
                    CompApparelReloadable comp = item.GetComp<CompApparelReloadable>();
                    if (comp != null)
                    {
                        foreach (Verb verb in comp.AllVerbs)
                        {
                            this.TryCheckAndExtendVerbRange(verb);
                        }
                    }
                }
            }
            if (pawn?.equipment?.AllEquipmentListForReading != null)
            {
                List<ThingWithComps> allEquipmentListForReading = pawn.equipment.AllEquipmentListForReading;
                for (int i = 0; i < allEquipmentListForReading.Count; i++)
                {
                    foreach (Verb verb in allEquipmentListForReading[i].GetComp<CompEquippable>().AllVerbs)
                    {
                        this.TryCheckAndExtendVerbRange(verb);
                    }
                }
            }
        }

        public void TryCheckAndExtendVerbRange(Verb verb)
		{
			if (!extendedVerbs.Contains(verb) && verb.verbProps.range > 1.42f && verb.caster is Pawn pawn)
			{
				ExtendVerbRangeBy(verb, 1.30f);
				extendedVerbs.Add(verb);
			}
		}

		private void ExtendVerbRangeBy(Verb verb, float multiplier)
		{
			var newProperties = new VerbProperties();
			foreach (var fieldInfo in typeof(VerbProperties).GetFields())
			{
				try
				{
					var newField = verb.verbProps.GetType().GetField(fieldInfo.Name);
					newField.SetValue(newProperties, fieldInfo.GetValue(verb.verbProps));
				}
				catch { }
			}
			newProperties.range *= multiplier;
			verb.verbProps = newProperties;
		}
	}
}
