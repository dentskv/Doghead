using Controllers;
using UnityEngine;
using Zenject;

public class CoinCollect : MonoBehaviour
{
    [Inject] private CoinsController coinController;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            coinController.CollectCoins(1);
            Destroy(gameObject);
        }
    }
}
