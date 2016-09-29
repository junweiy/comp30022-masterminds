using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public void checkIfGameEnds() {
		int numAlive = GameObject.FindGameObjectsWithTag ("Character").Length;
		if (numAlive == 1) {
			StateController.FinishRound ();
		}
	}

//	public void prepareForNextRound () {
//		foreach (Character c in characters) {
//			var characterObj = Instantiate<GameObject> (characterPrefab);
//			var charController = characterObj.GetComponent<CharacterController> ();
//			var spellController = characterObj.GetComponent<SpellController> ();
//			charController.initialise (c);
//			spellController.initialise (c);
//			if (GlobalState.isCurrentChar (c)) {
//				mainCamera.GetComponent<CameraControl> ().m_Target = characterObj.transform;
//				charController.setAsMainCharacter ();
//				spellController.setAsMainCharacter ();
//			}
//		}
//
//		numCharacterAlive = characters.Length;
//		setCharactersPos();
//
//		mainCamera.GetComponent<CameraControl> ().enabled = true;
//	}
//		
//
//	public void finishRound() {
//		Debug.Log ("Round Finished");
//		Character winner = this.characters [0];
//		// TODO Players having same score not proply handled here
//		foreach (Character c in this.characters) {
//			if (c.score > winner.score) {
//				winner = c;
//			}
//		}
//
//		bool gameHasFinished = StateController.finishRound();
//	}
//


}
