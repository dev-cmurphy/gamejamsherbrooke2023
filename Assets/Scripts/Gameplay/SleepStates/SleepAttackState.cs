using System.Collections;
using UnityEngine;

namespace kingcrimson.gameplay
{
    internal abstract class SleepAttackState : SleepState
    {

        private Coroutine m_attackCoroutine;
        private bool m_isAttackDone;
        private readonly SleepState m_previousState;

        public SleepAttackState(SleepState previousState, Sleep sleep) : base(sleep)
        {
            m_attackCoroutine = null;
            m_isAttackDone = false;
            m_previousState = previousState;
        }

        private IEnumerator InternalAttackCoroutine()
        {
            yield return AttackCoroutine();
            m_isAttackDone = true;
        }

        public override sealed SleepState HandleContext(StateContext context)
        {
            if (m_attackCoroutine == null)
            {
                m_attackCoroutine = m_owner.StartCoroutine(InternalAttackCoroutine());
            }
            else if (m_isAttackDone)
            {
                return m_previousState;
            }

            return this;
        }

        protected abstract IEnumerator AttackCoroutine();
    }
}
