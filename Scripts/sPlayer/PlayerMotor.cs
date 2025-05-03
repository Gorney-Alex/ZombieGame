using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerMotor : MonoBehaviour
{
    [Header("Components")]
    private CharacterGravity _characterGravity;
    private CharacterAnimatorController _characterAnimatorController;
    private CheckCharacterGrounded _checkCharacterGrounded;
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

    private Transform _groundChecker;
    [SerializeField] private LayerMask _groundMask;
    


    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
        var groundCheckerMarker = GetComponentInChildren<GroundCheckerComponent>();
        _groundChecker = groundCheckerMarker.transform;
        _checkCharacterGrounded = new CheckCharacterGrounded(_groundChecker, _groundMask);

        CharacterController characterController = GetComponent<CharacterController>();
        _characterGravity = new CharacterGravity(_characterGravityForce, characterController);

        Animator animator = GetComponent<Animator>();
        _characterAnimatorController = new CharacterAnimatorController(animator);


        _inputControlsScript.JumpEvent.AddListener(OnJump);
        _inputControlsScript.UseEvent.AddListener(OnUse);
        _inputControlsScript.CrouchEvent.AddListener(OnCrouch);
    }

    private void Update()
    {
        OnCameraLook();
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
    private void OnUse()
    {
        Debug.Log("Use item");
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


}
