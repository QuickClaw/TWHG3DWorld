using UnityEngine;
using TMPro;

public class TheMysteryOfTheMountain2 : MonoBehaviour
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

    public TMP_Text starsOnTheRightCount;
    public GameObject txtStarsOnTheRightSetActive;

    public TMP_Text expGain, starGain;
    public Animation expGainAnim, starGainAnim;

    private void Awake()
    {
        Player.mountainStarsRight = PlayerPrefs.GetInt("mountainStarsRight");

        questAudioSource.clip = questCompletedSound;
        txtStarsOnTheRightSetActive.SetActive(false);

        if (PlayerPrefs.HasKey("mountainStarsRightTextBool"))
        {
            starsOnTheRightCount.text = Player.mountainStarsRight.ToString() + "/5";
            txtStarsOnTheRightSetActive.SetActive(true);
        }

        if (PlayerPrefs.HasKey("questDoneMystery2"))
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
        }
    }

    private void Update()
    {
        if (Player.mountainStarsRight == 5)
        {
            txtStarsOnTheRightSetActive.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Menzile girdiðin an objeyi alýrsýn
        if (other.tag is "Player")
        {
            if (Player.mountainStarsRight == 4)
            {
                QuestCompleted();

                questTextParticle.Play();
                questAudioSource.Play();

                Sign.done.SetActive(true);
                Sign.doneIcon.SetActive(true);
                Sign.signIcon.SetActive(false);

                Sign.doneIconMinimap.SetActive(true);
                Sign.signIconMinimap.SetActive(false);

                txtStarsOnTheRightSetActive.SetActive(false);

                PlayExpStarTextAnim();
            }

            pickUpAudioSource.Play();

            Player.mountainStarsRight++;
            PlayerPrefs.SetInt("mountainStarsRight", Player.mountainStarsRight);
            PlayerPrefs.SetInt("mountainStarsRightTextBool", 1); // Text active için bool görevi görür

            starsOnTheRightCount.text = Player.mountainStarsRight.ToString() + "/5";
            txtStarsOnTheRightSetActive.SetActive(true);
        }
    }

    public void QuestCompleted()
    {
        AddToJournal.questDone = true;
        PlayerPrefs.SetInt("questDoneMystery2", 1);

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
