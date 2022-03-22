using FSM;
using UnityEngine;

namespace FSM.Enemy.Decisions
{
    [CreateAssetMenu(menuName = "FSM/Decisions/In Shooting Range")]
    public class InShootingRangeDecision : Decision
    {
        public override bool Decide(BaseStateMachine stateMachine)
        {
            // TODO: Create decision
            return true;
        }
    }
}