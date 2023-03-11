using System.Collections;
using UnityEngine;

namespace kingcrimson.gameplay
{
    public class Game : MonoBehaviour
    {
        // Points de wokeness perdus par tick
        [SerializeField] private float m_wokenessDecayPerTick;

        [SerializeField] private GameTime m_gameTime;
        [SerializeField] private Player m_player;

        [SerializeField] private IntroState m_introState;
        [SerializeField] private PlayState m_playState;

        private GameState m_state;

        private void Start()
        {
            m_gameTime.OnNewMinute.AddListener(ReducePlayerWokeness);

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

        private void ReducePlayerWokeness(int _)
        {
            float loss = m_wokenessDecayPerTick;

            m_player.ReduceWokeness(loss);
        }
    }
}