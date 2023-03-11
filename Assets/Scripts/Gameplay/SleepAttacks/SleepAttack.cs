using System.Collections;
using UnityEngine;

namespace kingcrimson.gameplay
{
    public abstract class SleepAttack : MonoBehaviour
    {
        protected Sleep m_owner;
        protected Player m_target;

        public void Initialize(Sleep sleep, Player player)
        {
            m_owner = sleep;
            m_target = player;
        }

        public abstract void Prepare();

        public abstract IEnumerator Execute();

        protected abstract void ApplyEffect(Player p);
    }
}