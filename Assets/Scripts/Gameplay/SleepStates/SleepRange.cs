using System.Collections;
using UnityEngine;

namespace kingcrimson.gameplay
{
    internal class SleepRange : SleepAttackState
    {
        public SleepRange(SleepState previousState, Sleep sleep) : base(previousState, sleep)
        {
        }

        protected override IEnumerator AttackCoroutine()
        {
            SleepAttack rangedAttack = GameObject.Instantiate(m_owner.RangedAttackPrefab);
            rangedAttack.Initialize(m_owner, m_owner.Player);

            rangedAttack.Prepare();

            yield return rangedAttack.Execute();

            Debug.Log("Ranged attack launched..");
        }
    }
}
