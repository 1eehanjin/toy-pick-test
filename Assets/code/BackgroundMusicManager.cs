using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    public static BackgroundMusicManager instance;
    private AudioSource audioSource;
    public AudioClip audioClip1;
    public AudioClip audioClip2;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //ResetGame();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void StopBackgroundMusic()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void ChangeBackgroundMusic1()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
            audioSource.clip = audioClip1;
            audioSource.Play();
        }
    }

    public void ChangeBackgroundMusic2()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
            audioSource.clip = audioClip2;
            audioSource.Play();
        }
    }
}