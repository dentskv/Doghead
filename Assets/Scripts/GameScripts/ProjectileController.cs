using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class ProjectileController : MonoBehaviour
{
    private List<Object> _bulletAnimations;
    private Animator _animator;
    private Vector2 _facingSide;
    private float _timeToDestroy;
    private float _maxTimeToDestroy;

    private void Awake()
    {
        _bulletAnimations = Resources.LoadAll("ProjectileAnimations").ToList();
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.runtimeAnimatorController = _bulletAnimations[PlayerPrefs.GetInt("gunID")] as RuntimeAnimatorController;
        _maxTimeToDestroy = 1f;
        _timeToDestroy = _maxTimeToDestroy;
    }

    public void SetSide(Vector2 side)
    {
        _facingSide = side;
    }

    public Vector2 GetSide
    {
        get => _facingSide;
    }

    private void Update()
    {
        _timeToDestroy -= Time.deltaTime;
        if (_timeToDestroy <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(PlayerPrefs.GetFloat("damage"));
            Destroy(gameObject);
        }
    }
}
