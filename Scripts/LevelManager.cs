using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Button[] RegionButtons;
    public RawImage[] LockImages;
    public Image[] ButtonImages;

    private void Start()
    {
        int saveIndex = PlayerPrefs.GetInt("saveIndex");

        for (int i = 0; i < RegionButtons.Length; i++)
        {
            if (i <= saveIndex)
            {
                RegionButtons[i].interactable = true;
                LockImages[i].gameObject.SetActive(false);
                RegionButtons[i].GetComponent<Image>().raycastTarget = true;

            }
            else
            {
                RegionButtons[i].interactable = false;
                LockImages[i].gameObject.SetActive(true);
                RegionButtons[i].GetComponent<Image>().raycastTarget = false;
            }
        }
    }
    public void Continue()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("saveIndex") + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

