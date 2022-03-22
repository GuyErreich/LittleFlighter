// using Demo.Enemy;
using FSM;
using UnityEngine;
using UnityEngine.AI;

namespace FSM.Enemy.Actions
{
    [CreateAssetMenu(menuName = "FSM/Actions/Track")]
    public class TrackAction : FSMAction
    {
        public override void Execute(BaseStateMachine stateMachine)
        {
           // TODO: add random rotations to feature track animation
        }
    }
}