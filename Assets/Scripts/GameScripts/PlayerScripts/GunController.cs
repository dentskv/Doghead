using UnityEngine;

[RequireComponent(typeof(Animator))]

public class GunController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Animator shootingAnimation;

    private GameObject _bullet;
    private float _bulletSpeed = 8f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        shootingAnimation.SetTrigger("Shoot");
        _bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        if (playerController.IsFacingRight)
        {
            _bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.right * _bulletSpeed, ForceMode2D.Impulse);
        }
        else
        {
            _bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.left * _bulletSpeed, ForceMode2D.Impulse);
        }
    }
}
