using UnityEngine;
using System.Collections;

public class playerManager : MonoBehaviour
{
    [SerializeField] playerMovement playerMovement;
    [SerializeField] playerAttack playerAttack;
    void Start()
    {
        playerMovement = GetComponent<playerMovement>();
        playerAttack = GetComponent<playerAttack>();
    }
    void Update()
    {
      if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if  (!GameManager.instance.isPaused)
            {
                GameManager.instance.Pause();
                Debug.Log("pm - Game Paused");
            }
            else
            {
                GameManager.instance.Resume();
                Debug.Log("pm - Game Resumed");
            } 

        }
        if (Time.timeScale == 0f){
                playerMovement.enabled = false;
                playerAttack.enabled = false;
            }
            else {
                playerMovement.enabled = true;
                playerAttack.enabled = true;
            }
    }

    
}
