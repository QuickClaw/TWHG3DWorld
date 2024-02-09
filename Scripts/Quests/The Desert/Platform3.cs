using UnityEngine;
using TMPro;

public class Platform3 : MonoBehaviour
{
    // Class alýnacak objeye atanmalý

    public Player Player;
    public Sign Sign;
    public Experience Experience;
    public AddToJournal AddToJournal;
    public Movement Movement;
    public PauseMenu PauseMenu;

    public AudioSource questAudioSource;
    public AudioClip questCompletedSound;

    public ParticleSystem questTextParticle;
    public bool questDone;

    public TMP_Text expGain, starGain;
    public Animation expGainAnim, starGainAnim;

    public Collider platform3Collider;
    public Transform player;

    public AudioSource tpAudioSource;
    public Animation platform3Road;
    public ParticleSystem platform3Effect;

    private void Awake()
    {
        questAudioSource.clip = questCompletedSound;

        if (PlayerPrefs.HasKey("questPlatform3"))
        {
            AddToJournal.questDone = true;
            platform3Collider.enabled = false;
            platform3Road.Play();

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag is "Player")
        {
            QuestCompleted();

            Sign.done.SetActive(true);
            Sign.doneIcon.SetActive(true);
            Sign.signIcon.SetActive(false);

            Sign.doneIconMinimap.SetActive(true);
            Sign.signIconMinimap.SetActive(false);

            questTextParticle.Play();
            questAudioSource.Play();

            PlayExpStarTextAnim();

            Player.platformQuests++;
            PlayerPrefs.SetInt("platformQuests", Player.platformQuests);

            platform3Road.Play();

            Movement.enabled = false;
            Invoke("TeleportPlatform4", 1.5f);
        }
    }

    public void QuestCompleted()
    {
        AddToJournal.questDone = true;
        PlayerPrefs.SetInt("questPlatform3", 1);

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

    public void TeleportPlatform4()
    {
        player.transform.position = new Vector3(-118.313f, 67.997f, -45.36101f);
        Movement.enabled = true;
        platform3Collider.enabled = false;
        tpAudioSource.Play();
        platform3Effect.Play();
    }
}
