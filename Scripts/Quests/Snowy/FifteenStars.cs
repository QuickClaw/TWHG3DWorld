using UnityEngine;
using TMPro;

public class FifteenStars : MonoBehaviour
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
    public TMP_Text txtStarsMountainFootCount;

    public GameObject starsMountainFootSetActive;

    public TMP_Text expGain, starGain;
    public Animation expGainAnim, starGainAnim;

    private void Awake()
    {
        Player.starsFoothills = PlayerPrefs.GetInt("starFootHills");

        questAudioSource.clip = questCompletedSound;
        starsMountainFootSetActive.SetActive(false);

        if (PlayerPrefs.HasKey("starFootHillsTextBool"))
        {
            txtStarsMountainFootCount.text = Player.starsFoothills.ToString() + "/15";
            starsMountainFootSetActive.SetActive(true);
        }

        if (PlayerPrefs.HasKey("questDoneFifteenStars"))
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
        if (Player.starsFoothills == 15)
        {
            starsMountainFootSetActive.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Menzile girdiðin an objeyi alýrsýn
        if (other.tag is "Player")
        {
            if (Player.starsFoothills == 14)
            {
                QuestCompleted();

                questTextParticle.Play();
                questAudioSource.Play();

                Sign.done.SetActive(true);
                Sign.doneIcon.SetActive(true);
                Sign.signIcon.SetActive(false);

                Sign.doneIconMinimap.SetActive(true);
                Sign.signIconMinimap.SetActive(false);

                starsMountainFootSetActive.SetActive(false);

                PlayExpStarTextAnim();
            }

            pickUpAudioSource.Play();

            Player.starsFoothills++;
            PlayerPrefs.SetInt("starFootHills", Player.starsFoothills);
            PlayerPrefs.SetInt("starFootHillsTextBool", 1); // Text active için bool görevi görür

            txtStarsMountainFootCount.text = Player.starsFoothills.ToString() + "/15";
            starsMountainFootSetActive.SetActive(true);
        }
    }

    public void QuestCompleted()
    {
        AddToJournal.questDone = true;
        PlayerPrefs.SetInt("questDoneFifteenStars", 1);

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
