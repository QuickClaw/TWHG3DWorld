using UnityEngine;
using TMPro;

public class StarsOfArena : MonoBehaviour
{
    // Class alýnacak objeye atanmalý

    public Player Player;
    public Sign Sign;
    public Experience Experience;
    public AddToJournal AddToJournal;
    public PauseMenu PauseMenu;

    public AudioSource questAudioSource;
    public AudioClip questCompletedSound;

    public ParticleSystem questTextParticle;
    public AudioSource pickUpAudioSource;
    public bool questDone;

    public TMP_Text starsOfArenaCount;
    public GameObject txtStarsOfArenaSetActive;

    public TMP_Text expGain, starGain;
    public Animation expGainAnim, starGainAnim;

    public Transform playerTransform;

    private void Awake()
    {
        questAudioSource.clip = questCompletedSound;
        txtStarsOfArenaSetActive.SetActive(false);

        if (PlayerPrefs.HasKey("starsOfArenaTextBool"))
        {
            Player.starsOfArena = PlayerPrefs.GetInt("starsOfArena");
            starsOfArenaCount.text = Player.starsOfArena.ToString() + "/9";
            txtStarsOfArenaSetActive.SetActive(true);
        }

        if (PlayerPrefs.HasKey("questDoneStarsOfArena"))
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

            txtStarsOfArenaSetActive.SetActive(false);
        }
    }

    private void Update()
    {
        if (Player.starsOfArena == 9)
        {
            txtStarsOfArenaSetActive.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Menzile girdiðin an objeyi alýrsýn
        if (other.tag is "Player")
        {
            if (Player.starsOfArena == 8)
            {
                QuestCompleted();

                questTextParticle.Play();
                questAudioSource.Play();

                Sign.done.SetActive(true);
                Sign.doneIcon.SetActive(true);
                Sign.signIcon.SetActive(false);

                Sign.doneIconMinimap.SetActive(true);
                Sign.signIconMinimap.SetActive(false);

                txtStarsOfArenaSetActive.SetActive(false);

                playerTransform.position = new Vector3(-205.2f, 14.335f, -99.29f);

                PlayExpStarTextAnim();
            }

            pickUpAudioSource.Play();

            Player.starsOfArena++;
            PlayerPrefs.SetInt("starsOfArena", Player.starsOfArena);
            PlayerPrefs.SetInt("starsOfArenaTextBool", 1); // Text active için bool görevi görür

            starsOfArenaCount.text = Player.starsOfArena.ToString() + "/9";
            txtStarsOfArenaSetActive.SetActive(true);
        }
    }

    public void QuestCompleted()
    {
        AddToJournal.questDone = true;
        PlayerPrefs.SetInt("questDoneStarsOfArena", 1);

        Player.questTheDesert++;
        PlayerPrefs.SetInt("completedQuestsTheDesert", Player.questTheDesert);

        Player.experience += Sign.exp;

        Player.star += Sign.star;
        PlayerPrefs.SetInt("playerStar", Player.star);

        PauseMenu.questCount.text = Player.questTheDesert.ToString() + "/" + AddToJournal.questNames.Count;

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
