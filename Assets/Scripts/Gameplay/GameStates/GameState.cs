using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace kingcrimson.gameplay
{
    internal abstract class GameState : MonoBehaviour
    {
        protected Game m_game;

        public void Init(Game game)
        {
            m_game = game;
            InternalInit();
        }

        protected virtual void InternalInit()
        {
        }

        public abstract GameState HandleContext();
    }
}
