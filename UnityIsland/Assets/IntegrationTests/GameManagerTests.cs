using System;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using NUnit.Framework.Constraints;


namespace UnityIsland
{
    public class GameManagerTests
    {
        [UnityTest]
        public IEnumerator ConnectionAfterStart()
        {
            new GameObject().AddComponent<GameManager>();
            var timeout = TimeSpan.FromSeconds(5);
            yield return new WaitUntilOrTimeout(() => PhotonNetwork.connected, timeout);
            Assert.IsTrue(PhotonNetwork.connected, "Not connected to Photon in " + timeout);
        }
    }
}
