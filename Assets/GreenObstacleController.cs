using UnityEngine;

public class GreenObstacleController : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float waitingTime = 3f;
    
    private Animator _animator;
    private float _timeToWait;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _timeToWait = waitingTime;
    }

    private void Update()
    {
        _timeToWait -= Time.deltaTime;
        if (_timeToWait <= 0)
        {
            _animator.SetTrigger("Drop");
            Invoke("GenerateProjectile", 0.2f);
            _animator.SetTrigger("Dropped");
            _timeToWait = waitingTime;
        }
    }

    private void GenerateProjectile()
    {
        Instantiate(projectile, new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z), gameObject.transform.rotation);
    }
}
