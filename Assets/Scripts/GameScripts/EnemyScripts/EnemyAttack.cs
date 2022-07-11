using System;
using ScriptableObjects;
using UnityEngine;
using Zenject;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Animator enemyAnimator;
    [SerializeField] private float timeToWaitAnimation = 0.3f;
    [SerializeField] private float timeToDamage;
    [SerializeField] private float bulletSpeed = 8f;

    private ShootingEnemyController _shootingEnemyController;
    private SoundController _soundController;
    private AudioSource _audioSource;
    private GameObject _bullet;
    private float _damageTime;
    private bool _isDamaged;

    private void Awake()
    {
        _soundController = GetComponent<SoundController>();
    }

    private void Start()
    {
        _audioSource = FindObjectOfType<AudioSource>();
        _shootingEnemyController = GetComponent<ShootingEnemyController>();
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
    }

    public void LateFire()
    {
        if (_isDamaged)
        {
            enemyAnimator.SetBool("IsFire", _isDamaged);
            enemyAnimator.SetTrigger("Shoot");
            Invoke("Fire", timeToWaitAnimation);
        }
    }

    public void Fire()
    {
        if (_isDamaged)
        {
            _audioSource.PlayOneShot(_soundController.audioClips[4]);
            _bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            if (_shootingEnemyController.IsFacingRight)
            {
                _bullet.GetComponent<EnemyProjectileController>().SetSide(Vector2.right);
                _bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.right * bulletSpeed, ForceMode2D.Impulse);
                _bullet.transform.localScale = new Vector2(-1 * _bullet.transform.localScale.x, _bullet.transform.localScale.y);
            }
            else
            {
                _bullet.GetComponent<EnemyProjectileController>().SetSide(Vector2.left);
                _bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.left * bulletSpeed, ForceMode2D.Impulse);
            }
            _isDamaged = false;
            enemyAnimator.SetBool("IsFire", _isDamaged);
        }
    }
}
