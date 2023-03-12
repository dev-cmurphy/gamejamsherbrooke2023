using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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


        [SerializeField] protected AK.Wwise.Event m_travelStartEvent, m_travelStopEvent;


        [SerializeField] private GameObject m_instantiateOnDestruction;

        private bool m_isActive;

        private Animator m_animator;

        private void Awake()
        {
            m_animator = GetComponent<Animator>();
        }

        public void Activate()
        {
            m_travelStartEvent.Post(gameObject);
            m_isActive = true;
        }

        public void MarkAsDone()
        {
            m_travelStopEvent.Post(gameObject);
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

            m_isActive = false;
            m_travelStopEvent.Post(gameObject);
            Destroy(this.gameObject, m_sustainTime);
        }
        public override IEnumerator Execute()
        {
            for (float t = 0; t < 1; t += Time.fixedDeltaTime)
            {
                transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);
                yield return new WaitForFixedUpdate();
            }
            StartCoroutine(GoToTarget(m_target.transform));

            yield return null;
        }

        public override void Prepare()
        {
            Debug.Log("Preparing range attack...");
            m_isActive = false;
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
                m_travelStopEvent.Post(gameObject);
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