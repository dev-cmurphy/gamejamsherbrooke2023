using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kingcrimson.gameplay
{
    internal abstract class SleepState
    {
        private Sleep m_owner;

        public SleepState(Sleep sleep)
        {
            m_owner = sleep;
        }
    }
}
