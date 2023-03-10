using Assets.Scripts.Gameplay;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace kingcrimson.gameplay
{
    public class Radio : MonoBehaviour
    {
        [SerializeField] private float m_maxBattery;
        [SerializeField] private float m_batteryDecayRate;
        [SerializeField] private GameTime m_gameTime;

        [SerializeField] private AK.Wwise.Event m_playMusicEvent;
        [SerializeField] private Slider m_slider;

        [SerializeField] private ParticleSystem notes;

        private float m_currentBattery;
        private bool m_inUse;

        private void Awake()
        {
            m_currentBattery = m_maxBattery;
            m_inUse = false;
            m_slider.gameObject.SetActive(false);
            m_slider.maxValue = m_maxBattery;
            
        }

        private void FixedUpdate()
        {
            if (m_gameTime.IsTimePassing() )
            {
                if (m_inUse && m_currentBattery > 0)
                {
                    m_slider.gameObject.SetActive(true);
                    m_currentBattery -= m_batteryDecayRate * Time.fixedDeltaTime;

                    m_slider.value = m_currentBattery;

                    if (m_currentBattery <= 0)
                    {
                        notes.Stop();
                        m_inUse = false;
                        m_currentBattery = 0;
                        MusicStateManager.TurnOffRadio();
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
                    if (!MusicStateManager.HasStarted)
                        m_playMusicEvent.Post(this.gameObject);

                    StartCoroutine(PlayCoroutine());
                }
            }
        }

        private IEnumerator PlayCoroutine()
        {
            MusicStateManager.TurnOnRadio();
            yield return new WaitForSeconds(0.34f);
            notes.Play();
            m_inUse = true;
        }

        public void Stop()
        {
            m_inUse = false;
            AkSoundEngine.SetState("Music", "Silence");
            MusicStateManager.TurnOffRadio();
            notes.Stop();
        }
    }
}