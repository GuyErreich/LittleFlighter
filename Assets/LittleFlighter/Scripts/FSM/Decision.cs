using UnityEngine;

namespace LittleFlighter.FSM
{
    public abstract class Decision : ScriptableObject
    {
        public abstract bool Decide(BaseStateMachine state);
    }
}