using UnityEngine;

public class CheckCharacterGrounded
{
    private LayerMask _groundMask;
    private const float GROUND_DISTANCE = 0.4f;

    private Transform _groundChecker;

    public CheckCharacterGrounded(Transform groundChecker, LayerMask groundMask)
    {
        _groundChecker = groundChecker;
        _groundMask = groundMask;
    }

    public bool CheckIsGrounded()
    {
        return Physics.CheckSphere(_groundChecker.position, GROUND_DISTANCE, _groundMask);
    }



}
