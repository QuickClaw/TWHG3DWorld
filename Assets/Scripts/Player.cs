using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    public Experience Experience;
    public Movement Movement;
    public Potions Potions;

    public TMP_Text questCountJournal;

    Scene currentScene;
    public static string sceneName;

    public float experience;  // Level atlamaya yarar
    public int level;       // Oyuncu kaç level
    public int star;        // Geliþtirmeler yapmaya yarar
    public int heart;       // Ölünce yýldýz kaybetmeyi önler
    public int death;       // Ölüm sayýsý

    // Upgrade levellarý
    public int speedLvl;
    public int dashCdLvl;
    public int potionDurationLvl;

    // Haritaya göre oyuncunun topladýðý yýldýz sayýsý
    public int playerStarsTheForest;
    public int playerStarsSnowy;
    public int playerStarsTheDesert;
    public int playerStarsFoggy;
    public int playerStarsInvisible;

    // Görev deðiþkenleri
    #region The Forest
    public int boxCount;
    public int starCount;
    #endregion

    #region Snowy
    public int flagCount;
    public int mountainStarsLeft;
    public int mountainStarsRight;
    public int heartFiveHillsCount;
    public int starsFoothills;
    public int presents;
    #endregion

    #region The Desert
    public int starRibCount;
    public int starRedMaze;
    public int starRedTunnel;
    public int platformQuests;
    public int mudBones;
    public int starsOfArena;
    #endregion

    #region Foggy
    public int pumpkin;
    #endregion

    #region Invisible
    public int pinkStars;
    public int theLastMazeStars;
    public int impossibleStars;
    #endregion 

    // Haritaya göre haritada bulunan toplam ve kalan yýldýz sayýsý
    #region The Forest
    public int allStarsTheForest = 15;
    public int remainingStarsTheForest;
    #endregion

    #region Snowy
    public int allStarsSnowy = 42;
    public int remainingStarsSnowy;
    #endregion

    #region The Desert
    public int allStarsTheDesert = 157;
    public int remainingStarsTheDesert;
    #endregion

    #region Foggy
    public int allStarsFoggy = 11;
    public int remainingStarsFoggy;
    #endregion

    #region Invisible
    public int allStarsInvisible = 39;
    public int remainingStarsInvisible;
    #endregion

    // Haritaya göre tamamlanmýþ görev sayýsý
    #region The Forest
    public int questInJournalTheForest;
    public int questTheForest;
    #endregion

    #region Snowy
    public int questInJournalSnowy;
    public int questSnowy;
    #endregion

    #region The Desert
    public int questInJournalTheDesert;
    public int questTheDesert;
    #endregion

    #region Foggy
    public int questInJournalFoggy;
    public int questFoggy;
    #endregion

    #region Invisible
    public int questInJournalInvisible;
    public int questInvisible;
    #endregion

    private void Awake()
    {
        Movement.enabled = true;
        Time.timeScale = 1f;

        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        star = PlayerPrefs.GetInt("playerStar");
        death = PlayerPrefs.GetInt("playerDeath");
        heart = PlayerPrefs.GetInt("playerHeart");
        level = PlayerPrefs.GetInt("playerLevel");

        speedLvl = PlayerPrefs.GetInt("speedLvl");
        dashCdLvl = PlayerPrefs.GetInt("dashCdLvl");
        potionDurationLvl = PlayerPrefs.GetInt("potionDurationLvl");

        platformQuests = PlayerPrefs.GetInt("platformQuests"); // The Desert görevi

        questTheForest = PlayerPrefs.GetInt("completedQuestsTheForest");
        questSnowy = PlayerPrefs.GetInt("completedQuestsSnowy");
        questTheDesert = PlayerPrefs.GetInt("completedQuestsTheDesert");
        questFoggy = PlayerPrefs.GetInt("completedQuestsFoggy");
        questInvisible = PlayerPrefs.GetInt("completedQuestsInvisible");

        questInJournalTheForest = PlayerPrefs.GetInt("questInJournalTheForest");
        questInJournalSnowy = PlayerPrefs.GetInt("questInJournalSnowy");
        questInJournalTheDesert = PlayerPrefs.GetInt("questInJournalTheDesert");
        questInJournalFoggy = PlayerPrefs.GetInt("questInJournalFoggy");
        questInJournalInvisible = PlayerPrefs.GetInt("questInJournalInvisible");

        playerStarsTheForest = PlayerPrefs.GetInt("starsTheForest");
        playerStarsSnowy = PlayerPrefs.GetInt("starsSnowy");
        playerStarsTheDesert = PlayerPrefs.GetInt("starsTheDesert");
        playerStarsFoggy = PlayerPrefs.GetInt("starsFoggy");
        playerStarsInvisible = PlayerPrefs.GetInt("starsInvisible");

        remainingStarsTheForest = PlayerPrefs.GetInt("remainingStarsTheForest");
        remainingStarsSnowy = PlayerPrefs.GetInt("remainingStarsSnowy");
        remainingStarsTheDesert = PlayerPrefs.GetInt("remainingStarsTheDesert");
        remainingStarsFoggy = PlayerPrefs.GetInt("remainingStarsFoggy");
        remainingStarsInvisible = PlayerPrefs.GetInt("remainingStarsInvisible");

        if (sceneName == "The Forest")
            questCountJournal.text = questInJournalTheForest.ToString() + "/6";

        if (sceneName == "Snowy")
            questCountJournal.text = questInJournalSnowy.ToString() + "/8";

        if (sceneName == "The Desert")
            questCountJournal.text = questInJournalTheDesert.ToString() + "/12";

        if (sceneName == "Foggy")
            questCountJournal.text = questInJournalFoggy.ToString() + "/3";

        if (sceneName == "Invisible")
            questCountJournal.text = questInJournalInvisible.ToString() + "/3";

    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.B))
    //    {
    //        PlayerPrefs.DeleteAll();
    //        Debug.Log("PlayerPrefs resetlendi.");
    //    }
    //
    //    if (Input.GetKey(KeyCode.L))
    //    {
    //       Player.star += 100;
    //       StarPickup.txtStarCount.text = Player.star.ToString();
    //    }
    //
    //    if (Input.GetKey(KeyCode.Space))
    //    {
    //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //    }
    //}  
}

