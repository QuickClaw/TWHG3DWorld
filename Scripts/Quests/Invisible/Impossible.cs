using UnityEngine;
using TMPro;
using Steamworks;

public class Impossible : MonoBehaviour
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

    public TMP_Text impossibleStarsCount;
    public GameObject impossibleStarSetActive;

    public TMP_Text expGain, starGain;
    public Animation expGainAnim, starGainAnim;


    private void Awake()
    {
        questAudioSource.clip = questCompletedSound;
        impossibleStarSetActive.SetActive(false);

        if (PlayerPrefs.HasKey("impossibleStarTextBool"))
        {
            Player.impossibleStars = PlayerPrefs.GetInt("impossibleStar");
            impossibleStarsCount.text = Player.impossibleStars.ToString() + "/16";
            impossibleStarSetActive.SetActive(true);
        }

        if (PlayerPrefs.HasKey("questDoneImpossible"))
        {
            AddToJournal.questDone = true;

            Sign.done.SetActive(true);
            Sign.doneIcon.SetActive(true);
            Sign.signIcon.SetActive(false);

            Sign.doneIconMinimap.SetActive(true);
            Sign.signIconMinimap.SetActive(false);

            AddToJournal.questTakenIcon.SetActive(false);
            AddToJournal.questTakenIconMinimap.SetActive(false);

            impossibleStarSetActive.SetActive(false);
        }
    }

    private void Update()
    {
        if (Player.impossibleStars == 16)
            impossibleStarSetActive.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Menzile girdiðin an objeyi alýrsýn
        if (other.tag is "Player")
        {
            if (Player.impossibleStars == 15)
            {
                QuestCompleted();

                questTextParticle.Play();
                questAudioSource.Play();

                Sign.done.SetActive(true);
                Sign.doneIcon.SetActive(true);
                Sign.signIcon.SetActive(false);

                PlayExpStarTextAnim();

                impossibleStarSetActive.SetActive(false);

                SteamUserStats.SetAchievement("achievement_19");
                SteamUserStats.StoreStats();
            }

            pickUpAudioSource.Play();

            Player.impossibleStars++;
            PlayerPrefs.SetInt("impossibleStar", Player.impossibleStars);
            PlayerPrefs.SetInt("impossibleStarTextBool", 1); // Text active için bool görevi görür

            impossibleStarsCount.text = Player.impossibleStars.ToString() + "/16";
            impossibleStarSetActive.SetActive(true);
        }
    }

    public void QuestCompleted()
    {
        AddToJournal.questDone = true;
        PlayerPrefs.SetInt("questDoneImpossible", 1);

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
