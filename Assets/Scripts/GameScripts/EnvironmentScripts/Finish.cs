using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameplayManager gameplayManager;

    [Inject] private StarPreset star;
    
    private int _sceneIndex;
    private bool _isFinised;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (!_isFinised)
            {
                StartCoroutine(gameplayManager.MoveAchievement("Find the exit"));
                _isFinised = true;
                star.stars[SceneManager.GetActiveScene().buildIndex - 1].starsAmount++;
                Invoke("NextLevel", 3f);
            }
        }
    }

    private void NextLevel()
    {
        _sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(++_sceneIndex);
    }
}
