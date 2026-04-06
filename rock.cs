using UnityEngine;

public class rock : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 5;
    public Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log("Hit: "+hitInfo.tag);
        if(hitInfo.CompareTag("Enemy"))
        {
            GameObject enemyObj = hitInfo.gameObject;
            Destroy(enemyObj);
        }
        Destroy(gameObject);
    }
   
}
