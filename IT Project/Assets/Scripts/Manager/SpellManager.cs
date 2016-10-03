using UnityEngine;
using System.Collections;
using Effect;


namespace Manager
{
	public static class SpellManager
	{


		public static ISpell GetSpellByID(int i){

			if (i == 0) {
				return new FireBall ();
			}
			if (i == 1) {
				return new FireNova ();
			}
			throw new System.Exception ("Unknow Spell");
		}


	}
}