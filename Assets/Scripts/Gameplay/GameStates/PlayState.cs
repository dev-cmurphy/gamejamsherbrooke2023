using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;

namespace kingcrimson.gameplay
{
    internal class PlayState : GameState
    {
        [SerializeField] private Player m_player;
        [SerializeField] private EndState m_endState;

        private bool m_readyToEnd;

        protected override void InternalInit()
        {
            m_game.StartTime();
            m_player.OnSleep.AddListener(OnPlayerSleep);
            m_readyToEnd = false;
        }

        public void Pause()
        {

        }

        public override GameState HandleContext()
        {
            if (m_readyToEnd)
            {
                m_player.OnSleep.RemoveListener(OnPlayerSleep);
                m_endState.Init(m_game);
                return m_endState;
            }
            return this;
        }

        private void OnPlayerSleep()
        {
            m_readyToEnd = true;
        }
    }
}
