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
        private float m_minuteTimer;
        private int m_minuteCount;
        private float m_minuteDuration;

        public UnityEvent OnEndReached;
        public UnityEvent<int> OnNewMinute;

        private float m_timeFlowRate;

        private void Awake()
        {
            m_isTimePassing = false;
            m_minuteTimer = 0;
            m_minuteCount = 0;

            m_minuteDuration = m_realTimeDuration / m_gameMinutes;

            m_timeFlowRate = 1f;

            OnEndReached = new UnityEvent();
            OnNewMinute = new UnityEvent<int>();

            Debug.Log($"Starting with {m_minuteDuration} seconds per game minute.");
        }

        private void TickGameTime()
        {
            m_minuteTimer += Time.deltaTime * m_timeFlowRate;

            while (m_minuteTimer >= m_minuteDuration)
            {
                m_minuteTimer -= m_minuteDuration;
                m_minuteCount++;
                OnNewMinute.Invoke(m_minuteCount);
            }

            if (m_minuteCount > m_gameMinutes)
            {
                ResetTime();
                OnEndReached.Invoke();
            }
        }

        private void Update()
        {
            if (m_isTimePassing)
            {
                TickGameTime();
            }
        }

        public bool IsTimePassing()
        {
            return m_isTimePassing;
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
            m_minuteCount = 0;
        }
    }
}