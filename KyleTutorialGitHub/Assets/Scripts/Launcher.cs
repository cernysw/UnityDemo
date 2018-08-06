using Photon;
using UnityEngine;

namespace A.Kyle
{
    public class Launcher : PunBehaviour
    {
        string _gameVersion = "1";
        private void Awake()
        {
            PhotonNetwork.autoJoinLobby = false;
            PhotonNetwork.automaticallySyncScene = true;
        }

        // Use this for initialization
        void Start()
        {
            if (PhotonNetwork.connected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                PhotonNetwork.ConnectUsingSettings(_gameVersion);
            }
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("OnConnectedToMaster");
        }

        public override void OnDisconnectedFromPhoton()
        {
            Debug.Log("OnDisconnectedFromPhoton");
        }


    }
}