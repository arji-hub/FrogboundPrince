using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform; //sprite 
    public SpriteRenderer playerSprite;  //facing direction

    public Vector3 offset;

    [Header("Smoothing")]
    public float smoothSpeed = 10f;

    [Header("Look Ahead")]
    public float lookAheadDistance = 2f;
    public float lookAheadSmooth = 5f;

    private float currentLookAheadX;
    private float fixedY;  

    void Start()
    {
        fixedY = transform.position.y;
    } 


    void LateUpdate()
    {
        float facingDirection = playerSprite.flipX ? -1f : 1f;

        float targetLookAheadX = facingDirection * lookAheadDistance;

        currentLookAheadX = Mathf.Lerp(
            currentLookAheadX,
            targetLookAheadX,
            lookAheadSmooth * Time.deltaTime
        );

        Vector3 desiredPosition = new Vector3(
            playerTransform.position.x +offset.x + currentLookAheadX,
            fixedY +offset.y,
            transform.position.z);  

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed
        );
    }
}
