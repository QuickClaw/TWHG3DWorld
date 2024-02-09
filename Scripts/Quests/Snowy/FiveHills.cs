using UnityEngine;
using TMPro;

public class FiveHills : MonoBehaviour
{
    // Class alýnacak objeye atanmalý

    public Player Player;
    public Sign Sign;
    public Experience Experience;
    public AddToJournal AddToJournal;
    public PauseMenu PauseMenu;
    public StarPickup StarPickup;

    public AudioSource questAudioSource;
    public AudioClip questCompletedSound;

    public ParticleSystem questTextParticle;
    public AudioSource pickUpAudioSource;
    public bool questDone;
    public TMP_Text txtFiveHillsHeartCount;

    public GameObject fiveHillsHeartSetActive;

    public TMP_Text expGain, starGain;
    public Animation expGainAnim, starGainAnim;

    private void Awake()
    {
        Player.heartFiveHillsCount = PlayerPrefs.GetInt("heartFiveHills");

        questAudioSource.clip = questCompletedSound;
        fiveHillsHeartSetActive.SetActive(false);

        if (PlayerPrefs.HasKey("heartFiveHillsTextBool"))
        {
            txtFiveHillsHeartCount.text = Player.heartFiveHillsCount.ToString() + "/9";
            fiveHillsHeartSetActive.SetActive(true);
        }

        if (PlayerPrefs.HasKey("questDoneFiveHills"))
        {
            AddToJournal.questDone = true;

            Sign.done.SetActive(true);
            Sign.doneIcon.SetActive(true);
            Sign.signIcon.SetActive(false);

            Sign.doneIconMinimap.SetActive(true);
            Sign.signIconMinimap.SetActive(false);

            AddToJournal.questTakenIcon.SetActive(false);
            AddToJournal.questTakenIconMinimap.SetActive(false);
            AddToJournal.journalCollider.enabled = false;

            fiveHillsHeartSetActive.SetActive(false);
        }
    }

    private void Update()
    {
        if (Player.heartFiveHillsCount == 9)
        {
            fiveHillsHeartSetActive.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Menzile girdiðin an objeyi alýrsýn
        if (other.tag is "Player")
        {
            if (Player.heartFiveHillsCount == 8)
            {
                QuestCompleted();

                questTextParticle.Play();
                questAudioSource.Play();

                Sign.done.SetActive(true);
                Sign.doneIcon.SetActive(true);
                Sign.signIcon.SetActive(false);

                Sign.doneIconMinimap.SetActive(true);
                Sign.signIconMinimap.SetActive(false);

                fiveHillsHeartSetActive.SetActive(false);

                PlayExpStarTextAnim();
            }

            pickUpAudioSource.Play();

            Player.heartFiveHillsCount++;
            PlayerPrefs.SetInt("heartFiveHills", Player.heartFiveHillsCount);
            PlayerPrefs.SetInt("heartFiveHillsTextBool", 1); // Text active için bool görevi görür

            txtFiveHillsHeartCount.text = Player.heartFiveHillsCount.ToString() + "/9";
            fiveHillsHeartSetActive.SetActive(true);
        }
    }

    public void QuestCompleted()
    {
        AddToJournal.questDone = true;
        PlayerPrefs.SetInt("questDoneFiveHills", 1);

        Player.questSnowy++;
        PlayerPrefs.SetInt("completedQuestsSnowy", Player.questSnowy);

        Player.experience += Sign.exp;

        Player.star += Sign.star;
        PlayerPrefs.SetInt("playerStar", Player.star);

        StarPickup.txtStarCount.text = Player.star.ToString();
        PauseMenu.questCount.text = Player.questSnowy.ToString() + "/" + AddToJournal.questNames.Count;

        if (AddToJournal.questTaken)
        {
            AddToJournal.questNames[AddToJournal.numberOfQuest - 1].text = Sign.questTitle + "\n" + "• " + Sign.questDesc + "\n" + "<color=orange>Exp: " + Sign.exp.ToString() + "</color>" + "    " + "<color=yellow>Star: " + Sign.star.ToString() + "</color>" + "\n" + "<color=green>QUEST COMPLETED</color>";
        }

        AddToJournal.questTakenIcon.SetActive(false);
        AddToJournal.questTakenIconMinimap.SetActive(false);
        AddToJournal.journalCollider.enabled = false;

        Experience.EarnExp(Sign.exp);
    }

    public void PlayExpStarTextAnim()
    {
        expGain.text = "+" + Sign.exp + " experience";
        starGain.text = "+" + Sign.star + " stars";

        expGainAnim.Play();
        starGainAnim.Play();
    }
}
