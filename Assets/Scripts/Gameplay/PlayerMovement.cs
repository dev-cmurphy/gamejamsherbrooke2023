using System.Collections;
using UnityEngine;

namespace kingcrimson.gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [Min(0f)]
        [SerializeField] private float m_speed;

        [SerializeField] private AK.Wwise.Event m_footstepsPlayEvent, m_footstepsStopEvent;

        private bool m_isSoundPlaying;


        private Rigidbody2D m_rigidbody;

        private void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody2D>();
            m_isSoundPlaying = false;
        }

        private Vector2 InputDirection()
        {
            Vector2 dir = Vector2.zero;
            if (Input.GetKey(KeyCode.W))
            {
                dir += Vector2.up;
            }

            if (Input.GetKey(KeyCode.S))
            {
                dir += Vector2.down;
            }

            if (Input.GetKey(KeyCode.A))
            {
                dir += Vector2.left;
            }

            if (Input.GetKey(KeyCode.D))
            {
                dir += Vector2.right;
            }

            return dir.normalized;
        }

        private void FixedUpdate()
        {
            Vector2 dir = InputDirection();

            Vector2 displacement = dir * m_speed;
            Vector3 newPos = transform.position + Time.fixedDeltaTime * new Vector3(displacement.x, displacement.y);

            if (displacement.sqrMagnitude > 0.1f)
            {
                if (!m_isSoundPlaying)
                {
                    m_isSoundPlaying = true;
                    m_footstepsPlayEvent.Post(gameObject);
                }
            }
            else
            {
                m_isSoundPlaying = false;
                m_footstepsStopEvent.Post(gameObject);
            }

            m_rigidbody.MovePosition(newPos);
        }
    }
}