using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OnewayPlatform : MonoBehaviour
{
    public GameObject currentOnewayPlatform;
    [SerializeField] private BoxCollider2D playerCollider;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            moveDown();
        }
    }

    public void moveDown()
    {
        if (currentOnewayPlatform != null)
        {
            StartCoroutine(DisableCollision());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {//if the player collides with a one-way platform
       if(collision.gameObject.CompareTag("OnewayPlatform"))
        {//if the collided object is tagged as "OnewayPlatform"
            currentOnewayPlatform = collision.gameObject;
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {//if the player exits collision with a one-way platform
        if(collision.gameObject.CompareTag("OnewayPlatform"))
        {//clear the reference
           currentOnewayPlatform = null;
        }
    }

    private IEnumerator DisableCollision()
    {//disable collision between player and one-way platform for a short duration
        TilemapCollider2D platformCollider = currentOnewayPlatform.GetComponent<TilemapCollider2D>();
        CompositeCollider2D compositeCollider = currentOnewayPlatform.GetComponent<CompositeCollider2D>();
        Physics2D.IgnoreCollision(playerCollider, platformCollider, true);
        Physics2D.IgnoreCollision(playerCollider, compositeCollider, true);
        yield return new WaitForSeconds(0.2f);//waiting time before re-enabling collision
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
        Physics2D.IgnoreCollision(playerCollider, compositeCollider, false);

    }

    
}
