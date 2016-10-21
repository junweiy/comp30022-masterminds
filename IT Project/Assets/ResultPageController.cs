using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ResultPageController : MonoBehaviour {
    public bool IsWinner;
    public string UserName;
    public int Kill;
    public int Death;

    public GameObject PlayerResult;
    public RectTransform PlayerList;
    public GameObject Result;
    public GameObject SavedImage;
    public Button SaveButton;

    // Update is called once per frame
    private void Start() {
        //display result (win or lose)
        Result.GetComponent<Text>().text = IsWinner ? "VICTORY" : "DEFEAT";

        // generate a panel to display details of result for each user
        for (int i = 0; i < 1; i++) {
            GameObject userDetail = (GameObject) Instantiate(PlayerResult);
            var texts = userDetail.GetComponentsInChildren<Text>();
            texts[0].text = UserName;
            texts[1].text = Kill.ToString();
            texts[2].text = Death.ToString();

            userDetail.transform.SetParent(PlayerList, false);
            userDetail.transform.localScale = new Vector3(1, 1, 1);
        }
    }


    public void ReturnToMainMenu() {
        StateController.SwitchToMainMenu();
    }

    public void SaveReplay() {
        SavedImage.SetActive(true);
        SaveButton.interactable = false;
        ReplayIo.SaveReplayWithTimeAsFilename(GlobalState.Instance.ReplayToSave);
    }
}