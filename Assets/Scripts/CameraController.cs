using UnityEngine;


public class CameraController : MonoBehaviour
{
    #region Fields

    [SerializeField] private Transform _johnTransform;
    [SerializeField] private float _height = 7.5f;
    [SerializeField] private float _offsetZ = -3.5f;
    [SerializeField] private float _lerpTime = 0.250f;

    #endregion


    #region UnityMethods

    private void FixedUpdate()
    {
        if (_johnTransform == null) return;

        Vector3 desiredPos = new Vector3(_johnTransform.position.x, _height, _johnTransform.position.z + _offsetZ);
        Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, _lerpTime);
        transform.position = smoothPos;
    }

    #endregion
}
