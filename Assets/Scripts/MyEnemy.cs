using UnityEngine;
using UnityEngine.AI;


public class MyEnemy : MonoBehaviour
{
    #region Fields

    [SerializeField] private KilledBossesCounter _bossesCounter;
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _playerDeathDistance = 1.5f;
    [SerializeField] private float _visionLength = 10.0f;
    [SerializeField] private int _health;
    [SerializeField] private bool _isBoss = false;

    private NavMeshAgent _sanitarAgent;
    private Animator _animator;
    private Transform _playerTransform;
    private Vector3 _startRaycastPosition;
    private Vector3 _directionToPlayer;
    private Color _color;
    private float _idleTime = 6.0f;
    private float _startOffset = 1.0f;
    private int _currentPatrolPoint;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _playerTransform = FindObjectOfType<LemonController>().transform;
        _animator = GetComponent<Animator>();
        _sanitarAgent = GetComponent<NavMeshAgent>();
        _sanitarAgent.SetDestination(_patrolPoints[0].position);
        if (_isBoss)
        {
            _animator.speed = 0.75f;
        }
    }

    private void Update()
    {
        AI();
    }

    #endregion


    #region Methods

    public void Hurt(int dmg)
    {
        _health -= dmg;
        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (_isBoss)
        {
            if (_bossesCounter)
            {
                _bossesCounter.BossKilled();
            }
        }
        Destroy(gameObject);
    }

    private void CalculatePath()
    {
        if (_sanitarAgent.remainingDistance <= _sanitarAgent.stoppingDistance)
        {
            _currentPatrolPoint = (++_currentPatrolPoint) % _patrolPoints.Length;
            _sanitarAgent.SetDestination(_patrolPoints[_currentPatrolPoint].position);
        }
    }

    private void AI()
    {
        _color = Color.red;
        RaycastHit hit;
        _startRaycastPosition = CalculateOffset(transform.position);
        _directionToPlayer = CalculateOffset(_playerTransform.position) - _startRaycastPosition;
        _animator.SetFloat("Speed", _sanitarAgent.velocity.magnitude);

        var raycast = Physics.Raycast(_startRaycastPosition, _directionToPlayer, out hit, _directionToPlayer.magnitude, _layerMask);

        if (raycast)
        {
            if (hit.collider.gameObject.CompareTag("Player") && _directionToPlayer.magnitude < _visionLength)
            {
                _color = Color.green;
                _sanitarAgent.SetDestination(hit.point);
                if (_directionToPlayer.magnitude <= _playerDeathDistance)
                {
                    hit.collider.GetComponent<LemonController>().Die();
                }
            }
            else if (!IsInvoking() && _sanitarAgent.remainingDistance < _sanitarAgent.stoppingDistance)
            {
                Invoke(nameof(CalculatePath), _idleTime);
            }
        }
        Debug.DrawRay(_startRaycastPosition, _directionToPlayer, _color);
    }

    private Vector3 CalculateOffset(Vector3 position)
    {
        position.y += _startOffset;
        return position;
    }

    #endregion
}
