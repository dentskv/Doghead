using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform enemyModelTransform;
    [SerializeField] private float patrolDistance = 45f;
    [SerializeField] private float enemySpeed = 2f;

    private Rigidbody2D _enemyRigidbody;
    private Vector2 _rightBoundaryPosition;
    private Vector2 _leftBoundaryPosition;
    private Vector2 _nextPoint;
    private bool _isFacingRight = true;

    private void Start()
    {
        _enemyRigidbody = GetComponent<Rigidbody2D>();
        _leftBoundaryPosition = transform.position;
        _rightBoundaryPosition = _leftBoundaryPosition + Vector2.right * patrolDistance;
    }

    private void FixedUpdate()
    {
        {
            _nextPoint = Vector2.right * enemySpeed * Time.fixedDeltaTime;
            Patrol();
        }
    }

    private void Patrol()
    {

        if (!_isFacingRight)
        {
            _nextPoint.x *= -1;
        }

        _enemyRigidbody.MovePosition((Vector2)transform.position + _nextPoint);

        if (ShouldFlip())
        {
            Flip();
        }
    }

    private bool ShouldFlip()
    {
        bool isOutOfLeftBoundary = _isFacingRight && transform.position.x >= _rightBoundaryPosition.x;
        bool isOutOfRightBoundary = !_isFacingRight && transform.position.x <= _leftBoundaryPosition.x;
        return isOutOfLeftBoundary || isOutOfRightBoundary;
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector2 enemyScale = enemyModelTransform.localScale;
        enemyScale.x *= -1;
        enemyModelTransform.localScale = enemyScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_leftBoundaryPosition, _rightBoundaryPosition);
    }
}
