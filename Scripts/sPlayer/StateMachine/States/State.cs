using UnityEngine;

public abstract class State : MonoBehaviour
{
    public bool isComplete { get; protected set; }
    protected float startTime;
    public float time => Time.time - startTime;

    protected PlayerMotor playerMotor;
    protected CharacterAnimatorController animatorController;
    protected InputControlsScript inputControls;

    public virtual void Initialize(PlayerMotor motor, CharacterAnimatorController animator, InputControlsScript input)
    {
        playerMotor = motor;
        animatorController = animator;
        inputControls = input;
    }

    public virtual void Enter() 
    { 
        startTime = Time.time;
        isComplete = false;
    }
    
    public virtual void Do() { }
    public virtual void FixedDo() { }
    public virtual void Exit() { }
}