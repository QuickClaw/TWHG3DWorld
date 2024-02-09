using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class UISlider : MonoBehaviour
{
    public AudioMixer UIMixer;
    public Slider UI_Slider;
    public TMP_Text txtUIVolume;
    public float UIValue;

    public AudioSource sliderAudioSource;

    public void ChangeUIVolume(float sliderValue)
    {
        UIMixer.SetFloat("UIVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("UIVolume", sliderValue);

        UIValue = sliderValue * 100;
        txtUIVolume.text = UIValue.ToString("f0");
        sliderAudioSource.Play();
    }
}
