using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class EffectSlider : MonoBehaviour
{
    public AudioMixer effectMixer;
    public Slider effectSlider;
    public TMP_Text txtEffectVolume;
    public float effectValue;

    public AudioSource sliderAudioSource;

    public void ChangeEffectVolume(float sliderValue)
    {
        effectMixer.SetFloat("EffectVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("EffectVolume", sliderValue);

        effectValue = sliderValue * 100;
        txtEffectVolume.text = effectValue.ToString("f0");
        sliderAudioSource.Play();
    }
}
