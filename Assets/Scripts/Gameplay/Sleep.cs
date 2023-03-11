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
    }
}