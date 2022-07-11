using UnityEngine;
using UnityEngine.UI;

public class HeartsController : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    public void ChangeHearts()
    {
        for (int i = 0; i < hearts.Length;  i++)
        {
            if (i < playerHealth.PlayerHP) hearts[i].sprite = fullHeart;
            else hearts[i].sprite = emptyHeart;
            
            if(i < playerHealth.MaxPlayerHP) hearts[i].enabled = true;
            else hearts[i].enabled = false;
        }
    }
}
