using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class SpellIconController : MonoBehaviour {

    private SpellController spellController;
    public Spell spell;
    private Image spellBG;
    private Image spellImage;

    public bool isClicked;

    // Initialise the spell icon
    void Start()
    {
        spellController = GetMainPlayerController<SpellController>();
        spellBG = GetComponent<Image>();
        spellImage = transform.GetChild(0).GetComponent<Image>();
        isClicked = false;
    }

    public GameObject FindMainPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Character");
        foreach (GameObject player in players)
        {
            if (player.GetPhotonView().isMine)
            {
                return player;
            }
        }
        throw new UnityException();

    }

    public T GetMainPlayerController<T>()
    {
        GameObject mainPlayer = FindMainPlayer();
        return mainPlayer.GetComponent<T>();
    }

    // On click event
    public void onclick()
    {
        if (spell.currentCooldown >= spell.cooldown)
        {
            spellController.CastSpell(spell);
        }
    }

    // Update the display of image
    void Update()
    {
        Debug.Assert(this.spell != null);
        if (this.spell.currentCooldown < this.spell.cooldown)
        {
            spellImage.fillAmount = this.spell.currentCooldown / this.spell.cooldown;
        }
    }
}
