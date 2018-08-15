using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityIsland
{

    public class LocalGameObjectCreator : MonoBehaviour, IGameObjectCreator
    {
        public GameObject m_StealthBomberPrefab;

        public static IGameObjectCreator Instance { get; private set; }

        void Awake()
        {
            Instance = this;
        }

        public GameObject CreateStealthBomber(Transform transform)
        {
            return GameObject.Instantiate(m_StealthBomberPrefab, transform);
        }
    }
}

