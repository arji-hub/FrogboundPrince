using System.Collections;
using UnityEngine;

public class HomingAttack : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float detectionRange = 8f;
    public float fireInterval = 2f;
    [SerializeField] private Animator animator;
    private Transform player;
    private bool isShooting;
    audioManager audioManager;
    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<audioManager>();
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        //check distance to player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange && !isShooting)
        {
            StartCoroutine(ShootHomingProjectiles());
        }
    }

    IEnumerator ShootHomingProjectiles()
    {
        isShooting = true;

        //pag nasa loob ng range, shoot homing projectiles 
        while (Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            fire();
            yield return new WaitForSeconds(fireInterval);
        }

        isShooting = false;
    }

    void fire()
    {
        audioManager.PlaySFX(audioManager.plantAttack);
        animator.SetTrigger("shoot");
    }

    public void Shoot()
    {
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, projectilePrefab.transform.rotation);

        Vector2 lockedPosition = player.position; 

        proj.GetComponent<HomingProjectile>().Initialize(lockedPosition);
        
    }

    void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, detectionRange);
        }
}
