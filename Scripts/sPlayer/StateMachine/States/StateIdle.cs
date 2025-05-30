using UnityEngine;

public class StateIdle : State
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entering Idle State");
        
        animatorController.UpdateMovementState(Vector3.zero, false, inputControls.IsCrouchedState, false);
    }

    public override void Do()
    {
        Vector2 moveInput = inputControls.Move;

        if (moveInput.magnitude > 0.1f)
        {
            if (inputControls.IsCrouchedState)
            {
                playerMotor.GetComponent<StateMachine>().ChangeState(playerMotor.GetComponent<StateCrouchWalk>());
            }
            else
            {
                playerMotor.GetComponent<StateMachine>().ChangeState(playerMotor.GetComponent<StateWalk>());
            }
            return;
        }

        if (inputControls.IsJump && playerMotor.GetComponent<CheckCharacterGrounded>().CheckIsGrounded())
        {
            playerMotor.GetComponent<StateMachine>().ChangeState(playerMotor.GetComponent<StateJump>());
            return;
        }

        if (inputControls.IsCrouchedState)
        {
            playerMotor.GetComponent<StateMachine>().ChangeState(playerMotor.GetComponent<StateCrouchIdle>());
            return;
        }

        animatorController.UpdateMovementState(Vector3.zero, false, inputControls.IsCrouchedState, false);
    }

    public override void Exit()
    {
        Debug.Log("Exiting Idle State");
    }
}