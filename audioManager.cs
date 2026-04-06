using UnityEngine;

using UnityEngine.SceneManagement;

public class audioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] public AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip bgMain;
    public AudioClip bgCave;
    public AudioClip bgForest;
    public AudioClip bgCastle;
    public AudioClip gameOver;
    public AudioClip faah;
    public AudioClip playerHit;
    public AudioClip buttonClick;
    public AudioClip plantAttack;
    public AudioClip croak1;     
    public AudioClip croak2;
    public AudioClip jump;

    private void Start()
    {
         Debug.Log("Current Scene Index: " + SceneManager.GetActiveScene().buildIndex);
        switch(SceneManager.GetActiveScene().buildIndex)
        {
            case 2:
            case 3:
            case 4:
                musicSource.clip = bgCave;
                
                break;
            case 5:
            case 6:
            case 7:
                musicSource.clip = bgForest;
                break;
            case 1:
                musicSource.clip = bgCastle;
                break;
            default:
                musicSource.clip = bgMain;
                break;
        }
        musicSource.volume = 0.5f;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

}
