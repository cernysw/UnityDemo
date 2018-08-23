using System;
using UnityEngine;

namespace UnityIsland
{
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