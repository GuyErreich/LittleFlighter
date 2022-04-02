using UnityEngine;

namespace LittleFlighter.FSM
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