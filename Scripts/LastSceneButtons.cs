using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LastSceneButtons : MonoBehaviour
{
    public void ReturnMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenURL()
    {
        Application.OpenURL("https://store.steampowered.com/search/?developer=Batuhan%20Ertan&ndl=1");
    }
}
