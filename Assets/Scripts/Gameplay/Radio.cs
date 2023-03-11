using System.Collections;
using UnityEngine;

namespace kingcrimson.gameplay
{
    public class Radio : MonoBehaviour
    {
        [SerializeField] private float m_maxBattery;
        [SerializeField] private float m_batteryDecayRate;
        [SerializeField] private GameTime m_gameTime;

        [SerializeField] private AK.Wwise.Event m_playRadioEvent, m_stopRadioEvent;

        private float m_currentBattery;
        private bool m_inUse;

        private void Awake()
        {
            m_currentBattery = m_maxBattery;
            m_inUse = false;
        }

        private void FixedUpdate()
        {
            if (m_gameTime.IsTimePassing() )
            {
                if (m_inUse && m_currentBattery > 0)
                {
                    m_currentBattery -= m_batteryDecayRate * Time.fixedDeltaTime;

                    if (m_currentBattery <= 0)
                    {
                        m_inUse = false;
                        m_currentBattery = 0;
                    }
                }
            }
        }

        public bool IsPlaying()
        {
            return m_inUse;
        }

        public void Play()
        {
            if (!m_inUse)
            {
                if (m_currentBattery > 0)
                {
                    m_inUse = true;
                    m_playRadioEvent.Post(gameObject);
                }
            }
        }

        public void Stop()
        {
            m_inUse = false;
            m_stopRadioEvent.Post(gameObject);
        }
    }
}