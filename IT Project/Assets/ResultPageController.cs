﻿using UnityEngine;
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
       
        //TODO: team related information

        // generate a panel to display details of result for each user
        for (int i = 0; i < chars.Length; i++)
        {
            GameObject userDetail = (GameObject)Instantiate(detailPanel);
            Text[] texts;
            texts = userDetail.GetComponentsInChildren<Text>();
            texts[0].text = chars[i].player.username;
            texts[1].text = chars[i].player.userId.ToString();           
            texts[2].text = chars[i].numKill.ToString();
            texts[3].text = chars[i].numDeath.ToString();

            Image[] skill_images;
            skill_images = userDetail.GetComponentsInChildren<Image>();
            for (int j=0; j<chars[i].spells.Count; j++)
            {
                skill_images[j+2].sprite = Resources.Load<Sprite>(chars[i].spells[j].iconPath);
                skill_images[j+2].color = new Color32(255,255,255,255);
            }

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
