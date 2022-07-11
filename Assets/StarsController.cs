using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class StarsController : MonoBehaviour
{
    [SerializeField] private Image[] starImages;
    [SerializeField] private int _buttonID = 0;

    [Inject] private StarPreset star;

    private Color _alphaStar;

    private void Start()
    {
        UpdateStars();
    }

    private void UpdateStars()
    {
        _alphaStar = new Color(255, 255, 255, 1f);
        for (int i = 0; i < star.stars.Length; i++)
        {
            if (star.stars[i].starsAmount != 0 && star.stars[i].idLvl == _buttonID)
            {
                for (int j = 0; j < star.stars[i].starsAmount; j++)
                {
                    starImages[j].color = _alphaStar;
                }
            }
        }
    }
}
