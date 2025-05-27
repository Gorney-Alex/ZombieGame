using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private State _currentState;

    public void OnServerInitialized(State startAnimation)
    {
        _currentState = startAnimation;
        _currentState.Enter();
    }

    public void ChangeState(State NewState)
    {
        if (_currentState != null)
        {
            _currentState.Exit();
        }
        _currentState = NewState;
        _currentState.Enter();
    }

    private void Update()
    {
        if (_currentState != null)
        {
            _currentState.Do();
        }
    }

    private void FixedUpdate()
    {
        if (_currentState != null)
        {
            _currentState.Do();
        }
    }
}
