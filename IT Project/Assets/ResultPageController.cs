using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultPageController : MonoBehaviour {

    private bool isWinner;
    Character[] chars;

    public GameObject playerResult;
    public RectTransform playerList;
    public GameObject result;

    // Update is called once per frame
    void Start()
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
            GameObject userDetail = (GameObject)Instantiate(playerResult);
            Text[] texts;
            texts = userDetail.GetComponentsInChildren<Text>();
            //texts[0].text = chars[i].player.username;
            //texts[1].text = chars[i].player.userId.ToString();
            //texts[2].text = chars[i].numKill.ToString();
            texts[3].text = chars[i].numDeath.ToString();

            userDetail.transform.SetParent(playerList, false);
            userDetail.transform.localScale = new Vector3(1, 1, 1);
        }


    }

    public void loadData()
    {
        //Character current = GlobalState.instance.currentChar;
        //this.chars = GlobalState.instance.gameController.characters;
        //int maxScore = chars.Max(c => c.score);
        //Character[] winners = chars.Where(c => c.score == maxScore).ToArray();
        //this.isWinner = winners.Contains(current);
    }

	public void returnToMainMenu() {
		StateController.switchToMainMenu ();
	}

	public void saveReplay() {
		// TODO
		Debug.LogWarning ("Replay function not implemented yet");
	}
}
