using UnityEngine;

public class CharacterAnimatorController
{
    private const string IS_WALK = "_isWalk";
    private const string IS_RUN = "_isRun";
    private const string IS_CROUCH_DOWN = "_isCrouchDown";
    private const string IS_CROUCH_UP = "_isCrouchUp";
    private const string IS_CROUCH_IDLE = "_isCrouchIdle";
    private const string IS_CROUCH_WALK = "_isCrouchWalk";
    private const string IS_JUMP_UP = "_isJumpUp";
    
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
        
        if(IsControlsScriptCrouching)
        {
            _animator.SetBool(IS_CROUCH_DOWN, true);
            _animator.SetBool(IS_CROUCH_IDLE, true && !isWalking);
            _animator.SetBool(IS_CROUCH_WALK, isWalking);
        }
        
        _animator.SetBool(IS_CROUCH_UP, !IsControlsScriptCrouching);


        _animator.SetBool(IS_JUMP_UP, IsJump);
        
        _animator.SetBool(IS_WALK, isWalking);
        _animator.SetBool(IS_RUN, isRunning);



    }

}