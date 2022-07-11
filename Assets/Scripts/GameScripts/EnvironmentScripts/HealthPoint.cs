using UnityEngine;

public class HealthPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (!collision.gameObject.GetComponent<PlayerHealth>().IsEnough)
            {
                collision.gameObject.GetComponent<PlayerHealth>().TakeHeal(1);
                Destroy(gameObject);
            }
        }
    }
}
