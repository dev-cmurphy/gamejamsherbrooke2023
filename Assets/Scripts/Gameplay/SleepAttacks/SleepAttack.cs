using System.Collections;
using UnityEngine;

namespace kingcrimson.gameplay
{
    public abstract class SleepAttack : MonoBehaviour
    {
        public abstract void Prepare();

        public abstract void Execute();

        protected abstract void ApplyEffect(Player p);
    }
}