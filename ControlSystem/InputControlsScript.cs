using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputControlsScript : MonoBehaviour
{
    [SerializeField] private Vector2 _move;
    [SerializeField] private Vector2 _look;
    [SerializeField] private bool _isRun;

    public UnityEvent UseEvent = new();
    public UnityEvent JumpEvent = new();
    
    public Vector2 Move => _move;
    public Vector2 Look => _look;
    public bool IsRun => _isRun;

    private void OnMove(InputValue moveValue)
    {
        _move = moveValue.Get<Vector2>();
    }
    private void OnCameraLook(InputValue lookValue)
    {
        _look = lookValue.Get<Vector2>();
    }
    private void OnUse(InputValue useValue)
    {
        if (useValue.isPressed) { UseEvent?.Invoke(); }
    }

    private void OnRun(InputValue runValue)
    {
        _isRun = runValue.isPressed;
    }
    private void OnJump(InputValue jumpValue)
    {
        if(jumpValue.isPressed) { JumpEvent?.Invoke(); }
    }


}
