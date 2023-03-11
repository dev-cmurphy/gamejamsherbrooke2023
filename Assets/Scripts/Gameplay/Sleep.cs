using System.Collections;
using UnityEngine;

namespace kingcrimson.gameplay
{
    [RequireComponent(typeof(Collider2D))]
    public class Sleep : MonoBehaviour
    {
        private SleepState m_state;

        private void Awake()
        {
            //
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