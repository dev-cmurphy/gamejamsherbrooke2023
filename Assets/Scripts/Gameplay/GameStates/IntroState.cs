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
    internal class IntroState : GameState
    {
        [SerializeField] private PlayState m_playState;

        private Coroutine m_introCoroutine;

        private bool m_readyForGame;

        protected override void InternalInit()
        {
            m_introCoroutine = null;
            m_readyForGame = false;
        }

        public void MarkReady()
        {
            m_readyForGame = true;
        }

        public override GameState HandleContext()
        {
            if (m_introCoroutine == null)
            {
                m_introCoroutine = m_game.StartCoroutine(IntroCoroutine());
            }
            else
            {
                if (m_readyForGame)
                {
                    m_playState.Init(m_game);
                    return m_playState;
                }
            }

            return this;
        }

        private IEnumerator IntroCoroutine()
        {
            yield return null;
        }
    }
}
