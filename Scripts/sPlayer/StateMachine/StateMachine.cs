using UnityEngine;
using System.Collections.Generic;

public class StateMachine : MonoBehaviour
{
    private State _currentState;
    
    private Dictionary<System.Type, State> _states = new Dictionary<System.Type, State>();
    
    [Header("References")]
    private PlayerMotor _playerMotor;
    private CharacterAnimatorController _animatorController;
    private InputControlsScript _inputControls;

    public State CurrentState => _currentState;

    public void Initialize(PlayerMotor motor, CharacterAnimatorController animator, InputControlsScript input)
    {
        _playerMotor = motor;
        _animatorController = animator;
        _inputControls = input;

        CreateStates();
        
        ChangeState<StateIdle>();
    }

    private void CreateStates()
    {
        var idleState = gameObject.AddComponent<StateIdle>();
        var walkState = gameObject.AddComponent<StateWalk>();
        var runState = gameObject.AddComponent<StateRun>();
        var jumpState = gameObject.AddComponent<StateJump>();
        var crouchIdleState = gameObject.AddComponent<StateCrouchIdle>();
        var crouchWalkState = gameObject.AddComponent<StateCrouchWalk>();

        idleState.Initialize(_playerMotor, _animatorController, _inputControls);

        _states[typeof(StateIdle)] = idleState;
    }

    public void ChangeState<T>() where T : State
    {
        if (_states.TryGetValue(typeof(T), out State newState))
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }
    }

    private void Update()
    {
        _currentState?.Do();
    }

    private void FixedUpdate()
    {
        _currentState?.FixedDo();
    }
}