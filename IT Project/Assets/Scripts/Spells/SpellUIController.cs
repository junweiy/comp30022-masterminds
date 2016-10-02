using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SpellUIController : MonoBehaviour {

    public GameObject spellIcon;
    private GameObject spellBar_go;
    private Character character;
    private bool isMainCharacter;
    private List<Spell> spells;
    private GameObject [] icons;

    public void initialise(Character c, bool isMain, CharacterController characterController)
    {
        character = c;
        spells = c.spells;
        spellBar_go = GameObject.FindGameObjectWithTag("SpellBar");
        //generate buttons
        if (isMain)
        {
            for (int i = 0; i < spells.Count; i++)
            {
                GameObject spellButton = (GameObject)Instantiate(spellIcon);
                var spellButtonController = spellButton.GetComponent<SpellIconController>();
                spellButtonController.initialise(spells[i], characterController);
                spellButton.transform.SetParent(spellBar_go.transform, false);
                spellButton.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        Debug.Assert(c.spells != null);
    }

}
