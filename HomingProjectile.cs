using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
     public float upwardForce = 6f;
    public float moveSpeed = 5f;
    public float redirectDelay = 0.5f;
    public float lifeTime = 3f;
    public int damage = 1;
    private playerHealth playerHealth;

    private Rigidbody2D rb;
    private Vector2 targetPosition;
    private bool redirected = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<playerHealth>();
        //shoot upward
        rb.linearVelocity = Vector2.up * upwardForce;

        //destroy after certain time to prevent clutter
        Destroy(gameObject, lifeTime);

        Invoke(nameof(RedirectToTarget), redirectDelay);
    }

    public void Initialize(Vector2 lockedTargetPosition)
    {
        targetPosition = lockedTargetPosition;
    }

    void RedirectToTarget()
    {
        //redirect projectile towards target position
        if (redirected) return;

        redirected = true;
        //movement
        Vector2 direction = (targetPosition - rb.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
        
        //rotate triangle to face direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle - 180f; 
    }

    void OnTriggerEnter2D(Collider2D collision)
    { 
            if (collision.CompareTag("Player"))
            {
                playerHealth.TakeDamage(damage, transform);
                Destroy(gameObject);
            }
    }

      
}
