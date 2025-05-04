using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] private float _speedWalk = 10;
    [SerializeField] private float _speedRun = 20;
    [SerializeField] private float _jumpForce = 5;
    [SerializeField] private float _mouseSensitivity = 150;
    [SerializeField] private float _characterGravityForce = -20f;
    [SerializeField] private bool _isGrounded = false;
    [SerializeField] private float _groundDistance = 0.4f;
    [SerializeField] private const float SPINE_MAX_TILT = 30;
    private const string IS_WALK = "_isWalk";
    private const string IS_RUN = "_isRun";
    private const string IS_CROUCH_DOWN = "_isCrouchDown";
    private const string IS_CROUCH_UP = "_isCrouchUp";
    private const string IS_CROUCH_IDLE = "_isCrouchIdle";
    private const string IS_CROUCH_WALK = "_isCrouchWalk";
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Transform _groundChecker;

    [SerializeField] private Transform _spineBone;
    [SerializeField] private Transform _chestBone;
    [SerializeField] private Transform _neckBone;
    [SerializeField] private Transform _headBone;

    [SerializeField] private float _standingHeight = 2.6f;
    [SerializeField] private float _crouchingHeight = 1.8f;
    [SerializeField] private float _crouchSpeed = 5f;
    

    private float _xRoatation = 0;
    private float _yRoatation = 0;
    Vector3 move;
    private Vector3 _playerGravity;

    private CharacterController _characterController;
    private Animator _animator;

    [SerializeField] private InputControlsScript _inputControlsScript;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();


        _inputControlsScript.JumpEvent.AddListener(OnJump);
        _inputControlsScript.UseEvent.AddListener(OnUse);
        _inputControlsScript.CrouchEvent.AddListener(OnCrouch);
    }

    private void Update()
    {
        _isGrounded = CheckIsGrounded();
        CharacterGravity();
        OnMove();
        OnCameraLook();
        UpdateMovementState();
    }

    private void LateUpdate()
    {
        UpdateBonesRotation();
    }

    private void OnCameraLook()
    {
        float mouseX = _inputControlsScript.Look.x * _mouseSensitivity * Time.deltaTime;
        float mouseY = _inputControlsScript.Look.y * _mouseSensitivity * Time.deltaTime;

        _xRoatation -= mouseY;
        _xRoatation = Mathf.Clamp(_xRoatation, -90f, 90f);

        _yRoatation += mouseX;

        transform.rotation = Quaternion.Euler(0, _yRoatation, 0);
    }

    private void UpdateBonesRotation()
    {
        float tilt = Mathf.Clamp(_xRoatation, -SPINE_MAX_TILT, SPINE_MAX_TILT);

        if (_spineBone != null)
            _spineBone.localRotation = Quaternion.Euler(0, 0, tilt * 0.3f);

        if (_chestBone != null)
            _chestBone.localRotation = Quaternion.Euler(0, 0, tilt * 0.5f);

        if (_neckBone != null)
            _neckBone.localRotation = Quaternion.Euler(0, 0, tilt * 0.7f);

        if (_headBone != null)
            _headBone.localRotation = Quaternion.Euler(0, 0, tilt);
    }

    private void OnMove()
    {
        Vector2 moveInput = _inputControlsScript.Move;
        move = transform.right * moveInput.x + transform.forward * moveInput.y;

        float currentSpeed = _inputControlsScript.IsRun ? _speedRun : _speedWalk;
        _characterController.Move(move * currentSpeed * Time.deltaTime);
    }

    private void OnJump()
    {
        if (_isGrounded)
        {
            _playerGravity.y = Mathf.Sqrt(_jumpForce * -2f * _characterGravityForce);
        }
    }

    private void OnCrouch()
    {
        float newHeight = _inputControlsScript.IsCrouchedState ? _crouchingHeight : _standingHeight;
        _characterController.height = newHeight;
        _characterController.center = new Vector3(0, newHeight / 2f, 0);
    }

    private void UpdateMovementState()
    {
        float moveAmount = new Vector2(move.x, move.z).magnitude;
        bool isWalking = moveAmount > 0.1f;
        bool isRunning = isWalking && _inputControlsScript.IsRun;
        bool isCrouched = _inputControlsScript.IsCrouchedState;

        _animator.SetBool(IS_WALK, isWalking && !isRunning && !isCrouched);
        _animator.SetBool(IS_RUN, isRunning && !isCrouched);
        
        _animator.SetBool(IS_CROUCH_IDLE, isCrouched && !isWalking);
        _animator.SetBool(IS_CROUCH_WALK, isCrouched && isWalking);

        if (isCrouched)
        {
            _animator.SetTrigger(IS_CROUCH_DOWN);
        }
        else
        {
            _animator.SetTrigger(IS_CROUCH_UP);
        }
    }

    private void OnUse()
    {
        Debug.Log("Use item");
    }
    

    private void CharacterGravity()
    {
        if (_isGrounded && _playerGravity.y < 0)
        {
            _playerGravity.y = -2f;
        }

        _playerGravity.y += _characterGravityForce * Time.deltaTime;
        _characterController.Move(_playerGravity * Time.deltaTime);
    }

    private bool CheckIsGrounded()
    {
        return Physics.CheckSphere(_groundChecker.position, _groundDistance, _groundMask);
    }



}
