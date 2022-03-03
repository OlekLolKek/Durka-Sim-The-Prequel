using UnityEngine;


public class Wheelchair : MonoBehaviour
{
    #region Fields

    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _rotateSpeed;

    private Rigidbody _rigidbody;
    private Transform _playerTransform;
    private Vector3 _startRaycastPosition;
    private Vector3 _directionToPlayer;
    private Color _color;
    private float _startOffset = 1.0f;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _playerTransform = FindObjectOfType<LemonController>().transform;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _color = Color.red;
        RaycastHit hit;
        _startRaycastPosition = CalculateOffset(transform.position);
        _directionToPlayer = CalculateOffset(_playerTransform.position) - _startRaycastPosition;

        var raycast = Physics.Raycast(_startRaycastPosition, _directionToPlayer, out hit, _directionToPlayer.magnitude, _layerMask);

        if (raycast)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                _color = Color.green;
                Vector3 movement = Vector3.RotateTowards(transform.forward, _directionToPlayer.normalized, _rotateSpeed * Time.deltaTime, 0f);
                _rigidbody.MoveRotation(Quaternion.LookRotation(movement));
            }
        }
 
        Debug.DrawRay(_startRaycastPosition, _directionToPlayer, _color);
    }

    #endregion


    #region Methods

    private Vector3 CalculateOffset(Vector3 position)
    {
        position.y += _startOffset;
        return position;
    }

    #endregion
}
