using UnityEngine;

public class CharacterController : Photon.MonoBehaviour {
    // the character model
    public Character Character;
	// The velocity of the character
    public const float VELOCITY = 200f;
	// The max velocity of the player
    public const float MAX_VELOCITY = 300f;
	// Whether the player is speaking
    public bool IsSpeaking;
	// Rigidbody component
    private Rigidbody _rb;
	// Whether the player is being controlled
    private bool _controlling;
	// Rotation in the last frame
    private Quaternion _lastRotation;

    // Use this for initialization
    public void Start() {
        this.gameObject.tag = "Character";
        enabled = photonView.isMine;
        Character = GetComponent<Character>();
        _rb = GetComponent<Rigidbody>();
        IsSpeaking = GetComponent<PhotonVoiceRecorder>().IsTransmitting;
        _controlling = false;
    }

	// Adjust camera to allow control of the character
    public void SetControllable() {
        CameraControl cc = this.GetComponentInChildren<CameraControl>();
        Camera c = this.GetComponentInChildren<Camera>();
        AudioListener al = this.GetComponentInChildren<AudioListener>();
        cc.MTarget = this.transform;
        cc.enabled = true;
        c.enabled = true;
        al.enabled = true;
    }


    // Update is called once per frame
    private void Update() {
        if (!photonView.isMine) {
            return;
        }

		// Set controllable for current character
        if (!_controlling && PhotonNetwork.playerName == this.gameObject.GetComponent<Character>().UserName) {
            SetControllable();
            _controlling = true;
        }

        // Detect user input of movement
		GameObject joyStick = GameObjectFinder.FindJoyStick();
        if (joyStick == null) {
            return;
        }

		// Handle joystick input to move the character
        VirtualJoyStick vjs = joyStick.GetComponent<VirtualJoyStick>();
        Vector3 joyStickMovement = vjs.GetStickPosition();
        if (joyStickMovement != Vector3.zero) {
            _rb.AddForce(joyStickMovement*VELOCITY, ForceMode.Acceleration);
        }
        _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, MAX_VELOCITY);

		// Handle joystick input to rotate the character towards 
		// the same direction as the joystick
        if (!joyStickMovement.Equals(Vector3.zero)) {
            Vector3 targetDir = joyStickMovement;
            float step = 10;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
            _lastRotation = Quaternion.LookRotation(newDir);
            transform.rotation = _lastRotation;
            photonView.RPC("PlayAnim", PhotonTargets.All, "Move|Move");
        } else {
            transform.rotation = _lastRotation;
            photonView.RPC("PlayAnim", PhotonTargets.All, "Move|Idle");
        }
    }

    [PunRPC]
    private void PlayAnim(string name) {
        Animation anim = transform.GetChild(3).GetComponent<Animation>();
        if (anim.IsPlaying("Move|Cast")) {
            return;
        }
        anim.Play(name);
    }
}