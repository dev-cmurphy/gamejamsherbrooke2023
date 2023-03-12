using System.Collections;
using UnityEngine;

namespace kingcrimson.gameplay
{
    public class Game : MonoBehaviour
    {

        [SerializeField] private GameTime m_gameTime;
        [SerializeField] private Player m_player;

        [SerializeField] private IntroState m_introState;
        [SerializeField] private PlayState m_playState;

        private GameState m_state;

        private void Start()
        {
            m_state = m_introState;
            m_introState.Init(this);
        }

        public void StartTime()
        {
            m_gameTime.StartTime();
        }

        private void FixedUpdate()
        {
            m_state = m_state.HandleContext();
        }
    }
}