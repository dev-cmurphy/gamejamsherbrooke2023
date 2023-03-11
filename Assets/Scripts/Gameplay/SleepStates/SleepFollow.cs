using UnityEngine;

namespace kingcrimson.gameplay
{
    internal class SleepFollow : SleepState
    {
        private float m_lastMeleeTime;

        public SleepFollow(Sleep owner) : base(owner)
        {
            m_lastMeleeTime = Time.time;
        }

        public override SleepState HandleContext(StateContext context)
        {
            Vector3 displacement = context.Player.transform.position - m_owner.transform.position;

            if (ShouldMelee(displacement))
            {
                m_lastMeleeTime = Time.time;
                return new SleepMelee(this, m_owner);
            }

            displacement.Normalize();
            displacement = context.DeltaTime * m_owner.Speed * displacement;

            m_owner.transform.position = m_owner.transform.position + displacement;

            return this;
        }

        private bool ShouldMelee(Vector3 playerDistance)
        {
            if (playerDistance.magnitude < 5 && (Time.time - m_lastMeleeTime) > 4)
            {
                return true;
            }

            return false;
        }
    }
}
