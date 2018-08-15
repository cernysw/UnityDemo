using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UnityIsland
{

    public interface IGameObjectCreator
    {
        GameObject CreateStealthBomber(Transform transform);
    }

    public static class ServiceLocator
    {
        public static IGameObjectCreator GameObjectCreator { get; set; }

    }
}
