using ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameplayManager gameplayManager;
    [SerializeField] private AudioSource waterAudioSource;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Transform playerModelTransform;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Image reloadingBar;
    [SerializeField] private float movespeed = 3f;
    
    [Inject] private SoundPreset sound;
    [Inject] private StarPreset star;
    
    private Rigidbody2D _rbPlayer;
    private Vector2 _moveVector;
    private float _jumpingForce = 32000f;
    private float _timeToPortal = 1f;
    private bool _isFacingRight = true;
    private bool _isRepulse;
    private bool _isGround;
    private bool _isJumped;
    private bool _inPortal;
    private bool _inDark;
    
    private int _movespeedMultiplier = 100;
    
    //gun part
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Animator shootingAnimation;
    
    private GameObject bullet;
    private float _bulletSpeed = 4f;
    private float _reloadingTime;
    private float _timeToWait;
    private bool _isReloaded = true;
    //gun part
    
    void Start()
    {
        _rbPlayer = GetComponent<Rigidbody2D>();
        UpdateAchievements();
        SetGunStats();
        _timeToWait = _reloadingTime;
        waterAudioSource.clip = sound.audioClips[11];
    }

    private void Update()
    {
        ReloadGun();
        if (!_isJumped)
        {
            _isGround = true;
        }
    }

    private void ReloadGun()
    {
        if (!_isReloaded)
        {
            reloadingBar.fillAmount -= 1 / _timeToWait * Time.deltaTime * 2;
            if (reloadingBar.fillAmount <= 0)
            {
                reloadingBar.fillAmount = 1f;
                _isReloaded = true;
            }
        }
    }
    
    private void FixedUpdate()
    {
        playerAnimator.SetFloat("velocityY", _rbPlayer.velocity.y);

        _rbPlayer.velocity = new Vector2(_moveVector.x * movespeed * _movespeedMultiplier * Time.fixedDeltaTime, _rbPlayer.velocity.y);
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector2 playerScale = playerModelTransform.localScale;
        playerScale.x *= -1;
        playerModelTransform.localScale = playerScale;
    }

    private void UpdateAchievements()
    {
        for (int i = 0; i < star.stars.Length; i++)
        {
            if (star.stars[i].idLvl == SceneManager.GetActiveScene().buildIndex - 1)
            {
                star.stars[i].starsAmount = 0;
            }
        }
    }
    
    private void SetGunStats()
    {
        _bulletSpeed *= PlayerPrefs.GetFloat("bulletSpeed");
        _reloadingTime = PlayerPrefs.GetFloat("reload");
    }
    
    public void OnMove(InputValue input)
    {
        Debug.Log("OnMove: " + input);
        _moveVector = input.Get<Vector2>();
        
        playerAnimator.SetFloat("playerSpeed", Mathf.Abs(_moveVector.x));

        if (_moveVector.x > 0f && !_isFacingRight)
        {
            Flip();
        }
        else if (_moveVector.x < 0f && _isFacingRight)
        {
            Flip();
        }
    }

    public void OnJump()
    {
        _isJumped = true;
        if (_isGround)
        {
            if(_isRepulse) audioSource.PlayOneShot(sound.audioClips[15]);
            else audioSource.PlayOneShot(sound.audioClips[0]);
            _rbPlayer.AddForce(new Vector2(0f, _jumpingForce));
            playerAnimator.SetBool("isJumped", _isJumped);
            _isGround = false;
            playerAnimator.SetBool("isGrounded", _isGround);
        }
    }

    public void OnFire()
    {
        if (_isReloaded)
        {
            shootingAnimation.SetTrigger("Shoot");
            audioSource.PlayOneShot(sound.audioClips[2]);
            bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            _isReloaded = false;
            
            if (_isFacingRight)
            {
                bullet.GetComponent<ProjectileController>().SetSide(Vector2.right);
                bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.right * _bulletSpeed, ForceMode2D.Impulse);
            }
            else
            {
                bullet.GetComponent<ProjectileController>().SetSide(Vector2.left);
                bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.left * _bulletSpeed, ForceMode2D.Impulse);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Repulsive"))
        {
            _isGround = true;
            _isJumped = false;
            playerAnimator.SetBool("isGrounded", _isGround);
            playerAnimator.SetBool("isJumped", _isJumped);
        }
        
        if (collision.gameObject.CompareTag("Platform")) 
        {
            gameObject.transform.parent = collision.transform;
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            audioSource.PlayOneShot(sound.audioClips[6]);
        }

        if (collision.gameObject.CompareTag("Repulsive"))
        {
            _jumpingForce += 6000f;
            _isRepulse = true;
        }

        if (collision.gameObject.CompareTag("Coin"))
        {
            audioSource.PlayOneShot(sound.audioClips[5]);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Repulsive"))
        {
            _isGround = false;
            playerAnimator.SetBool("isGrounded", _isGround);
        }

        if (collision.gameObject.CompareTag("Repulsive"))
        {
            _jumpingForce -= 6000f;
            _isRepulse = false;
        }
        
        if (collision.gameObject.CompareTag("Platform"))
        {
            gameObject.transform.parent = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            movespeed -= 1f;
            waterAudioSource.Play();
        }
        
        if (collision.gameObject.CompareTag("Mine"))
        {
            Invoke("Explosion", 0.9f);
        }
        
        if (collision.gameObject.CompareTag("Hidden"))
        {
            collision.gameObject.GetComponent<Tilemap>().color = new Color(255, 255, 255, 0);
        }
    }

    private void Explosion()
    {
        audioSource.PlayOneShot(sound.audioClips[14]);
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            movespeed += 1f;
            waterAudioSource.Stop();
        }
        
        if (collision.gameObject.CompareTag("Hidden"))
        {
            collision.gameObject.GetComponent<Tilemap>().color = new Color(255, 255, 255, 255);
        }
    }
}

