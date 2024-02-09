using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenSettings : MonoBehaviour
{
    public bool isFullScreen;
    public bool isWindowed;
    public bool isBorderless;

    public GameObject tickIcon1, tickIcon2, tickIcon3;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("isFullScreen") == 1)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            isFullScreen = true;
            isWindowed = false;
            isBorderless = false;

            tickIcon1.SetActive(true);
            tickIcon2.SetActive(false);
            tickIcon3.SetActive(false);
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            isFullScreen = true;
            isWindowed = false;
            isBorderless = false;

            tickIcon1.SetActive(true);
            tickIcon2.SetActive(false);
            tickIcon3.SetActive(false);
        }

        if (PlayerPrefs.GetInt("isWindowed") == 1)
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
            isFullScreen = false;
            isWindowed = true;
            isBorderless = false;

            tickIcon1.SetActive(false);
            tickIcon2.SetActive(true);
            tickIcon3.SetActive(false);
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            isFullScreen = true;
            isWindowed = false;
            isBorderless = false;

            tickIcon1.SetActive(true);
            tickIcon2.SetActive(false);
            tickIcon3.SetActive(false);
        }

        if (PlayerPrefs.GetInt("isBorderless") == 1)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            isFullScreen = false;
            isWindowed = false;
            isBorderless = true;

            tickIcon1.SetActive(false);
            tickIcon2.SetActive(false);
            tickIcon3.SetActive(true);
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            isFullScreen = true;
            isWindowed = false;

            tickIcon1.SetActive(true);
            tickIcon2.SetActive(false);
            tickIcon3.SetActive(false);
        }
    }

    public void SetFullScreen()
    {
        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        isFullScreen = true;
        isWindowed = false;

        PlayerPrefs.SetInt("isFullScreen", 1);
        PlayerPrefs.DeleteKey("isWindowed");
        PlayerPrefs.DeleteKey("isBorderless");

        tickIcon1.SetActive(true);
        tickIcon2.SetActive(false);
        tickIcon3.SetActive(false);
    }

    public void SetWindowed()
    {
        Screen.fullScreenMode = FullScreenMode.Windowed;
        isFullScreen = false;
        isWindowed = true;

        PlayerPrefs.SetInt("isWindowed", 1);
        PlayerPrefs.DeleteKey("isFullScreen");
        PlayerPrefs.DeleteKey("isBorderless");

        tickIcon1.SetActive(false);
        tickIcon2.SetActive(true);
        tickIcon3.SetActive(false);
    }

    public void SetBorderless()
    {
        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        isFullScreen = false;
        isWindowed = true;

        PlayerPrefs.SetInt("isBorderless", 1);
        PlayerPrefs.DeleteKey("isFullScreen");
        PlayerPrefs.DeleteKey("isWindowed");

        tickIcon1.SetActive(false);
        tickIcon2.SetActive(false);
        tickIcon3.SetActive(true);
    }
}
