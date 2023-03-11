using System.Collections;
using UnityEngine;

namespace kingcrimson.gameplay
{
    [RequireComponent(typeof(Collider2D))]
    public class Sleep : MonoBehaviour
    {
        [SerializeField] private Player m_player;

        [Min(0f)]
        public float Speed;

        public SleepAttack MeleeAttackPrefab;

        private SleepState m_state;

        private StateContext m_context;

        public Player Player { get { return m_player; } }

        private void Awake()
        {
            //

            m_context = new();

        }

        private void Start()
        {
            m_context.Player = m_player;

            m_state = new SleepFollow(this);
        }

        private void FixedUpdate()
        {
            m_context.DeltaTime = Time.fixedDeltaTime;
            m_state = m_state.HandleContext(m_context);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player p))
            {
                p.ReduceWokeness(1f);
            }
        }
    }
}