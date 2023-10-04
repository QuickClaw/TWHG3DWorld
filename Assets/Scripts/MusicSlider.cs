using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class MusicSlider : MonoBehaviour
{
    public AudioMixer musicMixer;
    public Slider musicSlider;
    public TMP_Text txtMusicVolume;
    public float musicValue;

    public AudioSource sliderAudioSource;

    public void ChangeMusicVolume(float sliderValue)
    {
        musicMixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("musicVolume", sliderValue);

        musicValue = sliderValue * 100;
        txtMusicVolume.text = musicValue.ToString("f0");
        sliderAudioSource.Play();
    }
}
