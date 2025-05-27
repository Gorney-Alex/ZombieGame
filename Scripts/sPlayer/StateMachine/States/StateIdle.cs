using UnityEngine;

public class StateIdle : State
{
    public override void Enter()
    {
        Debug.Log("Idle animation is started");
        isComplete = false;
        startTime = Time.time;
    }

    public override void Do()
    {
        isComplete = true;
    }

    public override void Exit()
    {
        Debug.Log("Exit Idle animation");
    }
}
