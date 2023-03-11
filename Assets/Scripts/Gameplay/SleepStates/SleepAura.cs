using System.Collections;
using UnityEngine;

namespace kingcrimson.gameplay
{
    internal class SleepAura : SleepAttackState
    {
        public SleepAura(SleepState previousState, Sleep sleep) : base(previousState, sleep)
        {
        }

        protected override IEnumerator AttackCoroutine()
        {
            SleepAttack auraAttack = GameObject.Instantiate(m_owner.AuraAttackPrefab);
            auraAttack.Initialize(m_owner, m_owner.Player);

            auraAttack.Prepare();

            yield return auraAttack.Execute();

            Debug.Log("Aura attack launched..");
        }
    }
}
