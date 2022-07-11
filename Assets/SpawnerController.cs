using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class SpawnerController : MonoBehaviour
{
    [Inject] private DiContainer _container;

    [SerializeField] private GameplayManager gameplayManager;
    [SerializeField] private int amountOfEnemies = 3;

    private GameObject[] _enemiesArray;
    private Animator _animator;
    private bool _isEnemyExist;

    private void Awake()
    {
        gameplayManager.IncreaseAmount(amountOfEnemies);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _enemiesArray = Resources.LoadAll<GameObject>("Enemies");
    }

    private void Update()
    {
        if (amountOfEnemies > 0 && !_isEnemyExist)
        {
            InitializeEnemy();
        }

        if (amountOfEnemies <= 0)
        {
            OnDisable();
        }
    }

    private void InitializeEnemy()
    {
        _isEnemyExist = true;
        _animator.SetTrigger("Spawn");
        Invoke("CreateEnemy", 1.3f);
    }
    
    private void CreateEnemy()
    {
        var enemy = Instantiate(_enemiesArray[Random.Range(0, _enemiesArray.Length)], transform.position, transform.rotation);
        _container.Inject(enemy.GetComponent<EnemyHealth>());
        enemy.GetComponent<EnemyHealth>().EnemyIsDead += EnemyNotExist;
        _animator.SetTrigger("EndSpawn");
        amountOfEnemies--;
    }

    private void EnemyNotExist()
    {
        _isEnemyExist = false;
    }

    private void OnDisable()
    {
        GetComponent<SpawnerController>().enabled = false;
    }
}
