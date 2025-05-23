using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputControlsScript : MonoBehaviour
{
    [SerializeField] private Vector2 _move;
    [SerializeField] private Vector2 _look;
    [SerializeField] private bool _isRun;
    [SerializeField] private bool _isJump;
    [SerializeField] private bool _isCrouchedState = false;

    public UnityEvent UseEvent = new();
    public UnityEvent JumpEvent = new();
    public UnityEvent CrouchEvent = new();

    public Vector2 Move => _move;
    public Vector2 Look => _look;
    public bool IsRun => _isRun;
    public bool IsJump => _isJump;
    public bool IsCrouchedState => _isCrouchedState;

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
        if (useValue.isPressed)
            UseEvent?.Invoke();
    }

    private void OnRun(InputValue runValue)
    {
        _isRun = runValue.isPressed;
    }

    private void OnJump(InputValue jumpValue)
    {
        if (jumpValue.isPressed)
        {
            _isJump = true;
            JumpEvent?.Invoke();
        }
    }

    private void OnCrouch(InputValue crouchValue)
    {
        if (crouchValue.isPressed)
        {
            _isCrouchedState = !_isCrouchedState;
            CrouchEvent?.Invoke();
        }
    }

    public void JumpEnd()
    {
        _isJump = false;
    }
}
