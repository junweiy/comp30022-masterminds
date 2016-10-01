using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Manager;
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
            Ready
        }

        private State state;

        // Use this for initialization
        void Start()
        {


            GameManager.n = this;
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


        void Update()
        {
            if(state == State.Ready)
            {
                process.text = "You're Ready, The game will be started soon";
            }
            if (state == State.Joined)
            {
                process.text = "You've joined the room";
            }
            if (state == State.Failed)
            {
                process.text = "Connection failed, Server might be down, Please see our Website for server status"; // Actually we have no website
            }
            if(state == State.Connected)
            {
                process.text = "Joining room.........";
            }
            
        }


        public static void Ready()
        {
            ConnectionHandler.Send("Ready");
        }

    }
}