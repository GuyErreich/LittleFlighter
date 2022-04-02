using UnityEngine;
using LittleFlighter.Enemy;

namespace LittleFlighter.FSM.Enemy.Actions
{
    [CreateAssetMenu(menuName = "FSM/Actions/Track")]
    public class TrackAction : FSMAction
    {
        public override void Execute(BaseStateMachine stateMachine)
        {
            var enemy = stateMachine.GetComponent<EnemyController>();

            enemy.TrackMode();
        }
    }
}