using ScriptableObjects;
using UnityEngine;
using Zenject;

public class ChickenController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Sprite bulletSprite;
    [SerializeField] private float timeToDamage;
    [SerializeField] private float circleRadius;
    [SerializeField] private float maxDistance;
    [SerializeField] private float bulletSpeed = 8f;

    [Inject] private SoundPreset sound;
    
    private RaycastHit2D _hit;
    private GameObject _currentHitObject;
    private GameObject _bullet;
    private Transform _firePointTransform;
    private Animator _animator;
    private Vector2 _enemyPosition;
    private Vector2 _direction;
    private float _currentHitDistance;
    private float _damageTime;
    private bool _needToWait;
    private bool _isDamaged;
    private bool _wasShooting;

    private void Start()
    {
        _currentHitObject = GameObject.FindWithTag("Player");
        _animator = GetComponentInChildren<Animator>();
        _firePointTransform = GetComponentInChildren<Transform>();
        _direction = Vector2.left;
        _damageTime = timeToDamage;
        _enemyPosition = new Vector2(transform.position.x, transform.position.y - 1);
    }

    private void Update()
    {
        if(!_isDamaged)
        {
            _damageTime -= Time.deltaTime;
            if (_damageTime <= 0f)
            {
                _isDamaged = true;
                _damageTime = timeToDamage;
            }
        }
        
        _hit = Physics2D.CircleCast(_enemyPosition, circleRadius, _direction, maxDistance, layerMask);

        if (_hit)
        {
            _currentHitObject = _hit.transform.gameObject;
            _currentHitDistance = _hit.distance;
            _animator.SetTrigger("Opening");
            Invoke(nameof(LateFire), 0.6f);
            
        }
        else
        {
            _currentHitObject = null;
            _currentHitDistance = maxDistance;
            _animator.SetBool("IsFire", false);
            if (_wasShooting)
            {
                _animator.SetTrigger("Closing");
            }
            _wasShooting = false;
            Invoke(nameof(LateClose), 0.4f);
        }
    }

    private void LateClose()
    {
        _animator.SetTrigger("Closed");
    }
    
    private void LateFire()
    {
        if (_isDamaged)
        {
            _animator.SetTrigger("Opening");
            Fire();
        }
    }

    private void Fire()
    {
        _animator.SetBool("IsFire", true);

        if (!_isDamaged) return;
        audioSource.PlayOneShot(sound.audioClips[4]);
        _bullet = Instantiate(bulletPrefab, new Vector2 (_enemyPosition.x, _enemyPosition.y), _firePointTransform.rotation);
        _bullet.GetComponent<EnemyProjectileController>().SetSide(Vector2.left);
        _bullet.GetComponent<SpriteRenderer>().sprite = bulletSprite;
        _bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.left * bulletSpeed, ForceMode2D.Impulse);
        _wasShooting = true;
        _isDamaged = false;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_enemyPosition, _enemyPosition + _direction * _currentHitDistance);
        Gizmos.DrawWireSphere(_enemyPosition + _direction * _currentHitDistance, circleRadius);
    }
}
