using UnityEngine;
using LittleFlighter.Enemy;

namespace LittleFlighter.FSM.Enemy.Decisions
{
    [CreateAssetMenu(menuName = "FSM/Decisions/In Shooting Range")]
    public class InShootingRangeDecision : Decision
    {
        public override bool Decide(BaseStateMachine stateMachine)
        {
            var enemy =  stateMachine.GetComponent<EnemyController>();

            return enemy.IsInRange;
        }
    }
}