using UnityEngine;

public class gameOver : MonoBehaviour
{

    public void Retry()
    {
        GameManager.instance.Retry();
    }

    public void Quit()
    {
        GameManager.instance.Quit();
    }

}
