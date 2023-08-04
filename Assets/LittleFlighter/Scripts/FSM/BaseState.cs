using UnityEngine;

namespace LittleFlighter.FSM
{
    public class BaseState : ScriptableObject
    {
        public virtual void Execute(BaseStateMachine machine) { }
    }
}