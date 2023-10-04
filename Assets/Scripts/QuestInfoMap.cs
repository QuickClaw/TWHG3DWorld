using UnityEngine;
using TMPro;

public class QuestInfoMap : MonoBehaviour
{
    public Sign Sign;
    public AddToJournal AddToJournal;
    public PauseMenu PauseMenu;

    public TMP_Text questName;
    public TMP_Text questDesc;
    public TMP_Text questExpReward;
    public TMP_Text questStarReward;

    public GameObject questInfoPanel;

    void Start()
    {
        questInfoPanel.SetActive(false);

        questName.text = Sign.questTitle;
        questDesc.text = Sign.questDesc;
        questExpReward.text = "Exp: " + Sign.exp.ToString();
        questStarReward.text = "Star: " + Sign.star.ToString();
    }
    public void Update()
    {
        if (PauseMenu.isMapOpen == false)
        {
            questInfoPanel.SetActive(false);
        }
    }

    public void ShowQuestInfo()
    {
        if (AddToJournal.questTaken || AddToJournal.questDone)
        {
            questInfoPanel.SetActive(true);
        }
    }

    public void HideQuestInfo()
    {
        questInfoPanel.SetActive(false);
    }
}
