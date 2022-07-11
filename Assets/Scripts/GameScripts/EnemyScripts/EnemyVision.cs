using System;
using UnityEngine;
using Zenject.SpaceFighter;

public class EnemyVision : MonoBehaviour
{
    [SerializeField] private float circleRadius;
    [SerializeField] private float maxDistance;

    public event Action OnHit;
    
    private ShootingEnemyController _shootingEnemyController;
    private RaycastHit2D _hit;
    private EnemyAttack _enemyAttack;
    private GameObject _currentHitObject;
    private LayerMask layerMask;
    private Vector2 _enemyPosition;
    private Vector2 _direction;
    private float _currentHitDistance;
    private bool _needToWait;

    public bool IsWait
    {
        get => _needToWait;
    }

    private void Start()
    {
        layerMask = LayerMask.GetMask("Player");
        _currentHitObject = GameObject.FindWithTag("Player");
        _shootingEnemyController = GetComponent<ShootingEnemyController>();
        OnHit += gameObject.GetComponent<EnemyAttack>().LateFire;
        _enemyPosition = transform.position;
    }

    private void Update()
    {
        _hit = Physics2D.CircleCast(_enemyPosition, circleRadius, _direction, maxDistance, layerMask);

        if (_hit)
        {
            _currentHitObject = _hit.transform.gameObject;
            _currentHitDistance = _hit.distance;
            if (_currentHitObject.CompareTag("Player"))
            {
                _needToWait = true;
                OnHit?.Invoke();
            }
        }
        else
        {
            _needToWait = false;
            _currentHitObject = null;
            _currentHitDistance = maxDistance;
        }
    }

    private void FixedUpdate()
    {
        _enemyPosition = transform.position;

        if (_shootingEnemyController.IsFacingRight)
        {
            _direction = Vector2.right;
        }
        else 
        {
            _direction = Vector2.left;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_enemyPosition, _enemyPosition + _direction * _currentHitDistance);
        Gizmos.DrawWireSphere(_enemyPosition + _direction * _currentHitDistance, circleRadius);
    }
}
