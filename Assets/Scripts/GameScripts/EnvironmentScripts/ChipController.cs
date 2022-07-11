using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class ChipController : MonoBehaviour
{
    [SerializeField] private GameplayManager gameplayManager;

    [Inject] private StarPreset star;
    
    private bool isEnabled = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (isEnabled)
            {
                StartCoroutine(gameplayManager.MoveAchievement("Find a chip"));
                star.stars[SceneManager.GetActiveScene().buildIndex - 1].starsAmount++;
            }
            isEnabled = false;
            gameObject.GetComponent<ChipController>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
