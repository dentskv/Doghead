using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private float platformDistance = 10f;
    [SerializeField] private bool horizontal;

    private Transform _platformTransform;
    private Vector2 _firstBoundaryPosition;
    private Vector2 _secondBoundaryPosition;
    private Vector2 _nextPoint;
    private float _platformSpeed = 2f;
    private bool _isFacingForward = true;

    private void Start()
    {
        _platformTransform = gameObject.GetComponent<Transform>();
        _secondBoundaryPosition = transform.position;
        if (horizontal)
        {
            _firstBoundaryPosition = _secondBoundaryPosition + Vector2.right * platformDistance;
        }
        else
        {
            _firstBoundaryPosition = _secondBoundaryPosition + Vector2.up * platformDistance;
        }
    }

    private void FixedUpdate()
    {
        if(horizontal)
        {
            _nextPoint = transform.right * _platformSpeed * Time.fixedDeltaTime;
        }
        else
        {
            _nextPoint = transform.up * _platformSpeed * Time.fixedDeltaTime;
        }

        Patrol();
    }

    private void Patrol()
    {   
        if(horizontal)
        {
            if (!_isFacingForward)
            {
                _nextPoint.x *= -1;
            }
        }
        else
        {
            if (!_isFacingForward)
            {
                _nextPoint.y *= -1;
            }
        }

        _platformTransform.Translate((Vector2)_nextPoint);
        
        if(horizontal)
        {
            if (ShouldFlipHorizontal())
            {
                Flip();
            }
        }
        else
        {
            if(ShouldFlipVertical())
            {
                Flip();
            }
        }
    }

    private bool ShouldFlipVertical()
    {
        bool isOutOfUpperBoundary = _isFacingForward && transform.position.y >= _firstBoundaryPosition.y;
        bool isOutOfBottomBoundary = !_isFacingForward && transform.position.y <= _secondBoundaryPosition.y;
        return isOutOfUpperBoundary || isOutOfBottomBoundary;
    }

    private bool ShouldFlipHorizontal()
    {
        bool isOutOfLeftBoundary = _isFacingForward && transform.position.x >= _firstBoundaryPosition.x;
        bool isOutOfRightBoundary = !_isFacingForward && transform.position.x <= _secondBoundaryPosition.x;
        return isOutOfLeftBoundary || isOutOfRightBoundary;
    }


    private void Flip()
    {
        _isFacingForward = !_isFacingForward;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_secondBoundaryPosition, _firstBoundaryPosition);
    }
}
