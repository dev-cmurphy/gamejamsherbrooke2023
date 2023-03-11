using System.Collections;
using UnityEngine;

namespace kingcrimson.gameplay
{
    // vraiment besoin de trois classes différentes ?
    internal class SleepMelee : SleepAttackState
    {
        public SleepMelee(SleepState previousState, Sleep sleep) : base(previousState, sleep)
        {
        }

        protected override IEnumerator AttackCoroutine()
        {
            SleepAttack meleeAttack = GameObject.Instantiate(m_owner.MeleeAttackPrefab);
            meleeAttack.Initialize(m_owner, m_owner.Player);

            meleeAttack.Prepare();

            yield return meleeAttack.Execute();

            Debug.Log("Melee attack done.");
        }
    }
}
