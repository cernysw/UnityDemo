﻿using System;
using UnityEngine;
using System.Collections;

namespace UnityIsland
{

    /// <summary>
    /// This script automatically connects to Photon (using the settings file),
    /// tries to join a random room and creates one if none was found (which is ok).
    /// </summary>
    public class GameManager : Photon.MonoBehaviour
    {
        /// <summary>Connect automatically? If false you can set this to true later on or call ConnectUsingSettings in your own scripts.</summary>
        public bool AutoConnect = true;

        public byte Version = 1;

        /// <summary>if we don't want to connect in Start(), we have to "remember" if we called ConnectUsingSettings()</summary>
        private bool ConnectInUpdate = true;


        public virtual void Start()
        {
            PhotonNetwork.autoJoinLobby = false; // we join randomly. always. no need to join a lobby to get the list of rooms.
            //PhotonNetwork.sendRateOnSerialize = PhotonNetwork.sendRate = 10;
        }

        public virtual void Update()
        {
            if (ConnectInUpdate && AutoConnect && !PhotonNetwork.connected)
            {
                Debug.Log("Update() was called by Unity. Scene is loaded. Let's connect to the Photon Master Server. Calling: PhotonNetwork.ConnectUsingSettings();");

                ConnectInUpdate = false;
                PhotonNetwork.ConnectUsingSettings(Version + "." + SceneManagerHelper.ActiveSceneBuildIndex);
            }
        }


        // below, we implement some callbacks of PUN
        // you can find PUN's callbacks in the class PunBehaviour or in enum PhotonNetworkingMessage


        public virtual void OnConnectedToMaster()
        {
            Debug.Log("OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room. Calling: PhotonNetwork.JoinRandomRoom();");
            PhotonNetwork.JoinRandomRoom();
        }

        public virtual void OnJoinedLobby()
        {
            Debug.Log("OnJoinedLobby(). This client is connected and does get a room-list, which gets stored as PhotonNetwork.GetRoomList(). This script now calls: PhotonNetwork.JoinRandomRoom();");
            PhotonNetwork.JoinRandomRoom();
        }

        public virtual void OnPhotonRandomJoinFailed()
        {
            Debug.Log("OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one.");
            PhotonNetwork.CreateRoom(null, new RoomOptions() {MaxPlayers = 4, Plugins = new string[] { "UnityIslandPlugin" } }, null);
            //PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 4, Plugins = new string[] { "Webhooks" } }, null);
        }

        // the following methods are implemented to give you some context. re-implement them as needed.

        public virtual void OnFailedToConnectToPhoton(DisconnectCause cause)
        {
            Debug.LogError(this + " Connection to Photon failed. Cause: " + cause);
            Debug.Log(this + " using LocalGameObjectCreator");
            ServiceLocator.GameObjectCreator = LocalGameObjectCreator.Instance;
            CreatePlayer();
        }

        public void OnJoinedRoom()
        {
            Debug.Log(this + " OnJoinedRoom() called by PUN.");
            Debug.Log(this + " using PhotonGameObjectCreator");
            ServiceLocator.GameObjectCreator = PhotonGameObjectCreator.Instance;
            CreatePlayer();
        }

        public Transform m_playerSpawnPoint;

        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void CreatePlayer()
        {
            Debug.Log(this + " Instantiating ... ");
            var player = ServiceLocator.GameObjectCreator.CreateStealthBomber(m_playerSpawnPoint);
            player.GetComponent<MeshRenderer>().material.color = Color.red;
            GameObject.FindObjectOfType<Camera>().GetComponent<CameraControler>().m_observerObject = player;
            GameObject.Find("HUD").GetComponent<HUDController>().tracedObject = player;

        }

    }

}