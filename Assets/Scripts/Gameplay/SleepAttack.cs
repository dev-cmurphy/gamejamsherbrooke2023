using System.Collections;
using UnityEngine;

namespace kingcrimson.gameplay
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class SleepAttack : MonoBehaviour
    {
        protected abstract void ApplyEffect(Player p);

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player p))
            {
                ApplyEffect(p);
            }
        }
    }
}