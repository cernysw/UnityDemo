using System.Collections;
using System.Collections.Generic;
using Photon;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kyle
{
    public class GameManager : PunBehaviour
    {
        public GameObject playerPrefab;
        public void Awake()
        {
            Debug.Log(this + " Awake()");
        }

        public void Start()
        {
            Debug.Log(this + " Start()");
            PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 3f, 0f), Quaternion.identity, 0);
        }

        public void OnDestroy()
        {
            Debug.LogWarning(this + " OnDestroy()");
        }


        // called by Photon
        public override void OnLeftRoom()
        {
            Debug.Log(this + " OnLeftRoom()");
            SceneManager.LoadScene(0);
        }
        
        // called from button
        public void LeaveRoom()
        {
            Debug.Log(this + " LeaveRoom()");
            PhotonNetwork.LeaveRoom();
        }


    }
}
