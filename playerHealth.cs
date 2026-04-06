using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class playerHealth : MonoBehaviour
{
    [SerializeField] private CameraShake cameraShake;
    public Rigidbody2D rb;
    [SerializeField] Animator animator;
    [SerializeField] private GameObject deathPrefab;
    [Header("Health")]
    public int maxHealth = 6;
    public int currentHealth;
    public Image[] hearts; //3 hearts lahat
    //state of each heart
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    [Header("Knockback")]
    public float horizontalForce = 10f;
    public float verticalForce = 4f;
    public float knockbackDuration = 0.2f; 
    
    private playerMovement movement;

    audioManager audioManager;

    void Awake()
    {
        movement = GetComponent<playerMovement>();
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<audioManager>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        UpdateHearts();
    }
    public void UpdateHearts()
    {
        int health = currentHealth; //6hearts total
        for (int i = 0; i < hearts.Length; i++)
        {
           if(health >= 2)
            {
                hearts[i].sprite = fullHeart;
                health -= 2;
            }
            else if (health == 1)
            {
                hearts[i].sprite = halfHeart;
                health -= 1;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    public void TakeDamage(int damage, Transform enemy)
    {
        audioManager.PlaySFX(audioManager.faah);
        
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHearts();

        animator.SetTrigger("hit");
        animator.SetInteger("health", currentHealth);

        rb.linearVelocity = Vector2.zero;

         float direction = Mathf.Sign(transform.position.x - enemy.position.x);

        Vector2 knockback = new Vector2(direction * horizontalForce, verticalForce);
        rb.AddForce(knockback, ForceMode2D.Impulse);
        movement.knockbackTimer = knockbackDuration;
        
        if (currentHealth <= 0)
        {
            
            GetComponent<Collider2D>().enabled = false;
            StartCoroutine(HandleDeathAfterHit());
        }
    }

    IEnumerator HandleDeathAfterHit()
    {
        yield return new WaitForSeconds(0.2f); // match hit animation length

        Die();
    }
    

    public void Die()
    {
        //play death sound
        audioManager.PlaySFX(audioManager.faah);

        StartCoroutine(cameraShake.Shake(0.3f, 0.2f));

        // spawn death clone
        GameObject clone = Instantiate(deathPrefab, transform.position, transform.rotation);

        //velocity sa main copy to clone
        Rigidbody2D cloneRb = clone.GetComponent<Rigidbody2D>();
        Vector2 velocity = rb.linearVelocity;
       
        velocity.y += 5f;//knockback sa clone
        cloneRb.linearVelocity = rb.linearVelocity * 0.5f;

        // hide player
        GetComponent<SpriteRenderer>().enabled = false;
        rb.simulated = false;

        //disable movement and attack scripts
        playerMovement movement = GetComponent<playerMovement>();
        movement.enabled = false;
        playerAttack attack = GetComponent<playerAttack>();
        attack.enabled = false;
        StartCoroutine(ShowGameOverUI());
    }

    public IEnumerator ShowGameOverUI()
    {
        yield return new WaitForSeconds(1.0f);

        GameManager.instance.GameOver();
    }
}
