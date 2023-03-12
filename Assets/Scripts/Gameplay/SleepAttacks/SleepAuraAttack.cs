using Assets.Scripts.Gameplay;
using System.Collections;
using UnityEngine;

namespace kingcrimson.gameplay
{
    public class SleepAuraAttack : SleepAttack
    {
        [Min(0)]
        [SerializeField] private float m_damagePerTick;

        [Min(0)]
        [SerializeField] private float m_tickPerSecond;

        [SerializeField] private AK.Wwise.RTPC m_rtpcMusique;

        [SerializeField] private Transform m_auraTransform;

        [SerializeField] private AK.Wwise.Event m_auraEvent;


        private bool m_isActive;
        private bool m_isOver;

        private Animator m_animator;

        private float m_tickTimer;
        private bool m_tickReady;

        private void Awake()
        {
            m_animator = GetComponent<Animator>();
        }

        public void Activate()
        {
            m_isActive = true;
        }

        public void MarkAsDone()
        {
            m_isOver = true;
        }
        public override IEnumerator Execute()
        {
            m_animator.SetTrigger("StartAttack");
            
            if (!MusicStateManager.HasStarted)
            {
                m_auraEvent.Post(this.gameObject);
            }

            MusicStateManager.TurnOnAura();
            while (!m_isOver)
            {
                // 100: diminué full
                // 0 full son
                
                float scaleFactor = m_auraTransform.localScale.x * 10;
                float value = 100 - scaleFactor;

                value = Mathf.Clamp(value, 0, Mathf.Sqrt(value));
                m_rtpcMusique.SetValue(gameObject, value);
                m_tickTimer += Time.fixedDeltaTime;

                if (m_tickTimer >= 1f / m_tickPerSecond)
                {
                    m_tickReady = true;
                    m_tickTimer = 0;
                }

                yield return new WaitForFixedUpdate();
            }


            MusicStateManager.TurnOffAura();
            m_isActive = false;
            Destroy(this.gameObject);
        }

        public override void Prepare()
        {
            Debug.Log("Preparing range attack...");
            m_isActive = false;
            m_isOver = false;
            m_tickTimer = 0;
            m_tickReady = true;
            transform.position = m_owner.transform.position;
        }

        protected override void ApplyEffect(Player p)
        {
            p.ReduceWokeness(m_damagePerTick, true);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (m_isActive)
            {
                if (m_tickReady)
                {
                    if (collision.TryGetComponent(out Player p))
                    {
                        ApplyEffect(p);
                        m_tickReady = false;
                    }
                }
            }
        }
    }
}