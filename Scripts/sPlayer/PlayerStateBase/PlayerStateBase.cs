using UnityEngine;

public abstract class PlayerStateBase : IState
{
    protected PlayerMotor playerMotor;
    protected CharacterAnimatorController characterAnimatorController;
    protected InputControlsScript inputControlsScript;

    public bool isComplete { get; protected set; }

    protected float startTime;

    public float time => Time.time - startTime;

    public PlayerStateBase(PlayerMotor playerMotor, CharacterAnimatorController characterAnimatorController, InputControlsScript inputControlsScript)
    {
        this.playerMotor = playerMotor;
        this.characterAnimatorController = characterAnimatorController;
        this.inputControlsScript = inputControlsScript;
    }

    public abstract void Enter();
    public abstract void Do();
    public abstract void FixedDo();
    public abstract void Exit();
}
