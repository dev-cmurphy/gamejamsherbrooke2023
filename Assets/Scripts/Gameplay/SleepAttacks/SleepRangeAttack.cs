using System.Collections;
using UnityEngine;

namespace kingcrimson.gameplay
{
    public class SleepRangeAttack : SleepAttack
    {
        [Min(0)]
        [SerializeField] private float m_lifeTime;

        [Min(0)]
        [SerializeField] private float m_sustainTime;

        [Min(0)]
        [SerializeField] private float m_speed;

        [Min(0)]
        [SerializeField] private float m_effectDuration;

        [Min(0)]
        [SerializeField] private float m_playerWindUpOffset;

        [SerializeField] private GameObject m_instantiateOnDestruction;

        private bool m_isActive;
        private bool m_isOver;

        private Animator m_animator;

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

        private IEnumerator GoToTarget(Transform target)
        {
            m_animator.SetTrigger("StartAttack");
            for(float t = 0; t < m_lifeTime; t += Time.fixedDeltaTime)
            {
                Vector3 dir = target.position - transform.position;
                dir.Normalize();
                Vector3 newPos = transform.position + (m_speed * Time.fixedDeltaTime * dir);
                transform.position = newPos;
                yield return new WaitForFixedUpdate();
            }
            yield return null;
        }
        public override IEnumerator Execute()
        {
            StartCoroutine(GoToTarget(m_target.transform));

            yield return null;
        }

        public override void Prepare()
        {
            Debug.Log("Preparing range attack...");
            m_isActive = false;
            m_isOver = false;
            transform.position = m_owner.transform.position;
        }

        protected override void ApplyEffect(Player p)
        {
            p.ReduceFOV(m_effectDuration);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (m_isActive)
            {
                m_isActive = false;


                if (m_instantiateOnDestruction)
                {
                    var go = GameObject.Instantiate(m_instantiateOnDestruction);
                    go.transform.position = transform.position;
                }

                if (collision.TryGetComponent(out Player p))
                {
                    ApplyEffect(p);
                }

                Destroy(this.gameObject);

            }
        }
    }
}