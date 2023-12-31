using UnityEngine;
using TMPro;

public class TheMysteryOfTheMountain1 : MonoBehaviour
{
    // Class al�nacak objeye atanmal�

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

    public TMP_Text starsOnTheLeftCount;
    public GameObject txtStarsOnTheLeftSetActive;

    public GameObject mountainExitCollider;

    public TMP_Text expGain, starGain;
    public Animation expGainAnim, starGainAnim;

    private void Awake()
    {
        Player.mountainStarsLeft = PlayerPrefs.GetInt("mountainStarsLeft");

        questAudioSource.clip = questCompletedSound;
        txtStarsOnTheLeftSetActive.SetActive(false);

        if (PlayerPrefs.HasKey("mountainStarsLeftTextBool"))
        {
            starsOnTheLeftCount.text = Player.mountainStarsLeft.ToString() + "/5";
            txtStarsOnTheLeftSetActive.SetActive(true);
        }

        if (PlayerPrefs.HasKey("questDoneMystery1"))
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
        if (Player.mountainStarsLeft == 5)
        {
            txtStarsOnTheLeftSetActive.SetActive(false);
        }

        if (Player.mountainStarsRight == 5 && Player.mountainStarsLeft == 5)
        {
            mountainExitCollider.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Menzile girdi�in an objeyi al�rs�n
        if (other.tag is "Player")
        {
            if (Player.mountainStarsLeft == 4)
            {
                QuestCompleted();

                questTextParticle.Play();
                questAudioSource.Play();

                Sign.done.SetActive(true);
                Sign.doneIcon.SetActive(true);
                Sign.signIcon.SetActive(false);

                Sign.doneIconMinimap.SetActive(true);
                Sign.signIconMinimap.SetActive(false);

                txtStarsOnTheLeftSetActive.SetActive(false);

                PlayExpStarTextAnim();
            }

            pickUpAudioSource.Play();

            Player.mountainStarsLeft++;
            PlayerPrefs.SetInt("mountainStarsLeft", Player.mountainStarsLeft);
            PlayerPrefs.SetInt("mountainStarsLeftTextBool", 1); // Text active i�in bool g�revi g�r�r

            starsOnTheLeftCount.text = Player.mountainStarsLeft.ToString() + "/5";
            txtStarsOnTheLeftSetActive.SetActive(true);
        }
    }

    public void QuestCompleted()
    {
        AddToJournal.questDone = true;
        PlayerPrefs.SetInt("questDoneMystery1", 1);

        Player.questSnowy++;
        PlayerPrefs.SetInt("completedQuestsSnowy", Player.questSnowy);

        Player.experience += Sign.exp;

        Player.star += Sign.star;
        PlayerPrefs.SetInt("playerStar", Player.star);

        StarPickup.txtStarCount.text = Player.star.ToString();
        PauseMenu.questCount.text = Player.questSnowy.ToString() + "/" + AddToJournal.questNames.Count;

        if (AddToJournal.questTaken)
        {
            AddToJournal.questNames[AddToJournal.numberOfQuest - 1].text = Sign.questTitle + "\n" + "� " + Sign.questDesc + "\n" + "<color=orange>Exp: " + Sign.exp.ToString() + "</color>" + "    " + "<color=yellow>Star: " + Sign.star.ToString() + "</color>" + "\n" + "<color=green>QUEST COMPLETED</color>";
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
