
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    private Vector2 _moveInput;
    private Rigidbody2D _myRigidbody2D;
    private Animator _myAnimator;
    private CapsuleCollider2D _myBodyCollider2D;
    private BoxCollider2D _myFeetCollider2D;
    private float _starterGravityScale;
    private bool _isAlive = true;
    
    [Header("Player Options")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float climbingSpeed;

    [FormerlySerializedAs("Gun")]
    [Header("Bullets Options")]
    [SerializeField] private List<Transform> bulletsTransform;

    [SerializeField] public Transform GunChildTransform;
    public GameObject guns;
    private SpriteRenderer gunSpriteRenderer;
    [SerializeField] private Animator gunAnimation;
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Bullets = Animator.StringToHash("bullets");
    private static readonly int Run1 = Animator.StringToHash("Run");
    private static readonly int Fall = Animator.StringToHash("Fall");
    private static readonly int Dying = Animator.StringToHash("Dying");
   

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        gunSpriteRenderer = guns.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        _myRigidbody2D = GetComponent<Rigidbody2D>();
        _myAnimator = GetComponent<Animator>();
        _myBodyCollider2D = GetComponent<CapsuleCollider2D>();
        _myFeetCollider2D = GetComponent<BoxCollider2D>();
        _starterGravityScale = _myRigidbody2D.gravityScale;
        gunSpriteRenderer = guns.GetComponent<SpriteRenderer>();
        
        
    }

    
    void Update()
    {
        if (!_isAlive) return;
        Run();
        FlipSprite();
        JumpFallAnimation();
        ClimbLader();
        Die();
        
    }

    void OnMove(InputValue value)
    {
        if(!_isAlive){return;}
        _moveInput = value.Get<Vector2>();
        Debug.Log(_moveInput);
        AudioManager.Instance.PlaySFX("PlayerWalk");
    }

    void OnFire(InputValue value)
    {
        if(!_isAlive){return;}
        
        foreach (Transform bulletTransform in bulletsTransform)
        {
            GameObject bullet = ObjectPool.Instance.GetPoolObject();
            if (bullet != null)
            {

              
                //bullet.transform.rotation = bulletTransform.rotation;
                bullet.transform.position = bulletTransform.position;
                bullet.SetActive(true);
                AudioManager.Instance.PlaySFX("Fire");
            }

           
        }
        gunAnimation.SetTrigger(Bullets);
        
    }

    void Run()
    {
        if(!_isAlive){return;}
        Vector2 playerVelocity;
        var velocity = _myRigidbody2D.velocity;
        playerVelocity = new Vector2(_moveInput.x * speed, velocity.y);
        velocity = playerVelocity;
        _myRigidbody2D.velocity = velocity;
        bool playerHaseHorizontalSpeed = Mathf.Abs(velocity.x) > Mathf.Epsilon;
        _myAnimator.SetBool(Run1,playerHaseHorizontalSpeed);
        
    }

    void OnJump(InputValue value)
    {
        if(!_isAlive){return;}
        
        if(!_myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))){return;}
        
        if (value.isPressed)
        {
            _myRigidbody2D.velocity += new Vector2(0f, jumpSpeed);
            AudioManager.Instance.PlaySFX("Jump");
            
        }
    }

    void ClimbLader()
    {
        if(!_isAlive){return;}
        if (!_myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            _myRigidbody2D.gravityScale = _starterGravityScale;
            return;
        }

        Vector2 playerClimbCelocity = new Vector2(_myRigidbody2D.velocity.x, _moveInput.y * climbingSpeed);
        _myRigidbody2D.velocity = playerClimbCelocity;
        _myRigidbody2D.gravityScale = 0f;
        bool playerHasVerticalSpeed;
        playerHasVerticalSpeed = Mathf.Abs(_myRigidbody2D.velocity.y) > Mathf.Epsilon;
        _myAnimator.SetBool(Run1,playerHasVerticalSpeed);
        

    }

    void JumpFallAnimation()
    {
        _myAnimator.SetBool(Jump, !GroundCheck());
        if (_myRigidbody2D.velocity.y > 0)
        {
            _myAnimator.SetBool(Fall, false);
        }
        else if(!GroundCheck())
        {
            _myAnimator.SetBool(Fall,true);
          
        }else if (GroundCheck())
        {
            _myAnimator.SetBool(Fall,false);
        }
       

    }

    private bool GroundCheck()
    {
        if (_myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground","Climbing")))
        {
            return true;
        }else
        {
            return  false;
        }
    }

    void FlipSprite()
        {
            bool playerHaseHorizontalSpeed = Mathf.Abs(_myRigidbody2D.velocity.x) > Mathf.Epsilon;

            if (playerHaseHorizontalSpeed)
            {
                transform.localScale = new Vector2(Mathf.Sign(_myRigidbody2D.velocity.x), 1f);
            }

        }

    void Die()
    {
        if(_myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy","Hazard")))
        {
            _isAlive = false;
            _myAnimator.SetTrigger(Dying);
            _myRigidbody2D.bodyType=RigidbodyType2D.Static;
            _myBodyCollider2D.isTrigger = true;
            guns.SetActive(false);
            GameSessions.Instance.DeathSession();
            AudioManager.Instance.PlaySFX("Dead");
            //AudioManager.Instance.musicSource.Stop();
            
            
        }
    }
    
    // Method to change the player's equipped gun sprite
    public void ChangeGunSprite(Sprite newGunSprite)
    {
        if (gunSpriteRenderer != null && newGunSprite != null)
        {
            // Change the sprite of the gun
            gunSpriteRenderer.sprite = newGunSprite;
        }
        else
        {
            Debug.LogWarning("Gun sprite renderer or new sprite is null.");
        }
    }

  
    
}
