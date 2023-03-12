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
    [RequireComponent(typeof(Animator))]
    internal class EndState : GameState
    {
        [SerializeField] private GameTime m_gameTime;
        private Animator m_animator;
        [SerializeField] private AK.Wwise.Event m_endLoseEvent, m_endWinEvent;
        private bool m_isVictory;

        public void IsVictory(bool victory)
        {
            m_isVictory = victory;
        }

        protected override void InternalInit()
        {
            m_gameTime.PauseTime();
            m_animator = GetComponent<Animator>();

            StartCoroutine(EndCoroutine());
        }


        public override GameState HandleContext()
        {
            return this;
        }

        private IEnumerator EndCoroutine()
        {
            m_animator.SetBool("Victory", m_isVictory);
            m_animator.SetTrigger("End");

            if (!m_isVictory)
            {
                m_endLoseEvent.Post(gameObject);
            }
            else
            {
                m_endWinEvent.Post(gameObject);
            }
            yield return null;
        }
    }
}
