using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject pauseCanvas;
    public GameObject tempCanvas;
    public GameObject gameOverCanvas;
    public bool isPaused=false;

    public Animator transition;

    public audioManager audioManager;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Pause()
    {
        audioManager.PlaySFX(audioManager.croak2);
        audioManager.musicSource.Pause();
        tempCanvas = Instantiate(pauseCanvas);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        isPaused = false;
        audioManager.PlaySFX(audioManager.jump);
        audioManager.musicSource.UnPause();
        isPaused = false;
        Time.timeScale = 1f;
        Destroy(tempCanvas);
    }

    public void GameOver()
    {
        audioManager.musicSource.Stop();
        tempCanvas = Instantiate(gameOverCanvas);
        audioManager.PlaySFX(audioManager.gameOver);
    }

     public void Retry()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        gameObject.SetActive(true);
        StartCoroutine(retryLevel());
    }

    public void Quit()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        StartCoroutine(LoadMainMenu());
    }

    public void nextScene()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        transition.SetTrigger("end");
        yield return new WaitForSeconds(1f);
        //if index 8 ung credits scene, back to main menu
        if(SceneManager.GetActiveScene().buildIndex == 8)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        transition.SetTrigger("start");
    }

    IEnumerator retryLevel()
    {
        transition.SetTrigger("end");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        transition.SetTrigger("start");
    }

    IEnumerator LoadMainMenu()
    {
        transition.SetTrigger("end");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
        transition.SetTrigger("start");
    }

    
    public void selectLevel(int index)
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        StartCoroutine(LoadSelectedLevel(index));
    }

    IEnumerator LoadSelectedLevel(int index)
    {
        transition.SetTrigger("end");
        yield return new WaitForSeconds(1f);
        Debug.Log("Loading level: " + index);
        SceneManager.LoadScene(index);
        transition.SetTrigger("start");
    }

}
