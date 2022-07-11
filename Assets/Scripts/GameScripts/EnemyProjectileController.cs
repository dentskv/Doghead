using UnityEngine;

public class EnemyProjectileController : MonoBehaviour
{
    [SerializeField] private float _timeToDestroy = 1.2f;
    
    private Vector2 _facingSide;
    
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
        if(_timeToDestroy <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
