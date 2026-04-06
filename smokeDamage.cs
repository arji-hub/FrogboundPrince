using System.Collections;
using UnityEngine;

public class smokeDamage : MonoBehaviour
{
    public int damageAmount = 1;
    public float damageInterval = 0.5f;
    public string playerTag = "Player";

    private bool playerInside = false;
    private GameObject playerRef = null;
    private Coroutine damageCoroutine;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag)) return;
        playerInside = true;
        playerRef = other.gameObject;
        damageCoroutine = StartCoroutine(DamageRoutine());
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag)) return;
        playerInside = false;
        playerRef = null;
        if (damageCoroutine != null)
            StopCoroutine(damageCoroutine);
    }

    private IEnumerator DamageRoutine()
    {
        while (playerInside && playerRef != null)
        {
            playerHealth health = playerRef.GetComponent<playerHealth>();
            if (health != null)
                health.TakeDamage(damageAmount, transform);

            yield return new WaitForSeconds(damageInterval);
        }
    }
}