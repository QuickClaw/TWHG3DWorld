using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AddToJournal : MonoBehaviour
{
    public Player Player;
    public Sign Sign;

    public GameObject questTakenIcon, questTakenIconMinimap;
    public AudioSource journalAudioSource;
    public Collider journalCollider;
    public TMP_Text txtQuestTitle;

    public Animation questNameAnimation;

    public List<TMP_Text> questNames;

    public bool questTaken;
    public bool questDone;

    public int numberOfQuest;

    private void Start()
    {
        numberOfQuest = PlayerPrefs.GetInt("numberOfQuest" + gameObject.name);

        if (PlayerPrefs.HasKey("questTaken" + gameObject.name))
            questTaken = true;
        else
            questTaken = false;

        if (questTaken && questDone == false)
        {
            AddQuestName(Sign.questTitle + "\n" + "• " + Sign.questDesc + "\n" + "<color=orange>Exp: " + Sign.exp.ToString() + "</color>" + "    " + "<color=yellow>Star: " + Sign.star.ToString() + "</color>");

            journalAudioSource.Play();

            Sign.signIcon.SetActive(false);
            Sign.signIconMinimap.SetActive(false);

            questTakenIcon.SetActive(true);
            questTakenIconMinimap.SetActive(true);
            journalCollider.gameObject.SetActive(false);
        }
        if (questTaken && questDone)
        {
            AddQuestName(Sign.questTitle + "\n" + "• " + Sign.questDesc + "\n" + "<color=orange>Exp: " + Sign.exp.ToString() + "</color>" + "    " + "<color=yellow>Star: " + Sign.star.ToString() + "</color>" + "\n" + "<color=green>QUEST COMPLETED</color>");
            journalCollider.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag is "Player")
        {
            AddQuestName(Sign.questTitle + "\n" + "• " + Sign.questDesc + "\n" + "<color=orange>Exp: " + Sign.exp.ToString() + "</color>" + "    " + "<color=yellow>Star: " + Sign.star.ToString() + "</color>");
            journalAudioSource.Play();
            PlayQuestNameAnimation(Sign.questTitle);

            Sign.signIcon.SetActive(false);
            Sign.signIconMinimap.SetActive(false);

            questTakenIcon.SetActive(true);
            questTakenIconMinimap.SetActive(true);

            questTaken = true;
            PlayerPrefs.SetInt("questTaken" + gameObject.name, 1);
        }
    }

    public void AddQuestName(string phrase)
    {
        journalCollider.gameObject.SetActive(false);

        if (Player.sceneName == "The Forest")
        {
            Player.questInJournalTheForest++;
            Player.questCountJournal.text = Player.questInJournalTheForest.ToString() + "/6";

            numberOfQuest = Player.questInJournalTheForest;
            PlayerPrefs.SetInt("numberOfQuest" + gameObject.name, numberOfQuest);

            questNames[Player.questInJournalTheForest - 1].text = phrase;
        }

        if (Player.sceneName == "Snowy")
        {
            Player.questInJournalSnowy++;
            Player.questCountJournal.text = Player.questInJournalSnowy.ToString() + "/8";

            numberOfQuest = Player.questInJournalSnowy;
            PlayerPrefs.SetInt("numberOfQuest" + gameObject.name, numberOfQuest);

            questNames[Player.questInJournalSnowy - 1].text = phrase;
        }

        if (Player.sceneName == "The Desert")
        {
            Player.questInJournalTheDesert++;
            Player.questCountJournal.text = Player.questInJournalTheDesert.ToString() + "/13";

            numberOfQuest = Player.questInJournalTheDesert;
            PlayerPrefs.SetInt("numberOfQuest" + gameObject.name, numberOfQuest);

            questNames[Player.questInJournalTheDesert - 1].text = phrase;
        }

        if (Player.sceneName == "Foggy")
        {
            Player.questInJournalFoggy++;
            Player.questCountJournal.text = Player.questInJournalFoggy.ToString() + "/3";

            numberOfQuest = Player.questInJournalFoggy;
            PlayerPrefs.SetInt("numberOfQuest" + gameObject.name, numberOfQuest);

            questNames[Player.questInJournalFoggy - 1].text = phrase;
        }

        if (Player.sceneName == "Invisible")
        {
            Player.questInJournalInvisible++;
            Player.questCountJournal.text = Player.questInJournalInvisible.ToString() + "/3";

            numberOfQuest = Player.questInJournalInvisible;
            PlayerPrefs.SetInt("numberOfQuest" + gameObject.name, numberOfQuest);

            questNames[Player.questInJournalInvisible - 1].text = phrase;
        }
    }

    public void PlayQuestNameAnimation(string questName)
    {
        txtQuestTitle.text = questName + " added to your journal." + "\n" + " Press J to open your journal.";
        questNameAnimation.Play();
    }
}
