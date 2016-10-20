using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ResultPageController : MonoBehaviour {

    public bool isWinner;
	public string userName;
	public int kill;
	public int death;

    public GameObject playerResult;
    public RectTransform playerList;
    public GameObject result;
    public GameObject savedImage;
    public Button saveButton;

    // Update is called once per frame
    void Start()
    {
        
        //display result (win or lose)
        if (isWinner)
        {
            result.GetComponent<Text>().text = "VICTORY";
        }
        else
        {
            result.GetComponent<Text>().text = "DEFEAT";
        }

        // generate a panel to display details of result for each user
        for (int i = 0; i < 1; i++)
        {
            GameObject userDetail = (GameObject)Instantiate(playerResult);
            Text[] texts;
            texts = userDetail.GetComponentsInChildren<Text>();
			texts [0].text = userName;
			texts [1].text = kill.ToString ();
			texts [2].text = death.ToString ();

            userDetail.transform.SetParent(playerList, false);
            userDetail.transform.localScale = new Vector3(1, 1, 1);
        }


    }


	public void returnToMainMenu() {
		StateController.SwitchToMainMenu ();
	}

	public void saveReplay() {
        savedImage.SetActive(true);
        saveButton.interactable = false;
        ReplayIO.SaveReplayWithTimeAsFilename(GlobalState.instance.ReplayToSave);
	}
}
