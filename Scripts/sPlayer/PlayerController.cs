using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController _characterController;
    private float _gravityForce = -20f;
    private Vector3 _gravityVelocity;
    private bool _isGrounded;

    CharacterGravity _characterGravity;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _characterGravity = new CharacterGravity(_gravityForce);
    }

    private void Update()
    {
        _characterGravity.CharacterGravityUpdate(ref _gravityVelocity, _isGrounded, _characterController);
    }
}
