using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;


public class playerMovement:MonoBehaviour
{
    private Rigidbody2D playerRb;
    
    private SpriteRenderer spriteRenderer;
    
    [SerializeField] float speed;
    [SerializeField] public float input;
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;
    [SerializeField] Animator animator;
    public bool isGrounded;
    public bool isMobile;
    private bool jumpPressed;
    private bool jumpReleased;
    private bool jumpDown;
    private bool jumpUp;
    audioManager audioManager;
    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<audioManager>();
    }

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        checkFacingDirection();
        jump();
        jumpPressed = false;
        jumpReleased = false;
        animator.SetFloat("yVelocity", playerRb.linearVelocity.y);
    }

    public float knockbackTimer = 0f; 

    void FixedUpdate()
    {
               if (knockbackTimer > 0f)
        {
            //during knockback, do NOT accept horizontal input
            knockbackTimer -= Time.fixedDeltaTime;
        }
        else
        {
            float move = input * speed;

            playerRb.linearVelocity = new Vector2(move, playerRb.linearVelocity.y);
            animator.SetFloat("speed", Mathf.Abs(input));
        }
        
    }


    void checkFacingDirection()
    {
        if(!isMobile)
        {
            input=Input.GetAxisRaw("Horizontal");
        }


        if(input>0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if(input<0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    [SerializeField] float jumpForce;
    public void jump()
    {
        
        jumpDown = jumpPressed ||
        Input.GetButtonDown("Jump") ||
        Input.GetKeyDown(KeyCode.W) ||
        Input.GetKeyDown(KeyCode.UpArrow);

        jumpUp = jumpReleased ||
        Input.GetButtonUp("Jump") ||
        Input.GetKeyUp(KeyCode.W) ||
        Input.GetKeyUp(KeyCode.UpArrow);

        if(isGrounded)
            {
                coyoteTimeCounter=coyoteTime;
            }
            else
            {
                coyoteTimeCounter = Mathf.Max(coyoteTimeCounter - Time.deltaTime, 0f);
               
            }

        if (jumpPressed)
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter = Mathf.Max(jumpBufferCounter - Time.deltaTime, 0f);
           
        }
        
        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f)
        {
            audioManager.PlaySFX(audioManager.croak2);
            //jump code
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, 0f);
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpBufferCounter = 0f;
        }
      
        if(jumpReleased && playerRb.linearVelocity.y > 0f)
        {
            playerRb.linearVelocity = 
            new Vector2(
                playerRb.linearVelocity.x, 
                playerRb.linearVelocity.y * 0.5f);
            
            coyoteTimeCounter=0f;
        }

    }

    public void OnJumpPressed()
    {
        jumpPressed = true;
    }

    public void OnJumpReleased()
    {
        jumpReleased = true;
    }

    [SerializeField] LayerMask groundMask;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("OnewayPlatform"))
        {
            isGrounded = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("OnewayPlatform"))
        {
            animator.SetTrigger("jump");
            isGrounded = false;
        }
    }


    public void moveX(float x)
    {
        input = x;
    }
    
}