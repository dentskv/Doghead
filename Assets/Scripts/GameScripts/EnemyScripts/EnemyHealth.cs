using System;
using System.Collections;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

public class EnemyHealth : MonoBehaviour
{
    [Inject] private DiContainer container;
    [Inject] private SoundPreset sound;

    [SerializeField] private GameObject enemyModelGameObject;
    [SerializeField] private GameObject coin;
    [SerializeField] private Slider healthBar;
    [SerializeField] private float enemyHP = 2;
    [SerializeField] private bool isShooting;

    public event Action EnemyIsDead;
    public event Action EnemyIsDamaged;
    
    private EnemyStateDamage _enemyStateDamage;
    private GameplayManager _gameplayManager;
    private SpriteRenderer _enemySpriteRenderer;
    private AudioSource _audioSource;
    private GameObject _coin;
    private Animator _enemyAnimator;
    private bool _isDead;

    private void Start()
    {
        _enemySpriteRenderer = enemyModelGameObject.GetComponent<SpriteRenderer>();
        _enemyStateDamage = GetComponent<EnemyStateDamage>();
        _gameplayManager = FindObjectOfType<GameplayManager>();
        _enemyAnimator = enemyModelGameObject.GetComponent<Animator>();
        _audioSource = FindObjectOfType<AudioSource>();
        EnemyIsDamaged += ChangeHealthBar;
        
        healthBar.maxValue = enemyHP;
        healthBar.minValue = 0;
        ChangeHealthBar();
    }

    private void ChangeHealthBar()
    {
        healthBar.value = enemyHP;
    }

    public void TakeDamage(float damage)
    {
        if (!_isDead)
        {
            _audioSource.PlayOneShot(sound.audioClips[7]);
            StartCoroutine(Blinking());
            enemyHP -= damage;
            EnemyIsDamaged?.Invoke();
            
            if (enemyHP <= 0)
            {
                _isDead = true;
                _audioSource.PlayOneShot(sound.audioClips[8]);
                _enemyStateDamage.enabled = false;

                try
                {
                    if (isShooting) gameObject.GetComponent<ShootingEnemyController>().enabled = false;
                    else gameObject.GetComponent<EnemyController>().enabled = false;
                }
                catch
                {
                    Debug.Log("Hasn't component");
                }

                _enemyAnimator.SetTrigger("IsDead");
                Invoke("EnemyDie", 1f);
            }
        }
    }

    private IEnumerator Blinking()
    {
        for (int i = 1; i < 4; i++)
        {
            _enemySpriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            _enemySpriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void EnemyDie()
    {
        _gameplayManager.CountEnemies();
        EnemyIsDead?.Invoke();
        DropCoin();
        Destroy(gameObject);
    }

    private void DropCoin()
    {
        float randomValueX, randomValueY;
        int randomAmount = Random.Range(5, 8);
        for (int i = 0; i <= randomAmount; i++)
        {
            randomValueX = Random.Range(-1.4f, 1.4f);
            randomValueY = Random.Range(-0.5f, 1.5f);
            _coin = Instantiate(coin, new Vector2(gameObject.transform.position.x + randomValueX, gameObject.transform.position.y + randomValueY), Quaternion.identity);
            Debug.Log(_coin.GetComponent<CoinCollect>());

            container.Inject(_coin.GetComponent<CoinCollect>());
        }
    }
}
