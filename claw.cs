using UnityEngine;

public class claw : MonoBehaviour
{
     [Header("References")]
    public SpriteRenderer sr;
    public Transform colliderChild; 

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
            //change transform position to 2,0,0
            colliderChild.localPosition = new Vector3(2, 0, 0);
        }
        else
        {
            colliderChild.localPosition = new Vector3(0, 0, 0);
        }
    }
}
