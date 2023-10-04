using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public TPS TPS;
    public Movement Movement;
    public Journal Journal;
    public Player Player;
    public SkillPanel SkillPanel;
    public AddToJournal AddToJournal;

    public GameObject darkPanel;
    public Camera mapCamera;

    public TMP_Text questCount;
    public TMP_Text deathCount;

    public GameObject map;
    public bool isMapOpen;
    public bool isPaused;

    void Start()
    {
        darkPanel.SetActive(false);
        isPaused = false;

        if (Player.sceneName is "The Forest")
            questCount.text = Player.questTheForest.ToString() + "/" + AddToJournal.questNames.Count;

        if (Player.sceneName is "Snowy")
            questCount.text = Player.questSnowy.ToString() + "/" + AddToJournal.questNames.Count;

        if (Player.sceneName is "The Desert")
            questCount.text = Player.questTheDesert.ToString() + "/" + AddToJournal.questNames.Count;

        if (Player.sceneName is "Foggy")
            questCount.text = Player.questFoggy.ToString() + "/" + AddToJournal.questNames.Count;

        if (Player.sceneName is "Invisible")
            questCount.text = Player.questInvisible.ToString() + "/" + AddToJournal.questNames.Count;
    }

    void Update()
    {
        // Oyunu durdurur, devam ettirir
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused == false)
                Pause();
            else
                Resume();
        }

        // Map açar, kapatýr
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (isMapOpen == false)
                OpenMap();
            else
                CloseMap();
        }
    }

    public void Resume()
    {
        darkPanel.SetActive(false);
        isPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1f;

        TPS.enabled = true;
        Movement.enabled = true;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void Pause()
    {
        darkPanel.SetActive(true);
        isPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;

        TPS.enabled = false;
        Movement.enabled = false;
    }

    private void OpenMap()
    {
        map.SetActive(true);
        isMapOpen = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void CloseMap()
    {
        map.SetActive(false);
        isMapOpen = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
