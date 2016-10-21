using UnityEngine;

/*
 *  This class is the main class for the character. It stores all the information about the character
 *  
 * 
 */

public class Character : Photon.MonoBehaviour {
    private const int DEFAULT_HP = 100;
    public const int MAXIMUM_NUMBER_OF_ITEM = 6;

    public int CharId;
    public string UserName;

    public int Hp = DEFAULT_HP;
    private int _maxHp;

    public bool IsDead { get; private set; }
    public int NumKilled;
    public int NumDeath;


    public float Range { get; set; }

    private HealthBarUi _healthBarUi;

    private void Awake() {
        _maxHp = DEFAULT_HP;
        NumDeath = 0;
        NumKilled = 0;
        IsDead = false;
    }

    private void Start() {
        this._healthBarUi = this.GetComponent<HealthBarUi>();
        CharId = photonView.viewID;
        if (photonView.isMine) {
            UserName = GameObject.FindGameObjectWithTag("ProfileHandler").GetComponent<ProfileHandler>().UserName;
        }
    }

    private void Update() {
        _healthBarUi.SetHealthUi(Hp, _maxHp);
    }

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
        GameController gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        if (GameController.CheckIfGameEnds()) {
            UpdateProfile(false);
            gc.DisplayGameOverMessage();
        }
        if (photonView.isMine) {
            DisableAndObserveOtherPlayer();
        }
    }

    private void DisableAndObserveOtherPlayer() {
        FocusCameraOnOtherPlayer();
        MoveToHiddenPlace();
        DisableUi();
    }

    private void MoveToHiddenPlace() {
        this.transform.position = new Vector3(0, 0, 1000);
        this.transform.localScale = new Vector3(0, 0, 0);
        this.GetComponent<CharacterController>().enabled = false;
    }

    private void FocusCameraOnOtherPlayer() {
        GameObject anotherPlayer = FindAnotherPlayerAlive();
        transform.FindChild("CameraRig").gameObject.SetActive(false);
        GameObject cameraRig = anotherPlayer.transform.FindChild("CameraRig").gameObject;
        Debug.Log(anotherPlayer.GetComponent<Character>().CharId);
        cameraRig.GetComponent<CameraControl>().enabled = true;
        cameraRig.GetComponentInChildren<Camera>().enabled = true;
        cameraRig.GetComponentInChildren<AudioListener>().enabled = true;
    }

    private GameObject FindAnotherPlayerAlive() {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Character");
        foreach (GameObject player in players) {
            if (!player.GetComponent<Character>().IsDead) {
                return player;
            }
        }
        return null;
    }

    public static void DisableUi() {
        GameObject.FindGameObjectWithTag("JoyStick").SetActive(false);
        GameObject.FindGameObjectWithTag("SpellButton").SetActive(false);
    }


    public void Killed() {
        NumKilled++;
        GameController gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        if (GameController.CheckIfGameEnds()) {
            UpdateProfile(true);
            gc.DisplayGameOverMessage();
        }
    }

    public void UpdateProfile(bool win) {
        if (photonView.isMine) {
            ProfileHandler ph = GameObject.FindGameObjectWithTag("ProfileHandler").GetComponent<ProfileHandler>();
            ph.UpdateProfile(this.NumKilled, this.NumDeath, win);
        }
    }


    public static GameObject FindCharacterWithUserName(string userName) {
        GameObject[] characters = GameObject.FindGameObjectsWithTag("Character");
        foreach (GameObject character in characters) {
            if (character.GetComponent<Character>().UserName == userName) {
                return character;
            }
        }
        return null;
    }

    public void SetPositionForAll(Vector3 pos) {
        this.photonView.RPC("SetPositionRPC", PhotonTargets.All, pos);
    }

    public void SetHpForAll(int hp) {
        this.photonView.RPC("SetHPRPC", PhotonTargets.All, hp);
    }

    public void SetRotationForAll(Quaternion rot) {
        this.photonView.RPC("SetRotationRPC", PhotonTargets.All, rot);
    }

    [PunRPC]
    public void SetPositionRpc(Vector3 pos) {
        Debug.Log(pos);
        this.transform.position = pos;
    }

    [PunRPC]
    public void SetRotationRpc(Quaternion rot) {
        Debug.Log(rot);
        this.transform.rotation = rot;
    }

    [PunRPC]
    public void SetHprpc(int hp) {
        Debug.Log(hp);
        this.Hp = hp;
    }
}