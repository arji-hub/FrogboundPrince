using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class playerAttack : MonoBehaviour
{
    public Transform throwPoint;
    public GameObject rockPrefab;
    public Animator animator;
    public int count=5;
    public Image displayCount;
    public Sprite one, two, three, four, five,zero;
    audioManager audioManager;
    AudioClip attack;
    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<audioManager>();
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        displayCount=GameObject.FindGameObjectWithTag("count").GetComponent<Image>();
        updateCount();
    }
    void Update()
    {
        if(Input.GetButtonDown("Throw"))
        {
            Throw();
        }
    }

    public void Throw()
    {
        if(count<0)count = 0;
        
        if(count<=0)
        {
            return;
        }
        //random attack sound
        int rand = Random.Range(0, 2);
        if(rand == 0)
        {
            attack = audioManager.croak1;
        }
        else
        {
            attack = audioManager.croak2;
        }
        audioManager.PlaySFX(attack);
        animator.SetTrigger("shoot");
        Instantiate(rockPrefab, throwPoint.position, throwPoint.rotation);
        
        count--;
        updateCount();
    }

    public void updateCount()
    {                               
        int bulletCount = count;   
        switch (bulletCount)
        {
            case 5:
                displayCount.sprite = five;
                break;
            case 4:
                displayCount.sprite = four;
                break;
            case 3:
                displayCount.sprite = three;
                break;
            case 2:
                displayCount.sprite = two;
                break;
            case 1:
                displayCount.sprite = one;
                break;
            default:
                displayCount.sprite = zero;
                break;
        }
    
    }
}
