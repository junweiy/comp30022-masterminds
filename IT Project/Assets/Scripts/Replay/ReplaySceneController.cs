using UnityEngine;
using System.Collections.Generic;
using Replay;
using UnityEngine.UI;

public class ReplaySceneController : RecordHandler {

    public Text ButtonLabel;
    public GameObject pauseButton;
    public GameObject endMessage;
    //public Slider speedSlider;
    //public Text sliderText;
    //private decimal replaySpeed;
    private ReplayState _state = ReplayState.Preparing;

    private ReplayState state {
        get {
            return _state;
        } set {
            _state = value;

            if (ButtonLabel != null) {
                if (value == ReplayState.Preparing) {
                    ButtonLabel.text = "Preparing";
                } else if (value == ReplayState.Started) {
                    ButtonLabel.text = "Pause";
                } else if (value == ReplayState.Paused) {
                    ButtonLabel.text = "Continue";
                } else if (value == ReplayState.Ended) {
                    ButtonLabel.text = "Replay Ended";
                }
            }
        }
    }

    GameReplay replay;
    int frameCount = 0;
    
    // Use this for initialization
    void Start () {
        GameReplay p = GlobalState.instance.ReplayToLoad;

        if (p == null) {
            Debug.LogError("ReplayToLoad is empty");
        }

        LoadReplay(p);
        StartReplay();
    }

    void LoadReplay(GameReplay replay) {
        this.replay = replay;
        var info = replay.info;
        Application.targetFrameRate = info.targetFrameRate;
        frameCount = 0;
    }

    void StartReplay() {
        state = ReplayState.Started;
    }

    public void Exit() {
        StateController.SwitchToReplaySelection();
    }

    void FinishReplay() {
        endMessage.SetActive(true);
        state = ReplayState.Ended;
    }

    public void TriggerPauseContinue() {
        if(state == ReplayState.Started) {
            Pause();
        } else if (state == ReplayState.Paused) {
            Continue();
        }
    }

    public void Pause() {
        state = ReplayState.Paused;
        pauseButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/Continue");
        Time.timeScale = 0;
    }

    public void Continue() {
        state = ReplayState.Started;
        pauseButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/Pause");
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown("p")) {
            TriggerPauseContinue();
        }

        if (state == ReplayState.Started) {

            if (replay.entries.Count == 0) {
                FinishReplay();
                return;
            }

            //replaySpeed = (decimal)speedSlider.value;
            //Time.timeScale = (float)System.Decimal.Round(replaySpeed, 1);
            //sliderText.text = Time.timeScale.ToString("0.0");

            var nextEntry = replay.entries.Peek();
            while (nextEntry.frameTime <= frameCount) {
                replay.entries.Dequeue();
                nextEntry.record.applyEffect(this);
                if (replay.entries.Count == 0) {
                    FinishReplay();
                    return;
                }
                nextEntry = replay.entries.Peek();
            }

            frameCount += 1;
        }
	}
}
