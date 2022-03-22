// using Demo.Enemy;
using FSM;
using UnityEngine;
using UnityEngine.AI;

namespace FSM.Enemy.Actions
{
    [CreateAssetMenu(menuName = "FSM/Actions/Attack")]
    public class AttackAction : FSMAction
    {
        public override void Execute(BaseStateMachine stateMachine)
        {
           // TODO: lock on target and shoot it
        }
    }
}