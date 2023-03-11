using System.Collections;
using UnityEngine;

namespace kingcrimson.gameplay
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private GameTime m_gameTime;

        private void Start()
        {
            m_gameTime.StartTime();
        }
    }
}