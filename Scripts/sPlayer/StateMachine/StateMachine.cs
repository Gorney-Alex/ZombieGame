using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State CurrentState { get; private set; }

    public void ChangeState(State NewState)
    {
        if (CurrentState != null)
        {
            CurrentState.Exit();
        }
        CurrentState = NewState;
        CurrentState.Enter();
    }

    private void Update()
    {
        if (CurrentState != null)
        {
            CurrentState.Do();
        }
    }

    private void FixedUpdate()
    {
        if (CurrentState != null)
        {
            CurrentState.Do();
        }
    }
}
