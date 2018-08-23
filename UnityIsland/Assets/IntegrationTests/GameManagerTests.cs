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

        [Test]
        public void GameManagerTestsSimplePasses()
        {
            // Use the Assert class to test conditions.
        }

        // A UnityTest behaves like a coroutine in PlayMode
        // and allows you to yield null to skip a frame in EditMode
        [UnityTest]
        public IEnumerator GameManagerTestsWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // yield to skip a frame
            yield return null;
        }

        [UnityTest]
        public IEnumerator ConnectionAfterStart()
        {
            new GameObject().AddComponent<GameManager>();
            var timeout = TimeSpan.FromSeconds(5);
            yield return new WaitUntilOrTimeout(() => PhotonNetwork.connected, timeout);
            Assert.IsTrue(PhotonNetwork.connected, "Not connected to Photon in " + timeout);
        }



        //IEnumerator WaitUntilOrTimeout(Func<bool> predicate, float timeoutSec)
        //{
        //    while (!predicate())
        //    {
        //        yield return null;
        //        timeoutSec -= Time.deltaTime;
        //        if (timeoutSec <= 0f) break;
        //    }
        //}
    }


    public sealed class WaitUntilOrTimeout : CustomYieldInstruction
    {
        private readonly Func<bool> m_Predicate;
        private readonly TimeSpan m_Timeout;
        private readonly DateTime m_StartTime;

        public WaitUntilOrTimeout(Func<bool> predicate, TimeSpan timeout)
        {
            m_Predicate = predicate;
            m_Timeout = timeout;
            m_StartTime = DateTime.Now;
        }

        public override bool keepWaiting
        {
            get { return !this.m_Predicate() || DateTime.Now - m_StartTime > m_Timeout; }
        }
    }
}
