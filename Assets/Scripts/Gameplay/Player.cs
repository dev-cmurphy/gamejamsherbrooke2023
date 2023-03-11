using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace kingcrimson.gameplay
{

    public class Player : MonoBehaviour
    {
        [Min(1f)]
        [SerializeField] private float m_maxWokeness;

        private float m_wokeness;


        public float Wokeness { get { return m_wokeness; } }

        // TODO: Hide from inspector AJ (after jam)
        public UnityEvent<float, float> OnWokenessChange;

        private void Awake()
        {
            m_wokeness = m_maxWokeness;
            OnWokenessChange = new UnityEvent<float, float>();
        }
    }
}

