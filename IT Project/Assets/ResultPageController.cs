using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class ResultPageController : MonoBehaviour {
	private bool isWinner;
	Character[] chars;

    public GameObject detailPanel;
    public RectTransform displayDetailPanel;
    public GameObject result;

    // Update is called once per frame
    void Update()
    {
        loadData();
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
        for (int i = 0; i < chars.Length; i++)
        {
            GameObject userDetail = (GameObject)Instantiate(detailPanel);
            Text[] texts;
            texts = userDetail.GetComponentsInChildren<Text>();
            // character should know user name and id
            texts[0].text = chars[i].player.username;
            texts[1].text = chars[i].player.userId.ToString();
            
            texts[2].text = chars[i].numKill.ToString();
            texts[3].text = chars[i].numDeath.ToString();
            //TODO: load and display spell image needs implementation
            //...
            userDetail.transform.SetParent(displayDetailPanel, false);
            userDetail.transform.localScale = new Vector3(1, 1, 1);
        }


    }

    public void returnToRoom() {
		StateController.switchToRoom ();
	}

    public void loadData() {
        Character current = GlobalState.instance.currentChar;
        this.chars = GlobalState.instance.gameController.characters;
        int maxScore = chars.Max(c => c.score);
        Character[] winners = chars.Where(c => c.score == maxScore).ToArray();
        this.isWinner = winners.Contains(current);
    }

	public void saveReplay() {
		// TODO
		Debug.LogWarning ("Replay function not implemented yet");
	}

}
