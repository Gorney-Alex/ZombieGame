using UnityEngine;

public class CharacterGravity
{
    private Vector3 _gravityVelocity;
    private float _gravityForce;
    private CharacterController _characterController;

    public CharacterGravity(float gravityForce, CharacterController characterController)
    {
        _gravityForce = gravityForce;
        _characterController = characterController;
    }

    public void CharacterGravityUpdate(bool isGrounded)
    {
        if (isGrounded && _gravityVelocity.y < 0)
        {
            _gravityVelocity.y = -2f;
        }

        _gravityVelocity.y += _gravityForce * Time.deltaTime;
        _characterController.Move(_gravityVelocity * Time.deltaTime);
    }

    public void SetYVelocity(float yVelocity)
    {
        _gravityVelocity.y = yVelocity;
    }

    public float GetYVelocity()
    {
        return _gravityVelocity.y;
    }
}
