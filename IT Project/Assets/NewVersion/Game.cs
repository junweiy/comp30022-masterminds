using UnityEngine;
using System.Collections.Generic;
using Replay;
using NetworkManager;
using Manager;
using Command;

public class Game : MonoBehaviour {

    private float AccumilatedTime = 0f;
    private float FrameLength = 0.05f; //50 miliseconds

    private int GameFrame = 0;
    private int GameFramesPerLockstepTurn = 4;
    public int CurrentFrame { get; private set; }

    public int LockStep { get; private set; }
    private InputHandler input;

    void Start()
    {
        PlayerManager.SpawnAllPlayer();
        input = new InputHandler(this);
        CommandSender.SendLastLockedCommands();
    }


    void Update () {
        
        AccumilatedTime = AccumilatedTime + Time.deltaTime;
        //in case the FPS is too slow, we may need to update the game multiple times a frame
        while (AccumilatedTime > FrameLength)
        {
            GameFrameTurn();
            AccumilatedTime = AccumilatedTime - FrameLength;
        }

    }

    private void GameFrameTurn()
    {
        //first frame is used to process Commands
        if (GameFrame == 0)
        {
            if (LockStepTurn())
            {
                LockStep++;
                GameFrame++;
                CurrentFrame++;
                CommandSender.SendLastLockedCommands();
            }
        }
        else
        {

            GameUpdate();

            GameFrame++;
            CurrentFrame++;
            if (GameFrame == GameFramesPerLockstepTurn)
            {
                GameFrame = 0;
            }
        }

    }

    //Process all the Command
    private bool LockStepTurn() 
    {
        Queue<ICommand> CommandQueue = CommandReceiver.GetReceivedCommands();
        bool flag = false;
        while (CommandQueue.Count != 0)
        {
            ICommand act = CommandQueue.Dequeue();
            act.ProcessCommand();
            if(act.GetType() == CommandType.Move)
            {
                Recording.Record(act);
            }
            else if (act.GetType() == CommandType.Cast)
            {
                Recording.Record(act);
            }
            flag = true;
        }
        return flag;
    }

    void OnApplicationQuit() {
        ConnectionHandler.receive.Abort();
        if (ConnectionHandler.client != null)
            ConnectionHandler.client.Close();
        Recording.Finish("first.txt");
    }


    private void GameUpdate()
    {

        input.UpdateInput();
        PlayerManager.UpdateAll();

    }

}
