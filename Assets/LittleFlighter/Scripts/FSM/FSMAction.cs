using UnityEngine;

namespace LittleFlighter.FSM
{
    public abstract class FSMAction : ScriptableObject
    {
        public abstract void Execute(BaseStateMachine stateMachine);
    }
}