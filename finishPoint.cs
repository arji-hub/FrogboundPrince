using UnityEngine;

public class finishPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                MobMovement mob = enemy.GetComponent<MobMovement>();
                if (mob != null) mob.stopChase();
            }
            GameManager.instance.nextScene();

        }

    }
    
}
