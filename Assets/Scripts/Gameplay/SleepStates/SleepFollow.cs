using UnityEngine;

namespace kingcrimson.gameplay
{
    internal class SleepFollow : SleepState
    {
        public SleepFollow(Sleep owner) : base(owner)
        { }

        public override SleepState HandleContext(StateContext context)
        {
            Vector3 displacement = context.Player.transform.position - m_owner.transform.position;

            displacement.Normalize();
            displacement = context.DeltaTime * m_owner.Speed * displacement;

            m_owner.transform.position = m_owner.transform.position + displacement;

            return this;
        }
    }
}
