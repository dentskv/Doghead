using UnityEngine;

public class MineController : MonoBehaviour
{
    private Animator _mineAnimator;
    private PlayerHealth _playerHealth;
    private bool _playerIsHere;

    private void Start()
    {
        _mineAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerIsHere = true;
            _mineAnimator.SetTrigger("IsPlayer");
            _playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            Invoke("Explosion", 1.2f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            _playerIsHere = false;
        }
    }

    private void Explosion()
    {
        if(_playerIsHere)
        {
            _playerHealth.TakeDamage(1);
        }
        Destroy(gameObject);
    }
}
