using UnityEngine;

public class CharacterAnimatorController
{
    private const string IS_WALK = "_isWalk";
    private const string IS_RUN = "_isRun";
    private const string IS_CROUCH_DOWN = "_isCrouchDown";
    private const string IS_CROUCH_UP = "_isCrouchUp";
    private const string IS_CROUCH_IDLE = "_isCrouchIdle";
    private const string IS_CROUCH_WALK = "_isCrouchWalk";
    private Animator _animator;

    public CharacterAnimatorController(Animator animator)
    {
        _animator = animator;
    }

    public void UpdateMovementState(Vector3 moveDirection, bool IsControlsScriptRunning, bool IsControlsScriptCrouching)
    {
        float moveAmount = new Vector2(moveDirection.x, moveDirection.z).magnitude;
        bool isWalking = moveAmount > 0.1f;
        bool isRunning = isWalking && IsControlsScriptRunning;
        
        _animator.SetBool(IS_WALK, isWalking && !isRunning && !IsControlsScriptCrouching);
        _animator.SetBool(IS_RUN, isRunning && !IsControlsScriptCrouching);

        _animator.SetBool(IS_CROUCH_IDLE, IsControlsScriptCrouching && !isWalking);
        _animator.SetBool(IS_CROUCH_WALK, IsControlsScriptCrouching && isWalking);

        if (IsControlsScriptCrouching)
        {
            _animator.SetTrigger(IS_CROUCH_DOWN);
        }
        else
        {
            _animator.SetTrigger(IS_CROUCH_UP);
        }
    }
}
