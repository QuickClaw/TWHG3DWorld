using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeMusic : MonoBehaviour
{
    private AudioSource musicAudioSource;
    public AudioClip[] zoneMusics;

    private void Awake()
    {
        musicAudioSource = GameObject.FindGameObjectWithTag("MusicManager").GetComponent<AudioSource>();
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            musicAudioSource.clip = zoneMusics[0];
            musicAudioSource.Play();
        }

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            musicAudioSource.clip = zoneMusics[1];
            musicAudioSource.Play();
        }

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            musicAudioSource.clip = zoneMusics[2];
            musicAudioSource.Play();
        }

        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            musicAudioSource.clip = zoneMusics[3];
            musicAudioSource.Play();
        }

        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            musicAudioSource.volume = 0f;
            musicAudioSource.Play();
        }

        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            musicAudioSource.clip = zoneMusics[4];
            musicAudioSource.Play();
        }
    }
}
