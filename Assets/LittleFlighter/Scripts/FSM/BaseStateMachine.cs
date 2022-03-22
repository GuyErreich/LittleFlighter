using UnityEngine;

namespace FSM
{
    public class BaseStateMachine : MonoBehaviour
    {
        [SerializeField] private BaseState _initialState;

        public BaseState CurrentState {get; set;}

        private void Awake()
        {
            CurrentState = _initialState;
        }

        private void Update()
        {
            CurrentState.Execute(this);
        }
    }
}