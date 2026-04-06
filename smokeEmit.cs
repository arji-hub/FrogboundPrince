using System.Collections;
using UnityEngine;

public class smokeEmit : MonoBehaviour
{
    [Header("Smoke Settings")]
    public GameObject smokeCloudPrefab;
    public Transform emitPoint;

    [Header("Interval Settings")]
    public float emitInterval = 5f;
    public float smokeLifetime = 3f;

    [Header("Damage Settings")]
    public float damageInterval = 0.5f;
    public int damageAmount = 1;
    public string playerTag = "Player";

    Animator animator;

    [Header("Facing")]
    public bool defaultFacingRight = true;

    private void Start()
    {
        if (emitPoint == null)
            emitPoint = transform;

        StartCoroutine(EmitRoutine());
        animator = GetComponent<Animator>();
    }

    private IEnumerator EmitRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(emitInterval);
            EmitSmoke();
            animator.SetTrigger("attack");
        }
    }

    private void EmitSmoke()
    {
        if (smokeCloudPrefab == null) return;

        GameObject smoke = Instantiate(smokeCloudPrefab, emitPoint.position, Quaternion.identity);

        bool facingRight = defaultFacingRight
            ? transform.localScale.x > 0
            : transform.localScale.x < 0;

        smokeDamage dmg = smoke.GetComponent<smokeDamage>();
        if (dmg != null)
        {
            dmg.damageAmount = damageAmount;
            dmg.damageInterval = damageInterval;
            dmg.playerTag = playerTag;
        }

        smokeDirection dir = smoke.GetComponent<smokeDirection>();
        if (dir != null)
            dir.SetDirection(facingRight);

        Destroy(smoke, smokeLifetime);
    }

   

    private void OnDrawGizmosSelected()
    {
        if (emitPoint == null) return;
        Gizmos.color = new Color(0.2f, 0.8f, 0.2f, 0.4f);
        Gizmos.DrawSphere(emitPoint.position, 0.2f);
    }
}