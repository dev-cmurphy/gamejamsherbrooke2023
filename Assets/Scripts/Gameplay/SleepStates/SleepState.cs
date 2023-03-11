using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kingcrimson.gameplay
{
    public struct StateContext
    {
        public Player Player;
        public float DeltaTime;
    }

    internal abstract class SleepState
    {
        protected Sleep m_owner;

        public SleepState(Sleep sleep)
        {
            m_owner = sleep;
        }

        public abstract SleepState HandleContext(StateContext context);
    }
}
