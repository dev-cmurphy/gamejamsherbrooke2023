using System.Collections;
using UnityEngine;

namespace kingcrimson.utils
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] Transform m_target;

        [SerializeField] Camera m_camera;

        [Min(0f)]
        [SerializeField] private float m_lerpSpeed;

        private void FixedUpdate()
        {
            float z = transform.position.z;

            Vector3 pos = Vector3.Lerp(transform.position, m_target.transform.position, Time.deltaTime * m_lerpSpeed);
            pos.z = z;
            transform.position = pos;
        }
    }
}