using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SettingPanel : MonoBehaviour {

	public GameObject settingPanel;

	// Use this for initialization
	void Start () {
		settingPanel.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SettingButtonClicked(){
		settingPanel.SetActive (settingPanel.activeSelf==true?false:true);

	}

	public void ReturnToMainClicked(){
		SceneManager.LoadScene ("scenes/MainMenu");
	}

	public void ResumeClicked(){
		settingPanel.SetActive (false);
	}


}
