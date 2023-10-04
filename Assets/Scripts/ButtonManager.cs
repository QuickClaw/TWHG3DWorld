using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject newGamePanel;
    public GameObject creditsPanel;

    public UISlider UISlider;
    public MusicSlider MusicSlider;
    public MasterSlider MasterSlider;
    public EffectSlider EffectSlider;

    bool creditsPanelOpen;
    bool settingPanelOpen;

    private void Start()
    {
        creditsPanelOpen = false;
        settingPanelOpen = false;

        if (PlayerPrefs.HasKey("masterVolume") == false)
        {
            MasterSlider.masterSlider.value = 100f;
            MasterSlider.masterMixer.SetFloat("MasterVol", Mathf.Log10(MasterSlider.masterSlider.value) * 20);
        }
        else
        {
            MasterSlider.masterSlider.value = PlayerPrefs.GetFloat("masterVolume");
            MasterSlider.masterMixer.SetFloat("MasterVol", Mathf.Log10(MasterSlider.masterSlider.value) * 20);
        }

        if (PlayerPrefs.HasKey("musicVolume") == false)
        {
            MusicSlider.musicSlider.value = 100f;
            MusicSlider.musicMixer.SetFloat("MusicVol", Mathf.Log10(MusicSlider.musicSlider.value) * 20);
        }
        else
        {
            MusicSlider.musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
            MusicSlider.musicMixer.SetFloat("MusicVol", Mathf.Log10(MusicSlider.musicSlider.value) * 20);
        }

        if (PlayerPrefs.HasKey("UIVolume") == false)
        {
            UISlider.UI_Slider.value = 100f;
            UISlider.UIMixer.SetFloat("UIVol", Mathf.Log10(UISlider.UI_Slider.value) * 20);
        }
        else
        {
            UISlider.UI_Slider.value = PlayerPrefs.GetFloat("UIVolume");
            UISlider.UIMixer.SetFloat("UIVol", Mathf.Log10(UISlider.UI_Slider.value) * 20);
        }

        if (PlayerPrefs.HasKey("EffectVolume") == false)
        {
            EffectSlider.effectSlider.value = 100f;
            EffectSlider.effectMixer.SetFloat("EffectVol", Mathf.Log10(EffectSlider.effectSlider.value) * 20);
        }
        else
        {
            EffectSlider.effectSlider.value = PlayerPrefs.GetFloat("EffectVolume");
            EffectSlider.effectMixer.SetFloat("EffectVol", Mathf.Log10(EffectSlider.effectSlider.value) * 20);
        }
    }

    public void OpenURL()
    {
        Application.OpenURL("https://store.steampowered.com/developer/zummar");
    }

    public void OpenCloseSettingPanel()
    {
        if (settingPanelOpen == false)
        {
            settingsPanel.SetActive(true);
            settingPanelOpen = true;
        }
        else
        {
            settingsPanel.SetActive(false);
            settingPanelOpen = false;
        }
    }

    public void OpenNewGamePanel()
    {
        newGamePanel.SetActive(true);
    }

    public void CloseNewGamePanel()
    {
        newGamePanel.SetActive(false);
    }

    public void StartNewGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("The Forest");
    }

    public void OpenCloseCreditsPanel()
    {
        if (creditsPanelOpen == false)
        {
            creditsPanel.SetActive(true);
            creditsPanelOpen = true;
        }
        else
        {
            creditsPanel.SetActive(false);
            creditsPanelOpen = false;
        }
    }
}
