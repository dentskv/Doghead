using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class InGameStarsController : MonoBehaviour
{
    [SerializeField] private Sprite fullStar;
    [SerializeField] private Image[] stars;

    [Inject] private StarPreset starPreset;
    
    private void OnEnable()
    {
        UpdateStars();
    }

    private void UpdateStars()
    {
        for (int i = 0; i < starPreset.stars[SceneManager.GetActiveScene().buildIndex - 1].starsAmount; i++)
        {
            stars[i].sprite = fullStar;
        }
    }
}
