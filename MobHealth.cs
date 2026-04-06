using UnityEngine;

//nde na nagamit e2 ksi 1hit nlng mob
public class MobHealth : MonoBehaviour
{
    public Rigidbody2D rb;
    public int maxHealth = 100;
    public int currentHealth;   
      [Header("Knockback")]
    public float horizontalForce = 10f;
    public float verticalForce = 4f;
    public float knockbackDuration = 0.2f; 
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }
     private MobMovement movement;

    void Awake()
    {
        movement = GetComponent<MobMovement>();
    }
    public void TakeDamage(int damage, Transform enemy)
    {
        currentHealth -= damage;
       

        rb.linearVelocity = Vector2.zero;

         float direction = Mathf.Sign(transform.position.x - enemy.position.x);

        Vector2 knockback = new Vector2(direction * horizontalForce, verticalForce);
        rb.AddForce(knockback, ForceMode2D.Impulse);
        movement.knockbackTimer = knockbackDuration;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
