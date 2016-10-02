using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Manager;
using UnityEngine.SceneManagement;

namespace NetworkManager
{
    public class Network : MonoBehaviour
    {

        public Text process;

        private bool joined;

        private enum State {
            Connected,
            Failed,
            Joined,
            Ready,
            Start
        }

        private State state;

        // Use this for initialization
        void Start()
        {

            GameManager.network = this;
            try
            {
                ConnectionHandler.StartConnection();
                process.text = "Connection sucessful, Joining room";
                state = State.Connected;

            }
            catch
            {
                process.text = "Connection failed, Server is Down";
                state = State.Failed;
            }

            

        }

        public void Joined()
        {
             state = State.Joined;
        }

        public void Ready()
        {
            state = State.Ready;
        }

        void Update()
        {
            if(state == State.Ready)
            {
                process.text = "You're Ready, The game will be started soon";
            }
            if (state == State.Joined)
            {
                process.text = "You've joined the room, You are player "+PlayerManager.LocalCharacterID;
            }
            if (state == State.Failed)
            {
                process.text = "Connection failed, Server might be down, Please see our Website for server status"; // Actually we have no website
            }
            if(state == State.Connected)
            {
                process.text = "Joining room.........";
            }
            if(state == State.Start)
            {
                SceneManager.LoadScene("newversion/Game");
            }
            
        }

        void OnApplicationQuit()
        {
            ConnectionHandler.receive.Abort();
            if (ConnectionHandler.client != null)
                ConnectionHandler.client.Close();
        }

        public void GetReady()
        {
            GameManager.GetReady();
        }

        public void StartGame()
        {
            state = State.Start;
        }


    }
}