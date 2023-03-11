using UnityEngine;

namespace kingcrimson.gameplay
{
    internal class SleepFollow : SleepState
    {
        private float m_lastMeleeTime;
        private float m_lastRangeTime;

        private float m_lastAuraTime;

        public SleepFollow(Sleep owner) : base(owner)
        {
            m_lastMeleeTime = -10;
            m_lastRangeTime = 0;
            m_lastAuraTime = 0;
        }

        public override SleepState HandleContext(StateContext context)
        {
            Vector3 displacement = context.Player.transform.position - m_owner.transform.position;

            float now = Time.time;
            if (ShouldMelee(displacement))
            {
                m_lastMeleeTime = now;
                return new SleepMelee(this, m_owner);
            }

            if (ShouldAura(now))
            {
                m_lastAuraTime = now;
                return new SleepAura(this, m_owner);
            }

            if (ShouldRange(displacement))
            {
                m_lastRangeTime = now;
                return new SleepRange(this, m_owner);
            }

            displacement.Normalize();
            displacement = context.DeltaTime * m_owner.Speed * displacement;

            m_owner.transform.position = m_owner.transform.position + displacement;

            return this;
        }

        private bool ShouldAura(float now)
        {
            if ((now - m_lastAuraTime) > 30)
            {
                return true;
            }

            return false;
        }

        private bool ShouldMelee(Vector3 playerDistance)
        {
            if (playerDistance.magnitude < 5 && (Time.time - m_lastMeleeTime) > 5)
            {
                return true;
            }

            return false;
        }

        private bool ShouldRange(Vector3 playerDistance)
        {
            float d = playerDistance.magnitude;
            if (d < 15 && d > 5 && (Time.time - m_lastRangeTime) > 10)
            {
                return true;
            }

            return false;
        }
    }
}
