using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpellIconController : MonoBehaviour {

    public Spell spell;
    private Image spellBG;
    private Image spellImage;
    private SpellController spellController;

    public bool isClicked;

    // Initialise the spell icon
    void Start()
    {
        spellBG = GetComponent<Image>();
        spellImage = transform.GetChild(0).GetComponent<Image>();
        spellController = GetMainPlayerController<SpellController>();
    }

    // On click event
    public void onclick()
    {
        if(spell.currentCooldown >= spell.cooldown)
        {
            spellController.CastSpell(spell);
        }
    }

    // Update the display of image
    void Update()
    {
		if (spellController == null) {
			spellController = GetMainPlayerController<SpellController>();
			return;
		}

        if (spell.currentCooldown < spell.cooldown)
        {
            spellImage.fillAmount = spell.currentCooldown / spell.cooldown;
        }
    }

    public static GameObject FindMainPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Character");
        foreach (GameObject player in players)
        {
            if (player.GetPhotonView().isMine)
            {
                return player;
            }
        }
        return null;

    }

    public T GetMainPlayerController<T>()
    {
        GameObject mainPlayer = FindMainPlayer();
		if (mainPlayer == null) {
			return default(T);
		}
        return mainPlayer.GetComponent<T>();
    }
}
