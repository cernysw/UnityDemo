using System.Collections;
using System.Collections.Generic;
using Photon;
using UnityEngine;

namespace UnityIsland
{

    public class MultiplayerManager : PunBehaviour
    {
        public Transform m_playerSpawnPoint;

        public static MultiplayerManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public override void OnJoinedRoom()
        {
            CreatePlayer();
        }

        public void CreatePlayer()
        {
            Debug.Log(this + " Instantiating ... ");
            var player = ServiceLocator.GameObjectCreator.CreateStealthBomber(m_playerSpawnPoint);
            //var player = PhotonNetwork.Instantiate((m_playerPrefabName, m_playerSpawnPoint, Quaternion.identity, 0);
            player.GetComponent<MeshRenderer>().material.color = Color.red;
            GameObject.FindObjectOfType<Camera>().GetComponent<CameraControler>().m_observerObject = player;
        }
    }
}
