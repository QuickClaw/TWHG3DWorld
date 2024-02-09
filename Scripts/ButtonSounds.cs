using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    public AudioSource buttonAudioSource;
    public AudioClip clickSound, highlightSound;

    public void ClickSound()
    {
        buttonAudioSource.PlayOneShot(clickSound);
    }

    public void HighlightSound()
    {
        buttonAudioSource.PlayOneShot(highlightSound);
    }
}
