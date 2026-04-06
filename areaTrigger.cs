using UnityEngine;

public class areaTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                MobMovement mob = enemy.GetComponent<MobMovement>();
                if (mob != null && mob.canChase)
                {
                    mob.chaseRange = 40f;
                    mob.speed = 15f;
                }
            }
        }
    }
}