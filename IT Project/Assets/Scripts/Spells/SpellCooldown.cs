using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SpellCooldown : MonoBehaviour {

	public List<DummySpell> spells;
    
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (spells[0].currentCooldown >= spells[0].cooldown)
            {
                //cast spell
                spells[0].currentCooldown = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (spells[1].currentCooldown >= spells[1].cooldown)
            {
                //cast spell
                spells[1].currentCooldown = 0;
            }
        }
    }

    void Update()
    {
		foreach (DummySpell s in spells)
        {
            if (s.currentCooldown < s.cooldown)
            {
                s.currentCooldown += Time.deltaTime;
                s.spellIcon.fillAmount = s.currentCooldown / s.cooldown;
            }
        } 
    }
	
}

[System.Serializable]
public class DummySpell
{
    public float cooldown;
    public Image spellIcon;
    [HideInInspector]
    public float currentCooldown;

}
