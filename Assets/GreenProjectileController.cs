using UnityEngine;

public class GreenProjectileController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private bool _isDamaged;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_isDamaged)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);
                _isDamaged = true;
                Decay();
            }
        }

        if (other.gameObject.CompareTag("Ground"))
        {
            _rigidbody2D.simulated = false;
            _isDamaged = true;
            _animator.SetTrigger("IsFallen");
            Invoke("Decay", 0.27f);
        }
    }

    private void Decay()
    {
        Destroy(gameObject);
    }
}
