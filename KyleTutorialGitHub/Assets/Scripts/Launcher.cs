﻿using Photon;
using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Kyle
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
            Debug.Log(this + " Start");
            if (PhotonNetwork.connectionStateDetailed == ClientState.ConnectedToMaster)
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
            Debug.Log(this + " OnConnectedToMaster");
            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnDisconnectedFromPhoton()
        {
            Debug.Log(this + " OnDisconnectedFromPhoton");
        }

        public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
        {
            Debug.LogWarning(this + " OnPhotonRandomJoinFailed(). Calling: PhotonNetwork.CreateRoom(); Details:  " + string.Join(", ",codeAndMsg.Select(o => o.ToString()).ToArray()));
            PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 4 }, null);
        }

        public override void OnJoinedRoom()
        {
            Debug.Log(this + " OnJoinedRoom()");
            if (PhotonNetwork.room.PlayerCount == 1)
            {
                Debug.Log(this + " OnJoinedRoom(): Loading level 1");
                PhotonNetwork.LoadLevel(1);
            }
        }
    }
}