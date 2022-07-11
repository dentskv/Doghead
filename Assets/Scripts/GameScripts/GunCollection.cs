using UnityEngine;

public class GunCollection : MonoBehaviour
{
    [SerializeField] private Sprite[] gunSprites;
    [SerializeField] private GameObject[] bullets;

    private SpriteRenderer _spriteRenderer;
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = gunSprites[PlayerPrefs.GetInt("gunID")];
    }
}
