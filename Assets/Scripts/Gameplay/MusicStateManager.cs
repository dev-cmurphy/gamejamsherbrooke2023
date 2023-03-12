using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Gameplay
{
    public static class MusicStateManager
    {
        private static bool m_isRadioOn = false;
        private static bool m_isAuraOn = false;

        public static bool HasStarted = true;

        public static void TurnOnRadio()
        {
            HasStarted = true;
            if (!m_isRadioOn)
            {
                m_isRadioOn = true;
                AkSoundEngine.SetState("Music", "Rock");
            }
        }

        public static void TurnOffRadio()
        {
            if(m_isRadioOn)
            {
                m_isRadioOn = false;
                string state = m_isAuraOn ? "Classique" : "Silence";
                AkSoundEngine.SetState("Music", state);
            }
        }

        public static void TurnOnAura()
        {
            HasStarted = true;
            if (!m_isAuraOn)
            {
                m_isAuraOn = true;
                if (!m_isRadioOn)
                {
                    AkSoundEngine.SetState("Music", "Classique");
                }
            }
        }

        public static void TurnOffAura()
        {
            if (m_isAuraOn) 
            { 
                m_isAuraOn = false;
                string state = m_isRadioOn ? "Rock" : "Silence";
                AkSoundEngine.SetState("Music", state);
            }
        }
    }
}
