using UnityEngine;
using Command;
using Manager;

namespace Replay
{
    public class Replay : MonoBehaviour
    {
        private float AccumilatedTime = 0f;
        private float FrameLength = 0.05f; //50 miliseconds
        private int GameFrame = 0;

        void Start()
        {
            Recording.Load("first.txt");
            PlayerManager.SpawnAllPlayer();

        }

        // Update is called once per frame
        void Update()
        {

            //Basically same logic as FixedUpdate, but we can scale it by adjusting FrameLength
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
            while (!Recording.IsFinished() && Recording.GetCurrentFrame() == GameFrame)
            {
                ICommand c = Recording.Next();
                c.ProcessCommand();
                Debug.Log(c.Show());
            }
            GameUpdate();
            GameFrame++;
        }


        private void GameUpdate()
        {
            PlayerManager.UpdateAll();
        }

    }
}