using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadingSlider;

    public TMP_Text txtProgress;

    // Ýstediðin bölümü açar
    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    // Kaldýðýn bölümden devam eder
    public void ContinueLevel()
    {
        StartCoroutine(ContinueAsynchronously());
    }

    // Yeni oyun baþlatýr
    public void StartNewGame()
    {
        StartCoroutine(StartNewGameAsynchronously());
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {        
            float progress = Mathf.Clamp01(operation.progress / .9f);

            loadingSlider.value = progress;
            txtProgress.text = (progress * 100f).ToString("f0") + "%";

            yield return null;
        }
    }

    IEnumerator ContinueAsynchronously()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(PlayerPrefs.GetInt("saveIndex") + 1);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            loadingSlider.value = progress;
            txtProgress.text = (progress * 100f).ToString("f0") + "%";

            yield return null;
        }
    }

    IEnumerator StartNewGameAsynchronously()
    {
        PlayerPrefs.DeleteAll();
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            loadingSlider.value = progress;
            txtProgress.text = (progress * 100f).ToString("f0") + "%";

            yield return null;
        }
    }
}
