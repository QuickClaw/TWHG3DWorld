using UnityEngine;
using TMPro;

public class HeartWithinStars : MonoBehaviour
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
    public TMP_Text starsCount;
    public GameObject starSetActive;

    public TMP_Text expGain, starGain;
    public Animation expGainAnim, starGainAnim;

    private void Awake()
    {
        questAudioSource.clip = questCompletedSound;
        starSetActive.SetActive(false);

        if (PlayerPrefs.HasKey("starSouthWestTextBool"))
        {
            Player.starCount = PlayerPrefs.GetInt("starSouthWest");
            starsCount.text = Player.starCount.ToString() + "/4";
            starSetActive.SetActive(true);
        }

        if (PlayerPrefs.HasKey("questDoneHeartWithinStars"))
        {
            AddToJournal.questDone = true;

            Sign.done.SetActive(true);
            Sign.doneIcon.SetActive(true);
            Sign.signIcon.SetActive(false);

            Sign.doneIconMinimap.SetActive(true);
            Sign.signIconMinimap.SetActive(false);

            AddToJournal.questTakenIcon.SetActive(false);
            AddToJournal.questTakenIconMinimap.SetActive(false);

            starSetActive.SetActive(false);
        }
    }

    private void Update()
    {
        if (Player.starCount == 4)
        {
            starSetActive.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Menzile girdiðin an objeyi alýrsýn
        if (other.tag is "Player")
        {
            if (Player.starCount == 3)
            {
                QuestCompleted();

                questTextParticle.Play();
                questAudioSource.Play();

                Sign.done.SetActive(true);
                Sign.doneIcon.SetActive(true);
                Sign.signIcon.SetActive(false);

                PlayExpStarTextAnim();
            }

            pickUpAudioSource.Play();

            Player.starCount++;
            PlayerPrefs.SetInt("starSouthWest", Player.starCount);
            PlayerPrefs.SetInt("starSouthWestTextBool", 1); // Text active için bool görevi görür

            starsCount.text = Player.starCount.ToString() + "/4";
            starSetActive.SetActive(true);
        }
    }

    public void QuestCompleted()
    {
        AddToJournal.questDone = true;
        PlayerPrefs.SetInt("questDoneHeartWithinStars", 1);

        Player.questTheForest++;
        PlayerPrefs.SetInt("completedQuestsTheForest", Player.questTheForest);

        Player.experience += Sign.exp;

        Player.star += Sign.star;
        PlayerPrefs.SetInt("playerStar", Player.star);

        StarPickup.txtStarCount.text = Player.star.ToString();
        PauseMenu.questCount.text = Player.questTheForest.ToString() + "/" + AddToJournal.questNames.Count;

        if (AddToJournal.questTaken)
        {
            AddToJournal.questNames[AddToJournal.numberOfQuest - 1].text = Sign.questTitle + "\n" + "• " + Sign.questDesc + "\n" + "<color=orange>Exp: " + Sign.exp.ToString() + "</color>" + "    " + "<color=yellow>Star: " + Sign.star.ToString() + "</color>" + "\n" + "<color=green>QUEST COMPLETED</color>";
        }

        AddToJournal.questTakenIcon.SetActive(false);
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
