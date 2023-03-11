using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace kingcrimson.gameplay
{
    public class GameTime : MonoBehaviour
    {
        [Min(1f)]
        [SerializeField] private float m_realTimeDuration;

        [Min(1f)]
        [SerializeField] private int m_gameMinutes;

        private bool m_isTimePassing;
        private float m_timeElapsed;
        private float m_minuteTimer;
        private int m_minuteCount;
        private float m_minuteDuration;

        public UnityEvent OnEndReached;
        public UnityEvent<int> OnNewMinute;

        private void Awake()
        {
            m_isTimePassing = false;
            m_timeElapsed = 0;
            m_minuteTimer = 0;
            m_minuteCount = 0;

            m_minuteDuration = m_realTimeDuration / m_gameMinutes;

            OnEndReached = new UnityEvent();

            Debug.Log($"Starting with {m_minuteDuration} seconds per game minute.");
        }

        private void Update()
        {
            if (m_isTimePassing)
            {
                m_timeElapsed += Time.deltaTime;
                m_minuteTimer += Time.deltaTime;

                while (m_minuteTimer >= m_minuteDuration)
                {
                    m_minuteTimer -= m_minuteDuration;
                    m_minuteCount++;
                    OnNewMinute.Invoke(m_minuteCount);
                }

                if (m_timeElapsed > m_realTimeDuration)
                {
                    ResetTime();
                    OnEndReached.Invoke();
                }
            }
        }

        public void StartTime()
        {
            m_isTimePassing = true;
        }

        public void PauseTime()
        {
            m_isTimePassing = false;
        }

        public void ResetTime()
        {
            m_isTimePassing = false;
            m_timeElapsed = 0;
        }
    }
}