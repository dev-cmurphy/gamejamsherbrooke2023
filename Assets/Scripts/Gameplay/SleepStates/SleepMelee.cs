using System.Collections;
using UnityEngine;

namespace kingcrimson.gameplay
{
    internal class SleepMelee : SleepAttackState
    {
        public SleepMelee(SleepState previousState, Sleep sleep) : base(previousState, sleep)
        {
        }

        protected override IEnumerator AttackCoroutine()
        {
            Debug.Log("Melee attack...");

            SleepAttack meleeAttack = GameObject.Instantiate(m_owner.MeleeAttackPrefab);

            meleeAttack.Prepare();

            yield return new WaitForSeconds(1.5f);

            meleeAttack.Execute();

            Debug.Log("Melee attack done.");
        }
    }
}
