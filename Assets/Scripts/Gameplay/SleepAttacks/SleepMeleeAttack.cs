using System.Collections;
using UnityEngine;

namespace kingcrimson.gameplay
{
    public class SleepMeleeAttack : SleepAttack
    {
        [Min(0)]
        [SerializeField] private float m_damage;

        [Min(0)]
        [SerializeField] private float m_sustainTime;

        [Min(0)]
        [SerializeField] private float m_playerWindUpOffset;

        private bool m_isActive;
        private bool m_isOver;
        private bool m_shouldFollowPlayer;

        private Animator m_animator;

        private void Awake()
        {
            m_animator = GetComponent<Animator>();
        }

        public void Activate()
        {
            m_isActive = true;
        }

        public void StopPlayerFollow()
        {
            m_shouldFollowPlayer = false;
        }

        public void MarkAsDone()
        {
            m_isOver = true;
        }

        public override IEnumerator Execute()
        {
            m_animator.SetTrigger("StartAttack");
            while(!m_isOver)
            {
                if (m_shouldFollowPlayer)
                {
                    Vector3 pos = (m_target.transform.position + Vector3.up * m_playerWindUpOffset);
                    transform.position = pos;
                }
                yield return null;
            }
            Destroy(this.gameObject, m_sustainTime);
        }

        public override void Prepare()
        {
            Debug.Log("Preparing melee attack...");
            m_isActive = false;
            m_isOver = false;
            m_shouldFollowPlayer = true;
        }

        protected override void ApplyEffect(Player p)
        {
            p.ReduceWokeness(m_damage);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (m_isActive)
            {
                if(collision.TryGetComponent(out Player p))
                {
                    ApplyEffect(p);
                    m_isActive = false;
                }
            }
        }
    }
}