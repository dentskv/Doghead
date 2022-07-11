using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform playerModelTransform;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private float movespeed = 3f;

    private Rigidbody2D _rbPlayer;
    private GameObject _playerGameObject;
    private float _horizontalMovement = 0f;
    private bool _isFacingRight = true;
    private bool _isGround = false;
    private bool _isJumped = false;
    private int _movespeedMultiplier = 100;

    public bool IsFacingRight
    {
        get => _isFacingRight;
    }

    public bool IsJumped
    {
        get => _isJumped;
    }

    public bool IsGrounded
    {
        get => _isGround;
    }

    public float HorizontalMovement
    {
        get => _horizontalMovement;
    }

    void Start()
    {
        _rbPlayer = GetComponent<Rigidbody2D>();
        _playerGameObject = gameObject;
    }
    
    void Update()
    {
        _horizontalMovement = Input.GetAxis("Horizontal");
        playerAnimator.SetFloat("playerSpeed", Mathf.Abs(_horizontalMovement));

        if (_isJumped)
        {
            _rbPlayer.AddForce(new Vector2(0f, 400f));
            _isGround = false;
            _isJumped = false;
        }

        if (_horizontalMovement > 0f && !_isFacingRight)
        {
            Flip();
        }
        else if (_horizontalMovement < 0f && _isFacingRight)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        _rbPlayer.velocity = new Vector2(_horizontalMovement * movespeed * _movespeedMultiplier * Time.fixedDeltaTime, _rbPlayer.velocity.y);
        playerAnimator.SetFloat("velocityY", _rbPlayer.velocity.y);

        if(_isJumped)
        {
            _rbPlayer.AddForce(new Vector2(0f, 1800f));
            playerAnimator.SetBool("isJumped", _isJumped);
            _isGround = false;
            playerAnimator.SetBool("isGrounded", _isGround);
            _isJumped = false;
        }

        if (_horizontalMovement > 0f && !_isFacingRight)
        {
            Flip();
        }

        if (_horizontalMovement < 0f && _isFacingRight)
        {
            Flip();
        }

    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector2 playerScale = playerModelTransform.localScale;
        playerScale.x *= -1;
        playerModelTransform.localScale = playerScale;
    }

    public void OnJump()
    {
        if(_isGround)
        {
            _isJumped = true;
            playerAnimator.SetBool("isJumped", _isJumped);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            _isGround = true;
            playerAnimator.SetBool("isGrounded", _isGround);
            playerAnimator.SetBool("isJumped", _isJumped);
        }

        if (collision.gameObject.CompareTag("Platform"))
        {
            gameObject.transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            _isGround = false;
        }
        if (collision.gameObject.CompareTag("Platform"))
        {
            gameObject.transform.parent = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Water"))
        {
            movespeed -= 0.8f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            movespeed += 0.8f;
        }
    }
}
