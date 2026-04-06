using UnityEngine;

public class facingDirection : MonoBehaviour
{

    [Header("References")]
    public SpriteRenderer sr;
    public Transform colliderChild; 

    [Header("Collider Settings")]
    public float rotationAngle = 45f;   

    void Start()
    {
        if (sr == null)
            sr = GetComponentInParent<SpriteRenderer>();
    }

    void Update()
    {
        FlipTowardsTarget();
    }

    void FlipTowardsTarget()
    {
        if (sr.flipX)
        {

            colliderChild.localRotation = Quaternion.Euler(0, 0, -rotationAngle);
        }
        else
        {
            colliderChild.localRotation = Quaternion.Euler(0, 0, rotationAngle);
        }
    }
}