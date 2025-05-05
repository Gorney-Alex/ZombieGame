using UnityEngine;

public class CharacterAnimatorController
{
    private const string IS_WALK = "_isWalk";
    private const string IS_RUN = "_isRun";
    private const string IS_CROUCH_DOWN = "_isCrouchDown";
    private const string IS_CROUCH_UP = "_isCrouchUp";
    private const string IS_CROUCH_IDLE = "_isCrouchIdle";
    private const string IS_CROUCH_WALK = "_isCrouchWalk";
    private const string IS_CROUCH_JUMP_UP = "_isJumpUp";
    
    private Animator _animator;

    public CharacterAnimatorController(Animator animator)
    {
        _animator = animator;
    }

    public void UpdateMovementState(Vector3 moveDirection, bool IsControlsScriptRunning, bool IsControlsScriptCrouching, bool IsJump)
    {
        float moveAmount = new Vector2(moveDirection.x, moveDirection.z).magnitude;
        bool isWalking = moveAmount > 0.1f;
        bool isRunning = isWalking && IsControlsScriptRunning;
        
        _animator.SetBool(IS_CROUCH_JUMP_UP, IsJump && !IsControlsScriptCrouching);
        
        _animator.SetBool(IS_WALK, isWalking && !isRunning && !IsControlsScriptCrouching && !IsJump);
        _animator.SetBool(IS_RUN, isRunning && !IsControlsScriptCrouching && !IsJump);

        _animator.SetBool(IS_CROUCH_IDLE, IsControlsScriptCrouching && !isWalking && !IsJump);
        _animator.SetBool(IS_CROUCH_WALK, IsControlsScriptCrouching && isWalking && !IsJump);

        _animator.SetBool(IS_CROUCH_DOWN, IsControlsScriptCrouching && !IsJump);
        _animator.SetBool(IS_CROUCH_UP, !IsControlsScriptCrouching && !IsJump);

    }
}