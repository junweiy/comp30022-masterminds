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
            result.GetComponent<Text>().text = "LOSE";
        }

        // generate a panel to display details of result for each user
        for (int i = 0; i < 1; i++)
        {
            GameObject userDetail = (GameObject)Instantiate(playerResult);
            Text[] texts;
            texts = userDetail.GetComponentsInChildren<Text>();
			texts [0].text = userName;
			texts [1].text = " ";
			texts [2].text = kill.ToString ();
			texts[3].text = death.ToString ();

            userDetail.transform.SetParent(playerList, false);
            userDetail.transform.localScale = new Vector3(1, 1, 1);
        }


    }


	public void returnToMainMenu() {
		StateController.SwitchToMainMenu ();
	}

	public void saveReplay() {
		// TODO
		Debug.LogWarning ("Replay function not implemented yet");
	}
}
