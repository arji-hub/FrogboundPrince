using UnityEngine;

public class MobMovement : MonoBehaviour
{
    [Header("Patrol Settings")]
    public float speed;
    public float patrolDistance = 5f; 
    private Vector2 startPos;
    public bool moveRightUp = true;
    public bool horizontal = true;
    private bool startDirection;
    private Vector2 pointA;
    private Vector2 pointB;

    private SpriteRenderer sr;

    [Header("Chase Settings")]
    public Transform player;
    public float chaseRange;
    public bool isChasing;
    public bool canChase;
    public float knockbackTimer = 0f;
    

    audioManager audioManager;
    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<audioManager>();
    }
     void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sr = GetComponent<SpriteRenderer>();
        setDistance(patrolDistance);
    }
    void Update()
    {
         if (knockbackTimer > 0f)
        {
            knockbackTimer -= Time.fixedDeltaTime;
        }
        else{
            isChasing = Vector2.Distance(transform.position, player.position) < chaseRange;
            if (isChasing && canChase)
            {
            Chase();
            }
            else
            {
                Patrol();
            }
        }
      
    }
    private void Chase()
    {
        //determine direction to player 1 for right, -1 for left
        float direction = Mathf.Sign(player.position.x - transform.position.x);
        //move towards player
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
        //flip sprite to face player
        if (direction > 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }

    public void stopChase()
    {
        canChase = false;
        isChasing = false;
    }   

    private void Patrol()
    {
        // values based on horizontal/vertical mode
        Vector2 rightup = horizontal ? Vector2.right : Vector2.up;
        Vector2 leftdown = horizontal ? Vector2.left : Vector2.down;
        float currentPos = horizontal ? transform.position.x : transform.position.y;
        float a = horizontal ? pointA.x : pointA.y;
        float b = horizontal ? pointB.x : pointB.y;

        float min = Mathf.Min(a, b);
        float max = Mathf.Max(a, b);

        //ikot kapag patrol bounds reached
        if (currentPos >= max) moveRightUp = false;
        else if (currentPos <= min) moveRightUp = true;

        Vector2 moveDirection = moveRightUp ? rightup : leftdown;

        if (horizontal) sr.flipX = moveRightUp;

        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    private void setDistance(float distance)
    {
        startDirection = moveRightUp;
        startPos = transform.position;

        if (startDirection)
        {
            // startPos is left/bottom edge
            pointA = startPos;
            pointB = horizontal?
                new Vector2(startPos.x + distance, startPos.y)
                : new Vector2(startPos.x, startPos.y + distance);
            moveRightUp = true;
        }
        else
        {
            // startPos is right/top edge
            pointA = horizontal? 
                new Vector2(startPos.x - distance, startPos.y)
                : new Vector2(startPos.x, startPos.y - distance);
            pointB = startPos;
            moveRightUp = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        moveRightUp = moveRightUp? false : true;

    }

    private void OnDrawGizmos()
    {
        Vector2 start = Application.isPlaying ? (Vector2)startPos : transform.position;
        bool movePositive = Application.isPlaying ? startDirection : moveRightUp;
        

        if (horizontal)
        {
            Vector2 startPoint = start;
            Vector2 endPoint   = movePositive? start + Vector2.right * patrolDistance
                                       : start + Vector2.left  * patrolDistance;

            // Patrol path line
            Gizmos.color = Color.red;
            Gizmos.DrawLine(startPoint, endPoint);
            Gizmos.DrawSphere(startPoint,  0.15f);
            Gizmos.DrawSphere(endPoint, 0.15f);

        }
        else
        {
            Vector2 downPoint = start;
            Vector2 upPoint   = movePositive? start + Vector2.up * patrolDistance
                                       : start + Vector2.down * patrolDistance;

            // Patrol path line
            Gizmos.color = Color.green;
            Gizmos.DrawLine(downPoint, upPoint);
            Gizmos.DrawSphere(downPoint, 0.15f);
            Gizmos.DrawSphere(upPoint,   0.15f);

        }

        if(canChase)
        {
            //range circle
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseRange);
        }
    }
    

}
