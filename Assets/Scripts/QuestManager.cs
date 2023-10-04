using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Player Player;

    public GameObject end;

    private void Start()
    {
        end.SetActive(false);
    }

    void Update()
    {
        if (Player.sceneName == "The Forest")
            if (Player.questTheForest == 6)
                end.SetActive(true);

        if (Player.sceneName == "Snowy")
            if (Player.questSnowy == 8)
                end.SetActive(true);

        if (Player.sceneName == "The Desert")
            if (Player.questTheDesert == 12)
                end.SetActive(true);

        if (Player.sceneName == "Foggy")
            if (Player.questFoggy == 3)
                end.SetActive(true);

        if (Player.sceneName == "Invisible")
            if (Player.questInvisible == 3)
                end.SetActive(true);
    }
}
