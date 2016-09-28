using UnityEngine;
using System.Collections;

public class VoiceChatController : MonoBehaviour {
	// The name of the default device
	private const string DEFAULT_DEVICE = "";
	// The max length of voice message in secs
	private const int MAX_LENGTH = 10;
	// The sample rate of the message
	private const int SAMPLE_RATE = 44100;

	// Audio source element from the object that the script attaches to
	private AudioSource aud;
	// The recorded clip to be sent
	private AudioClip recordedClip;

	// Flag indicating whether recording has finished
	private bool finishedRecording;
	// Flag indicating whether sending has finished
	private bool finishedSending;

	/* At the beginning, aud component and variables are initialised.
	 */ 
	public void Start() {
		aud = GetComponent<AudioSource> ();
		finishedRecording = true;
		finishedSending = true;
	}

	/* The function detects user input, and executes actions upon request.
	 */  
	public void Update() {
		// Begin recording when user started recording message to send
		if (StartRecording ()) {
			finishedRecording = false;
			record ();
		}

		// Stop recording when user has finished recording the message
		if (Microphone.IsRecording (DEFAULT_DEVICE)) {
			CheckIfEndRecording ();
		}
	}

	/* The function defines how user indicates the beginning of recording a voice message.
	 */ 
	public bool StartRecording() {
		if (Input.GetKeyDown(KeyCode.R) && finishedSending) {
			Debug.Log ("Started recording");
			return true;
		}
		return false;
	}

	/* The function defines how user indicates the end of recording a voice message. 
	 */
	public bool EndRecording() {
		if (Input.GetKeyDown (KeyCode.S)) {
			Debug.Log ("Finished recording");
			return true;
		}
		return false;
	}

	/* The function checks whether user has finished recording the voice message and will assign the 
	 * clip to the relative variable.
	 */
	public void CheckIfEndRecording() {
		// The last position of recording
		int pos;
		// Wait until the recording has started
		while (!(Microphone.GetPosition(DEFAULT_DEVICE) > 0)) {
		}
		// When User ends recording, current audioclip will be trimmed
		if (EndRecording()) {
			// Get the last position of recording
			pos = Microphone.GetPosition (DEFAULT_DEVICE);
			// Samples from current recording
			float[] samples = new float[recordedClip.samples * recordedClip.channels];
			recordedClip.GetData (samples, 0);
			// New samples for new audioclip
			float[] newSample = new float[pos * recordedClip.channels];
			// Copy old samples to a new array
			for (int i = 0; i < newSample.Length; i++) {
				newSample[i] = samples[i];
			}
			// Create new audioclip with given properties
			AudioClip newClip = AudioClip.Create (recordedClip.name, pos, recordedClip.channels, recordedClip.frequency, false, false);
			// Load the data from old clip
			newClip.SetData (newSample, 0);
			// Now recording can be ended
			Microphone.End (DEFAULT_DEVICE);
			// Replace the old clip
			AudioClip.Destroy (recordedClip);
			recordedClip = newClip;
			// Recording has finished
			finishedRecording = true;
			// Uncomment codes below to replay recording
			aud.clip = newClip;
			aud.Play ();
		}
	}

	/* The function starts recording and will assign the recorded clip to the relative variable
	 * once the length reached the limit.
	 */ 
	public void record() {
		aud.Stop ();
		// Start recording to audioclip from the mic
		recordedClip = Microphone.Start (DEFAULT_DEVICE, false, MAX_LENGTH, SAMPLE_RATE);
		// Recording has finished
		finishedRecording = true;
	}

}
