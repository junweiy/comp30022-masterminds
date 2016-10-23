using UnityEngine;

/*
 *  This class is the main class for the character. It stores all the information about the character
 *  
 * 
 */

public class Character : Photon.MonoBehaviour {
	// The default HP that a character has when spawned
    private const int DEFAULT_HP = 100;
	// ID of the character, same as Photon View ID
    public int CharId;
	// The user name of the player controlling current character
    public string UserName;
	// Current HP of the player, initialised with default hp
    public int Hp = DEFAULT_HP;
	// The maximum HP that the player can have
    private int _maxHp;
	// Whether the player is dead
    public bool IsDead { get; private set; }
	// Number of kills
    public int NumKilled;
	// Number of deaths
    public int NumDeath;
	// The range of spell that the player can cast
    public float Range { get; set; }
	// Health bar element of the character
    private HealthBarUI _healthBarUI;

	// Variables initialisation
    private void Awake() {
        _maxHp = DEFAULT_HP;
        NumDeath = 0;
        NumKilled = 0;
        IsDead = false;
    }
		
    private void Start() {
        this._healthBarUI = this.GetComponent<HealthBarUI>();
        CharId = photonView.viewID;
		// Find the user name from profile handler class
        if (photonView.isMine) {
			UserName = GameObjectFinder.FindProfileHandler().UserName;
        }
    }

    private void Update() {
		// Update the HP of current character in UI
        _healthBarUI.SetHealthUI(Hp, _maxHp);
    }

	// Method that can handle damage on current character
    public void TakeDamage(int f) {
        Hp -= f;
        if (Hp <= 0 && !IsDead) {
            OnDeath();
        }
    }

    private void OnDeath() {
        if (IsDead) {
            return;
        }
        IsDead = true;
        NumDeath++;
		GameController gc = GameObjectFinder.FindGameController();
		// Update the profile is game ends (handle being killed by lava)
        if (GameController.CheckIfGameEnds()) {
            UpdateProfile(false);
            gc.DisplayGameOverMessage();
        }
		// Disable UI and focus camera on another player alive
        if (photonView.isMine) {
            DisableAndObserveOtherPlayer();
        }
    }

	// Handle death in a naive way by moving to a hidden place and focuing
	// camera on another player alive
    private void DisableAndObserveOtherPlayer() {
        FocusCameraOnOtherPlayer();
        MoveToHiddenPlace();
        DisableUi();
    }

	// Move the character to a hidden place and set scale to 0 to avoid being found by
	// other players
    private void MoveToHiddenPlace() {
        this.transform.position = new Vector3(0, 0, 1000);
        this.transform.localScale = new Vector3(0, 0, 0);
        this.GetComponent<CharacterController>().enabled = false;
    }

	// Find another character and focus camera on the character
    private void FocusCameraOnOtherPlayer() {
        GameObject anotherPlayer = GameObjectFinder.FindAnotherPlayerAlive();
        transform.FindChild("CameraRig").gameObject.SetActive(false);
        GameObject cameraRig = anotherPlayer.transform.FindChild("CameraRig").gameObject;
        cameraRig.GetComponent<CameraControl>().enabled = true;
        cameraRig.GetComponentInChildren<Camera>().enabled = true;
        cameraRig.GetComponentInChildren<AudioListener>().enabled = true;
    }

	// Disable joystick and spell icon
    public static void DisableUi() {
		GameObjectFinder.FindJoyStick().SetActive(false);
		GameObjectFinder.FindSpellIcon().SetActive(false);
    }

	// Update number of kills and handle game state if game ends
    public void Killed() {
        NumKilled++;
		GameController gc = GameObjectFinder.FindGameController();
        if (GameController.CheckIfGameEnds()) {
            UpdateProfile(true);
            gc.DisplayGameOverMessage();
        }
    }

	// Update the profile in profile handler
    public void UpdateProfile(bool win) {
        if (photonView.isMine) {
			ProfileHandler ph = GameObjectFinder.FindProfileHandler();
            ph.UpdateProfile(this.NumKilled, this.NumDeath, win);
        }
    }

	// Set position of current character for all clients
    public void SetPositionForAll(Vector3 pos) {
        this.photonView.RPC("SetPositionRPC", PhotonTargets.All, pos);
    }

	// Set HP of current character for all clients
    public void SetHpForAll(int hp) {
        this.photonView.RPC("SetHPRPC", PhotonTargets.All, hp);
    }

	// Set rotation of current character for all clients
    public void SetRotationForAll(Quaternion rot) {
        this.photonView.RPC("SetRotationRPC", PhotonTargets.All, rot);
    }

    [PunRPC]
	public void SetPositionRPC(Vector3 pos) {
        this.transform.position = pos;
    }

    [PunRPC]
	public void SetRotationRPC(Quaternion rot) {
        this.transform.rotation = rot;
    }

    [PunRPC]
	public void SetHPRPC(int hp) {
        this.Hp = hp;
    }
}