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

        private void Start()
        {
            m_gameTime.StartTime();

            m_gameTime.OnNewMinute.AddListener(ReducePlayerWokeness);
        }

        private void ReducePlayerWokeness(int _)
        {
            float loss = m_wokenessDecayPerTick;

            m_player.ReduceWokeness(loss);
        }
    }
}