using UnityEngine;
using Steamworks;

public class SteamAchievements : MonoBehaviour
{
    void Start()
    {
        if (!SteamManager.Initialized)
        {
            return;
        }

        if (Player.sceneName == "The Forest")
        {
            SteamUserStats.SetAchievement("achievement_01");
            SteamUserStats.StoreStats();
        }

        if (Player.sceneName == "Snowy")
        {
            SteamUserStats.SetAchievement("achievement_02");
            SteamUserStats.StoreStats();
        }

        if (Player.sceneName == "The Desert")
        {
            SteamUserStats.SetAchievement("achievement_03");
            SteamUserStats.StoreStats();
        }

        if (Player.sceneName == "Foggy")
        {
            SteamUserStats.SetAchievement("achievement_04");
            SteamUserStats.StoreStats();
        }

        if (Player.sceneName == "Invisible")
        {
            SteamUserStats.SetAchievement("achievement_05");
            SteamUserStats.StoreStats();
        }       
    }
}
