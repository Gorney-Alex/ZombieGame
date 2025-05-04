using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [Header("Components")]
    private CharacterGravity _characterGravity;
    private CharacterAnimatorController _characterAnimatorController;
    private CheckCharacterGrounded _checkCharacterGrounded;
    private PlayerBoneController _playerBoneController;
    [SerializeField] private InputControlsScript _inputControlsScript;

    private float _characterGravityForce = -20f;

    [Header("PlayerMotor settings")]
    [SerializeField] private float _standingHeight = 2.6f;
    [SerializeField] private float _crouchingHeight = 1.8f;

    [SerializeField] private float _speedWalk = 10;
    [SerializeField] private float _speedRun = 20;
    [SerializeField] private float _crouchSpeed = 5f;
    [SerializeField] private float _jumpForce = 5;

    [SerializeField] private float _mouseSensitivity = 150;

    
    private float _xRotation = 0;
    private float _yRotation = 0;
    private Vector3 _move;

    [SerializeField] private LayerMask _groundMask;

    CharacterController _characterController;


    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
        var groundCheckerMarker = GetComponentInChildren<GroundCheckerComponent>();
        Transform groundChecker = groundCheckerMarker.transform;
        _checkCharacterGrounded = new CheckCharacterGrounded(groundChecker, _groundMask);

        _characterController = GetComponent<CharacterController>();
        _characterGravity = new CharacterGravity(_characterGravityForce, _characterController);

        Animator animator = GetComponent<Animator>();
        _characterAnimatorController = new CharacterAnimatorController(animator);

        var spineBone = GetComponentInChildren<StomachBoneMarker>()?.transform;
        var chestBone = GetComponentInChildren<ChestBoneMarker>()?.transform;
        var neckBone = GetComponentInChildren<NeckBoneMarker>()?.transform;
        var headBone = GetComponentInChildren<HeadBoneMarker>()?.transform;

        _playerBoneController = new PlayerBoneController(spineBone, chestBone, neckBone, headBone);


        _inputControlsScript.JumpEvent.AddListener(OnJump);
        _inputControlsScript.UseEvent.AddListener(OnUse);
        _inputControlsScript.CrouchEvent.AddListener(OnCrouch);
    }

    private void Update()
    {
        OnCameraLook();
        OnMove();
        _characterAnimatorController.UpdateMovementState(_move, _inputControlsScript.IsRun, _inputControlsScript.IsCrouchedState);
        _characterGravity.CharacterGravityUpdate(_checkCharacterGrounded.CheckIsGrounded());
    }

    private void LateUpdate()
    {
        _playerBoneController.UpdateBonesRotation(_xRotation);
    }

    private void OnCameraLook()
    {
        float mouseX = _inputControlsScript.Look.x * _mouseSensitivity * Time.deltaTime;
        float mouseY = _inputControlsScript.Look.y * _mouseSensitivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        _yRotation += mouseX;

        transform.rotation = Quaternion.Euler(0, _yRotation, 0);
    }

    private void OnMove()
    {
        Vector2 moveInput = _inputControlsScript.Move;
        _move = transform.right * moveInput.x + transform.forward * moveInput.y;

        float currentSpeed = _inputControlsScript.IsRun ? _speedRun : _speedWalk;
        _characterController.Move(_move * currentSpeed * Time.deltaTime);
    }
    private void OnUse()
    {
        Debug.Log("Use item");
    }

    private void OnJump()
    {
        bool IsGrounded = _checkCharacterGrounded.CheckIsGrounded();
        if (IsGrounded)
        {
            _characterGravity.SetYVelocity(Mathf.Sqrt(_jumpForce * -2f * _characterGravityForce));
        }
    }

    private void OnCrouch()
    {
        float newHeight = _inputControlsScript.IsCrouchedState ? _crouchingHeight : _standingHeight;
        _characterController.height = newHeight;
        _characterController.center = new Vector3(0, newHeight / 2f, 0);
    }


}
