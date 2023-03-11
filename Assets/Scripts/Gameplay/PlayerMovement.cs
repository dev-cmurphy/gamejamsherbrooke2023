using System.Collections;
using UnityEngine;

namespace kingcrimson.gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [Min(0f)]
        [SerializeField] private float m_speed;

        private Rigidbody2D m_rigidbody;

        private void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody2D>();
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

            m_rigidbody.MovePosition(newPos);
        }
    }
}