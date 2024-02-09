using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class MasterSlider : MonoBehaviour
{
    public AudioMixer masterMixer;
    public Slider masterSlider;
    public TMP_Text txtMasterVolume;
    public float masterValue;

    public AudioSource sliderAudioSource;

    public void ChangeMasterVolume(float sliderValue)
    {
        masterMixer.SetFloat("MasterVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("masterVolume", sliderValue);

        masterValue = sliderValue * 100;
        txtMasterVolume.text = masterValue.ToString("f0");
        sliderAudioSource.Play();
    }
}
