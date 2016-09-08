using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpellController : MonoBehaviour {
	
	public Character character;
	private bool isMainCharacter;
	public Image spellRange;

	public void initialise(Character c,bool isMainCharacter) {
		character = c;
		this.isMainCharacter = isMainCharacter;
		spellRange.enabled = false;
	}

	public void setAsMainCharacter(){
		isMainCharacter = true;
	}

	void Update () {

		if (isMainCharacter) {
			if (Input.GetKey ("1")) {
				Cast (1);
			}
			if (Input.GetKey ("2")) {
				Cast (2);
			}
			if (Input.GetKey ("3")) {
				Cast (3);
			}
			if (Input.GetKey ("4")) {
				Cast (4);
			}
		}
	}
	/* This function will cast spell based on the spell number stored in the character */
	private void Cast(int i)
	{
		Spell s = character.getSpell(i-1);
		if (s.isInstant())
		{
			//
		}
		else
		{
			spellRange.enabled = true;
			spellRange.transform.localScale *= s.range+character.range;

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100))
			{
				//s.applyEffect(character, this.transform, hit.point);
			}
			spellRange.enabled = false;
		}
	}

}
