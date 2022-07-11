using UnityEngine;

public class EnemyStateDamage : MonoBehaviour
{
    private float _timeToDamage = 2f;
    private bool _isDamaged;
    private int _damage = 1;

    private void Update()
    {
        if (!_isDamaged)
        {
            _timeToDamage -= Time.deltaTime;
            if (_timeToDamage <= 0f)
            {
                _isDamaged = true;
                _timeToDamage = 2f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

        if (playerHealth != null && _isDamaged)
        {
            playerHealth.TakeDamage(_damage);
            _isDamaged = false;
        }
    }
}
