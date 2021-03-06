﻿using UnityEngine;
using UnityEngine.UI;

// The main controller for the replay scene, performs the game replay
// and handles pause/continue actions
public class ReplaySceneController : RecordHandler {
    public Text ButtonLabel;
    public GameObject PauseButton;
    public GameObject EndMessage;
    private ReplayState _state = ReplayState.Preparing;

    private ReplayState State {
        get { return _state; }
        set {
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

    private GameReplay _replay;
    private int _frameCount = 0;

    // Use this for initialization
    private void Start() {
        GameReplay p = GlobalState.Instance.ReplayToLoad;

        if (p == null) {
            Debug.LogError("ReplayToLoad is empty");
        }

        LoadReplay(p);
        StartReplay();
    }

    private void LoadReplay(GameReplay replay) {
        this._replay = replay;
        var info = replay.Info;
        Application.targetFrameRate = info.TargetFrameRate;
        _frameCount = 0;
    }

    // Starts the replay
    private void StartReplay() {
        State = ReplayState.Started;
    }

    // Exit the replay scene, return to the replay selection menu
    public void Exit() {
        StateController.SwitchToReplaySelection();
    }

    private void FinishReplay() {
        EndMessage.SetActive(true);
        State = ReplayState.Ended;
    }

    // Trigger between pause/play state
    public void TriggerPauseContinue() {
        if (State == ReplayState.Started) {
            Pause();
        } else if (State == ReplayState.Paused) {
            Continue();
        }
    }

    // Pauses the replay
    public void Pause() {
        State = ReplayState.Paused;
        PauseButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/Continue");
        Time.timeScale = 0;
    }

    // Continues the replay from pause
    public void Continue() {
        State = ReplayState.Started;
        PauseButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/Pause");
        Time.timeScale = 1;
    }

    // Update is called once per frame
    private void Update() {
        if (Input.GetKeyDown("p")) {
            TriggerPauseContinue();
        }

        if (State == ReplayState.Started) {
            if (_replay.Entries.Count == 0) {
                FinishReplay();
                return;
            }

            //replaySpeed = (decimal)speedSlider.value;
            //Time.timeScale = (float)System.Decimal.Round(replaySpeed, 1);
            //sliderText.text = Time.timeScale.ToString("0.0");

            var nextEntry = _replay.Entries.Peek();
            while (nextEntry.FrameTime <= _frameCount) {
                _replay.Entries.Dequeue();
                nextEntry.Record.ApplyEffect(this);
                if (_replay.Entries.Count == 0) {
                    FinishReplay();
                    return;
                }
                nextEntry = _replay.Entries.Peek();
            }

            _frameCount += 1;
        }
    }
}