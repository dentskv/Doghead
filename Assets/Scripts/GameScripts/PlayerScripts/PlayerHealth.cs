using System.Collections;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private HeartsController heartsController;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject gun;
    [SerializeField] private int maxPlayerHP = 3;

    [Inject] private SoundPreset sound;
    
    private SpriteRenderer _playerSpriteRenderer;
    private PlayerMovement _playerMovement;
    private Animator _playerAnimator;
    private int _playerHP;

    public bool IsEnough
    {
        get => _playerHP == maxPlayerHP;
    }

    public int MaxPlayerHP
    {
        get => maxPlayerHP;
    }

    public int PlayerHP
    {
        get => _playerHP;
    }

    private void Start()
    {
        _playerSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _playerAnimator = GetComponentInChildren<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerHP = maxPlayerHP;
        heartsController.ChangeHearts();
    }

    public void TakeDamage(int damage)
    {
        audioSource.PlayOneShot(sound.audioClips[1]);
        _playerHP -= damage;
        heartsController.ChangeHearts();
        StartCoroutine(Blinking());

        if (_playerHP <= 0)
        {
            _playerMovement.enabled = false;
            gun.SetActive(false);
            _playerAnimator.SetTrigger("isDead");
            Invoke("PlayerDie", 1f);
        }
    }

    public void TakeHeal(int hp)
    {
        if(_playerHP < maxPlayerHP)
        {
            audioSource.PlayOneShot(sound.audioClips[9]);
            _playerHP += hp;
            heartsController.ChangeHearts();
        }
    }

    private IEnumerator Blinking()
    {
        for (int i = 1; i < 4; i++)
        {
            _playerSpriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.15f);
            _playerSpriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.15f);
        }
    }

    private void PlayerDie()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
