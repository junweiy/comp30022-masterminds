using UnityEngine;
using UnityEngine.UI;

public class SpellIconController : MonoBehaviour {
    public Spell Spell;
    private Image _spellBg;
    private Image _spellImage;
    private SpellController _spellController;

    public bool IsClicked;

    // Initialise the spell icon
    private void Start() {
        _spellBg = GetComponent<Image>();
        _spellImage = transform.GetChild(0).GetComponent<Image>();
        _spellController = GetMainPlayerController<SpellController>();
    }

    // On click event
    public void Onclick() {
        if (Spell.CurrentCooldown >= Spell.Cooldown) {
            _spellController.CastSpell(Spell);
        }
    }

    // Update the display of image
    private void Update() {
        if (_spellController == null) {
            _spellController = GetMainPlayerController<SpellController>();
            return;
        }

        if (Spell.CurrentCooldown < Spell.Cooldown) {
            _spellImage.fillAmount = Spell.CurrentCooldown/Spell.Cooldown;
        }
    }

    public static GameObject FindMainPlayer() {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Character");
        foreach (GameObject player in players) {
            if (player.GetPhotonView().isMine) {
                return player;
            }
        }
        return null;
    }

    public T GetMainPlayerController<T>() {
        GameObject mainPlayer = FindMainPlayer();
        if (mainPlayer == null) {
            return default(T);
        }
        return mainPlayer.GetComponent<T>();
    }
}