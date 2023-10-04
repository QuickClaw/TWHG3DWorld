using UnityEngine;
using TMPro;

public class JustBeginning : MonoBehaviour
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
    public TMP_Text pinkStarsCount;
    public GameObject pinkStarSetActive;

    public TMP_Text expGain, starGain;
    public Animation expGainAnim, starGainAnim;


    private void Awake()
    {
        questAudioSource.clip = questCompletedSound;
        pinkStarSetActive.SetActive(false);

        if (PlayerPrefs.HasKey("pinkStarTextBool"))
        {
            Player.pinkStars = PlayerPrefs.GetInt("pinkStar");
            pinkStarsCount.text = Player.pinkStars.ToString() + "/10";
            pinkStarSetActive.SetActive(true);
        }

        if (PlayerPrefs.HasKey("questDoneJustBeginning"))
        {
            AddToJournal.questDone = true;

            Sign.done.SetActive(true);
            Sign.doneIcon.SetActive(true);
            Sign.signIcon.SetActive(false);

            Sign.doneIconMinimap.SetActive(true);
            Sign.signIconMinimap.SetActive(false);

            AddToJournal.questTakenIcon.SetActive(false);
            AddToJournal.questTakenIconMinimap.SetActive(false);

            pinkStarSetActive.SetActive(false);
        }
    }

    private void Update()
    {
        if (Player.pinkStars == 10)
            pinkStarSetActive.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Menzile girdiðin an objeyi alýrsýn
        if (other.tag is "Player")
        {
            if (Player.pinkStars == 9)
            {
                QuestCompleted();

                questTextParticle.Play();
                questAudioSource.Play();

                Sign.done.SetActive(true);
                Sign.doneIcon.SetActive(true);
                Sign.signIcon.SetActive(false);

                PlayExpStarTextAnim();

                pinkStarSetActive.SetActive(false);
            }

            pickUpAudioSource.Play();

            Player.pinkStars++;
            PlayerPrefs.SetInt("pinkStar", Player.pinkStars);
            PlayerPrefs.SetInt("pinkStarTextBool", 1); // Text active için bool görevi görür

            pinkStarsCount.text = Player.pinkStars.ToString() + "/10";
            pinkStarSetActive.SetActive(true);
        }
    }

    public void QuestCompleted()
    {
        AddToJournal.questDone = true;
        PlayerPrefs.SetInt("questDoneJustBeginning", 1);

        Player.questInvisible++;
        PlayerPrefs.SetInt("completedQuestsInvisible", Player.questInvisible);

        Player.experience += Sign.exp;

        Player.star += Sign.star;
        PlayerPrefs.SetInt("playerStar", Player.star);

        StarPickup.txtStarCount.text = Player.star.ToString();
        PauseMenu.questCount.text = Player.questInvisible.ToString() + "/" + AddToJournal.questNames.Count;

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
