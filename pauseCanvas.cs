using UnityEngine;

public class pauseCanvas : MonoBehaviour
{
    public GameObject player;
    private playerMovement playerMovement;
    private playerAttack playerAttack;
     void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<playerMovement>();
        playerAttack = player.GetComponent<playerAttack>();
    }
    public void resume()
    {
        GameManager.instance.Resume();
        playerMovement.enabled = true;
        playerAttack.enabled = true;
    }

    public void quit()
    {
        Time.timeScale = 1f;
        GameManager.instance.Quit();
    }

    

}
