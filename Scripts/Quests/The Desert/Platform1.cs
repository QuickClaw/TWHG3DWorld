using UnityEngine;
using TMPro;

public class Platform1 : MonoBehaviour
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

    public Collider platform1Collider;
    public Transform player;

    public AudioSource tpAudioSource;
    public Animation platform1Road;
    public ParticleSystem platform1Effect;

    /// <summary>
    /// HER PLATFORM GÖREVÝ SONUNDA DÝÐER PLATFORMA YOL AÇILACAK, ANÝMASYON ÝLE YAPILACAK.
    /// </summary>

    private void Awake()
    {
        questAudioSource.clip = questCompletedSound;

        if (PlayerPrefs.HasKey("questPlatform1"))
        {
            AddToJournal.questDone = true;
            platform1Collider.enabled = false;
            platform1Road.Play();

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

            platform1Road.Play();

            Movement.enabled = false;
            Invoke("TeleportPlatform2", 1.5f);          
        }
    }

    public void QuestCompleted()
    {
        AddToJournal.questDone = true;
        PlayerPrefs.SetInt("questPlatform1", 1);

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

    public void TeleportPlatform2()
    {       
        player.transform.position = new Vector3(-142.92f, 60.313f, 31.668f);
        Movement.enabled = true;
        platform1Collider.enabled = false;
        tpAudioSource.Play();
        platform1Effect.Play();
    }
}
