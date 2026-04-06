using UnityEngine;

public class MobDamage : MonoBehaviour
{
    public bool beetle;
    public int damage;
    private Animator animator;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag =="Player" && !beetle)
        {
            playerHealth playerHealth = collision.gameObject.GetComponent<playerHealth>();

            playerHealth.TakeDamage(damage, transform);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag =="Player")
        {   
            if(beetle)animator.SetTrigger("attack");
            
            playerHealth playerHealth = collision.gameObject.GetComponent<playerHealth>();

            playerHealth.TakeDamage(damage, transform);
        }
    }
    void Start()
    {
        if(beetle)
        {
            animator = GetComponentInParent<Animator>(); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
