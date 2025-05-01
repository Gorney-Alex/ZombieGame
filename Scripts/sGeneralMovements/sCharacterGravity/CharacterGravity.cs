using UnityEngine;

public class CharacterGravity
{
    private float _gravityForce;

    public CharacterGravity(float gravityForce)
    {
        _gravityForce = gravityForce;
    }

    public void CharacterGravityUpdate(ref Vector3 gravityVelocity, bool isGrounded, CharacterController controller)
    {
        if (isGrounded && gravityVelocity.y < 0)
        {
            gravityVelocity.y = -2f;
        }

        gravityVelocity.y += _gravityForce * Time.deltaTime;
        controller.Move(gravityVelocity * Time.deltaTime);
    }
}
