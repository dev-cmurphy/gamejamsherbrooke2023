using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace kingcrimson.gameplay
{
    public class Player : MonoBehaviour
    {
        [Min(1f)]
        [SerializeField] private float m_maxWokeness;

        [SerializeField] private AK.Wwise.RTPC m_wokenessRTPC;

        [SerializeField] private Animator m_FOVAnimator;

        private float m_wokeness;

        public float Wokeness { get { return m_wokeness; } }

        // TODO: Hide from inspector AJ (after jam)
        public UnityEvent<float, float> OnWokenessChange;
        public UnityEvent OnSleep;

        private Animator m_animator;

        private Radio m_radio;

        private void Awake()
        {
            m_animator = GetComponent<Animator>();
            m_wokeness = m_maxWokeness;
            OnWokenessChange = new UnityEvent<float, float>();
            OnSleep = new UnityEvent();

            m_radio = null;
        }

        public void ReduceFOV(float duration)
        {
            StartCoroutine(FOVReductionCoroutine(duration));
        }

        private IEnumerator FOVReductionCoroutine(float duration)
        {
            m_FOVAnimator.SetTrigger("Reduce");
            m_animator.SetBool("ReducedFOV", true);
            yield return new WaitForSeconds(duration);
            m_animator.SetBool("ReducedFOV", false);
        }

        private void Update()
        {
            if (m_radio)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    if (m_radio.IsPlaying())
                    {
                        m_radio.Stop();
                    }
                    else
                    {
                        m_radio.Play();
                    }
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (m_radio != null)
            {
                return;
            }

            if (collision.TryGetComponent(out Radio radio))
            {
                m_radio = radio;
                // OnRadioObtained ?
                m_radio.transform.parent = this.transform;
                m_radio.transform.localPosition = Vector2.left * 0.2f;
                m_radio.transform.localScale = Vector2.one * 0.5f;
            }
        }

        public void ReduceWokeness(float amount, bool preventable = false)
        {
            if (m_radio != null && m_radio.IsPlaying() && preventable)
            {
                return;
            }

            m_wokeness -= amount;

            m_wokeness = Mathf.Clamp(m_wokeness, 0, m_maxWokeness);

            OnWokenessChange.Invoke(m_wokeness, amount);

            m_wokenessRTPC.SetValue(gameObject, 100 * (1 - (m_wokeness / m_maxWokeness)));

            if (m_wokeness == 0)
            {
                OnSleep.Invoke();
            }
        }
    }
}

