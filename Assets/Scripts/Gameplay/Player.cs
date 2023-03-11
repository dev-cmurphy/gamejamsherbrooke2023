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

        private float m_wokeness;

        public float Wokeness { get { return m_wokeness; } }

        // TODO: Hide from inspector AJ (after jam)
        public UnityEvent<float, float> OnWokenessChange;
        public UnityEvent OnSleep;

        private Animator m_animator;

        private void Awake()
        {
            m_animator = GetComponent<Animator>();
            m_wokeness = m_maxWokeness;
            OnWokenessChange = new UnityEvent<float, float>();
        }

        public void ReduceFOV(float duration)
        {
            StartCoroutine(FOVReductionCoroutine(duration));
        }

        private IEnumerator FOVReductionCoroutine(float duration)
        {
            m_animator.SetBool("ReducedFOV", true);
            yield return new WaitForSeconds(duration);
            m_animator.SetBool("ReducedFOV", false);
        }

        public void ReduceWokeness(float amount)
        {
            m_wokeness -= amount;

            m_wokeness = Mathf.Clamp(m_wokeness, 0, m_maxWokeness);

            OnWokenessChange.Invoke(m_wokeness, amount);

            if (m_wokeness == 0)
            {
                OnSleep.Invoke();
            }
        }
    }
}

