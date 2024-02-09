using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Steamworks;

public class Experience : MonoBehaviour
{
    public Player Player;
    public SkillPanel SkillPanel;

    public float maxExp;
    public float updatedExp;

    public Image expBar;
    public TMP_Text expText, lvlText;

    void Start()
    {
        #region PlayerPrefs exp ayarlarý
        if (PlayerPrefs.GetFloat("maxExp") > 30)
        {
            maxExp = PlayerPrefs.GetFloat("maxExp");
            expText.text = updatedExp.ToString() + "/" + maxExp.ToString();
        }
        else
        {
            maxExp = PlayerPrefs.GetFloat("maxExp", 30);
            expText.text = updatedExp.ToString() + "/" + maxExp.ToString();
        }

        if (PlayerPrefs.GetFloat("updatedExp") > 0)
        {
            updatedExp = PlayerPrefs.GetFloat("updatedExp");
            expText.text = updatedExp.ToString() + "/" + maxExp.ToString();
        }
        else
        {
            updatedExp = PlayerPrefs.GetFloat("updatedExp", 0);
            expText.text = updatedExp.ToString() + "/" + maxExp.ToString();
        }
        #endregion

        #region PlayerPrefs player level ayarlarý
        if (PlayerPrefs.GetInt("playerLevel") <= 1)
        {
            Player.level = 1;
            PlayerPrefs.SetInt("playerLevel", Player.level);
            lvlText.text = Player.level.ToString();
        }
        else
        {
            Player.level = PlayerPrefs.GetInt("playerLevel");
            lvlText.text = Player.level.ToString();
        }

        if (PlayerPrefs.GetFloat("expBarFillAmount") > 0)
            expBar.fillAmount = PlayerPrefs.GetFloat("expBarFillAmount");
        else
            expBar.fillAmount = PlayerPrefs.GetFloat("expBarFillAmount", 0);
        #endregion
    }

    public void EarnExp(float questExp)
    {
        if (updatedExp < maxExp)
        {
            updatedExp += questExp;
            PlayerPrefs.SetFloat("updatedExp", updatedExp);

            expBar.fillAmount = updatedExp / maxExp;
            PlayerPrefs.SetFloat("expBarFillAmount", expBar.fillAmount);

            expText.text = updatedExp.ToString() + "/" + maxExp.ToString();
        }

        if (updatedExp >= maxExp)
        {
            updatedExp -= maxExp;
            PlayerPrefs.SetFloat("updatedExp", updatedExp);

            maxExp += maxExp - 15;
            PlayerPrefs.SetFloat("maxExp", maxExp);

            Player.level += 1;
            PlayerPrefs.SetInt("playerLevel", Player.level);

            if (Player.level == 2)
            {
                SteamUserStats.SetAchievement("achievement_06");
                SteamUserStats.StoreStats();
            }

            if (Player.level == 5)
            {
                SteamUserStats.SetAchievement("achievement_07");
                SteamUserStats.StoreStats();
            }

            if (Player.level == 8)
            {
                SteamUserStats.SetAchievement("achievement_08");
                SteamUserStats.StoreStats();
            }

            if (Player.level == 10)
            {
                SteamUserStats.SetAchievement("achievement_09");
                SteamUserStats.StoreStats();
            }

            expBar.fillAmount = updatedExp / maxExp;
            PlayerPrefs.SetFloat("expBarFillAmount", expBar.fillAmount);

            expText.text = updatedExp.ToString() + "/" + maxExp.ToString();
            lvlText.text = Player.level.ToString();
            SkillPanel.currentLevel.text = "Level " + Player.level.ToString();
        }
    }
}
