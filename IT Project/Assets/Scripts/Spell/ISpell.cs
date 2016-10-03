using UnityEngine;
using System.Collections;

namespace Effect{
	
	public interface ISpell {

		void ApplyEffect(int pid, Vector3 dest);
		int GetSpellID ();

	}

}