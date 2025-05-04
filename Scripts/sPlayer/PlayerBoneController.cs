using UnityEngine;

public class PlayerBoneController : MonoBehaviour
{
    private Transform _stomachBone;
    private Transform _chestBone;
    private Transform _neckBone;
    private Transform _headBone;

    private const float SPINE_MAX_TILT = 30;

    public PlayerBoneController(Transform spineBone, Transform chestBone, Transform neckBone, Transform headBone)
    {
        _stomachBone = spineBone;
        _chestBone = chestBone;
        _neckBone = neckBone;
        _headBone = headBone;
    }

    public void UpdateBonesRotation(float xRoatation)
    {
        float tilt = Mathf.Clamp(xRoatation, -SPINE_MAX_TILT, SPINE_MAX_TILT);

        if (_stomachBone != null)
            _stomachBone.localRotation = Quaternion.Euler(0, 0, tilt * 0.3f);

        if (_chestBone != null)
            _chestBone.localRotation = Quaternion.Euler(0, 0, tilt * 0.5f);

        if (_neckBone != null)
            _neckBone.localRotation = Quaternion.Euler(0, 0, tilt * 0.7f);

        if (_headBone != null)
            _headBone.localRotation = Quaternion.Euler(0, 0, tilt);
    }
}
