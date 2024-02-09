using UnityEngine;
using TMPro;
using Steamworks;

public class LastSceneVariables : MonoBehaviour
{
    public TMP_Text txtDeath, txtStars, txtHearts, txtPlayerLevel;
    public TMP_Text txtSteamName;

    void Awake()
    {
        txtDeath.text = "Deaths:" + PlayerPrefs.GetInt("playerDeath").ToString();
        txtStars.text = "Stars:" + PlayerPrefs.GetInt("playerStar").ToString();
        txtHearts.text = "Hearts:" + PlayerPrefs.GetInt("playerHeart").ToString();
        txtPlayerLevel.text = "Level:" + PlayerPrefs.GetInt("playerLevel").ToString();

        txtSteamName.text = SteamFriends.GetPersonaName();
    }
}
