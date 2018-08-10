using System.Collections;
using System.Collections.Generic;
using Photon;
using UnityEngine;

namespace UnityIsland
{

    public class MultiplayerManager : PunBehaviour
    {
        public Vector3 m_spawnPoint = new Vector3(1000, 500, -300);
        public string m_playerPrefabName = "Stealth_Bomber";


        public override void OnJoinedRoom()
        {
            Debug.Log(this + " Instantiating ... ");
            var player = PhotonNetwork.Instantiate(m_playerPrefabName, m_spawnPoint, Quaternion.identity, 0);
            player.GetComponent<MeshRenderer>().material.color = Color.red;
            GameObject.FindObjectOfType<Camera>().GetComponent<CameraControler>().m_observerObject = player;
        }
    }
}
