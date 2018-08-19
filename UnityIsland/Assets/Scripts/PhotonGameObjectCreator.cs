using UnityEngine;

namespace UnityIsland
{
    public class PhotonGameObjectCreator : Photon.MonoBehaviour, IGameObjectCreator
    {
        public string m_playerPrefabName = "Stealth_Bomber";
        public static IGameObjectCreator Instance { get; private set; }

        void Awake()
        {
            Instance = this;
        }

        public GameObject CreateStealthBomber(Transform transform)
        {
            var player = PhotonNetwork.Instantiate(m_playerPrefabName, transform.position, transform.rotation, 0);
            return player;
        }
    }
}

