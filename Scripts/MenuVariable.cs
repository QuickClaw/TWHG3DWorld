using UnityEngine;
using TMPro;

public class MenuVariable : MonoBehaviour
{
    public TMP_Text txtDeath, txtStars, txtHearts, txtPlayerLevel;

    public TMP_Text txtTheForestCollectedStars, txtTheForestCompletedQuests;
    public TMP_Text txtSnowyCollectedStars, txtSnowyCompletedQuests;
    public TMP_Text txtTheDesertCollectedStars, txtTheDesertCompletedQuests;
    public TMP_Text txtFoggyCollectedStars, txtFoggyCompletedQuests;
    public TMP_Text txtInvisibleCollectedStars, txtInvisibleCompletedQuests;

    public GameObject tickImage1, tickImage2;
    public GameObject tickImage3, tickImage4;
    public GameObject tickImage5, tickImage6;
    public GameObject tickImage7, tickImage8;
    public GameObject tickImage9, tickImage10;

    void Awake()
    {
        txtDeath.text = "Deaths:" + PlayerPrefs.GetInt("playerDeath").ToString();
        txtStars.text = "Stars:" + PlayerPrefs.GetInt("playerStar").ToString();
        txtHearts.text = "Hearts:" + PlayerPrefs.GetInt("playerHeart").ToString();
        txtPlayerLevel.text = "Level:" + PlayerPrefs.GetInt("playerLevel").ToString();

        // The Forest 
        txtTheForestCollectedStars.text = PlayerPrefs.GetInt("starsTheForest").ToString() + "/15";
        txtTheForestCompletedQuests.text = PlayerPrefs.GetInt("completedQuestsTheForest").ToString() + "/6";

        // Snowy
        txtSnowyCollectedStars.text = PlayerPrefs.GetInt("starsSnowy").ToString() + "/42";
        txtSnowyCompletedQuests.text = PlayerPrefs.GetInt("completedQuestsSnowy").ToString() + "/8";

        // The Desert
        txtTheDesertCollectedStars.text = PlayerPrefs.GetInt("starsTheDesert").ToString() + "/157";
        txtTheDesertCompletedQuests.text = PlayerPrefs.GetInt("completedQuestsTheDesert").ToString() + "/12";

        // Darkness
        txtFoggyCollectedStars.text = PlayerPrefs.GetInt("starsFoggy").ToString() + "/11";
        txtFoggyCompletedQuests.text = PlayerPrefs.GetInt("completedQuestsFoggy").ToString() + "/3";

        // Invisible
        txtInvisibleCollectedStars.text = PlayerPrefs.GetInt("starsInvisible").ToString() + "/39";
        txtInvisibleCompletedQuests.text = PlayerPrefs.GetInt("completedQuestsInvisible").ToString() + "/3";

        if (txtTheForestCollectedStars.text == "15/15")
            tickImage2.SetActive(true);       
        if(txtTheForestCompletedQuests.text == "6/6")
            tickImage1.SetActive(true);

        if (txtSnowyCollectedStars.text == "42/42")
            tickImage4.SetActive(true);
        if (txtSnowyCompletedQuests.text == "8/8")
            tickImage3.SetActive(true);

        if (txtTheDesertCollectedStars.text == "157/157")
            tickImage6.SetActive(true);
        if (txtTheDesertCompletedQuests.text == "12/12")
            tickImage5.SetActive(true);

        if (txtFoggyCollectedStars.text == "11/11")
            tickImage8.SetActive(true);
        if (txtFoggyCompletedQuests.text == "3/3")
            tickImage7.SetActive(true);

        if (txtInvisibleCollectedStars.text == "39/39")
            tickImage10.SetActive(true);
        if (txtInvisibleCompletedQuests.text == "3/3")
            tickImage9.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("PlayerPrefs resetlendi.");
        }
    }
}
