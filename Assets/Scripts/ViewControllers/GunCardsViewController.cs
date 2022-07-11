using Controllers;
using UnityEngine;
using UnityEngine.UI;

public class GunCardsViewController : MonoBehaviour
{
    [SerializeField] private Image[] damagePoints;
    [SerializeField] private Image[] ratePoints;
    [SerializeField] private Sprite yellowPoint;
    [SerializeField] private Sprite whitePoint;

    private GunCardsController _gunCardsController;
    private Color _alphaColor = new Color(255, 255, 255, 1f);
    
    private void Start()
    {
        _gunCardsController = GetComponent<GunCardsController>();
    }

    public void ChangeDamagePoints(int gunLvl)
    {
        if (gunLvl == 0)
        {
            ChangeColorAndSprite(damagePoints[0], whitePoint);
            ChangeColorAndSprite(damagePoints[1], yellowPoint);
        }
        else
        {
            for (int i = 0; i <= gunLvl; i++)
            {
                if (i == gunLvl)
                {
                    ChangeColorAndSprite(damagePoints[i], yellowPoint);
                }
                else ChangeColorAndSprite(damagePoints[i], whitePoint);
            }
        }
    }
    
    public void ChangeRatePoints(int gunLvl)
    {
        if (gunLvl == 0)
        {
            ChangeColorAndSprite(ratePoints[0], whitePoint);
            ChangeColorAndSprite(ratePoints[1], yellowPoint);
        }
        else
        {
            for (int i = 0; i <= gunLvl; i++)
            {
                if (i == gunLvl)
                {
                    ChangeColorAndSprite(ratePoints[i], yellowPoint);
                }
                else ChangeColorAndSprite(ratePoints[i], whitePoint);
            }
        }
    }

    private void ChangeColorAndSprite(Image image, Sprite sprite)
    {
        image.color = _alphaColor;
        image.sprite = sprite;
    }
}