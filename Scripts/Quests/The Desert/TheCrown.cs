using UnityEngine;
using TMPro;
using Steamworks;

public class TheCrown : MonoBehaviour
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
    public bool questDone;

    public TMP_Text expGain, starGain;
    public Animation expGainAnim, starGainAnim;

    Vector3 posCrown;

    public GameObject playerCrown;
    public ParticleSystem crownEffect;
    public AudioSource pickUpAudioSource;

    private void Awake()
    {
        posCrown = transform.position;

        questAudioSource.clip = questCompletedSound;

        if (PlayerPrefs.HasKey("questTheCrown"))
        {
            AddToJournal.questDone = true;
            playerCrown.SetActive(true);

            Sign.done.SetActive(true);
            Sign.doneIcon.SetActive(true);
            Sign.signIcon.SetActive(false);

            Sign.doneIconMinimap.SetActive(true);
            Sign.signIconMinimap.SetActive(false);

            AddToJournal.questTakenIcon.SetActive(false);
            AddToJournal.questTakenIconMinimap.SetActive(false);
            AddToJournal.journalCollider.enabled = false;
        }

        if (PlayerPrefs.HasKey("CrownPosX" + gameObject.name) == true)
        {
            float x = PlayerPrefs.GetFloat("CrownPosX" + gameObject.name);
            float y = PlayerPrefs.GetFloat("CrownPosY" + gameObject.name);
            float z = PlayerPrefs.GetFloat("CrownPosZ" + gameObject.name);

            transform.position = new Vector3(x, y, z);
        }
        else
            transform.position = posCrown;
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

            SteamUserStats.SetAchievement("achievement_10");
            SteamUserStats.StoreStats();
        }

        transform.position = new Vector3(0, 5000, 0);
        PlayerPrefs.SetFloat("CrownPosX" + gameObject.name, transform.position.x);
        PlayerPrefs.SetFloat("CrownPosY" + gameObject.name, transform.position.y);
        PlayerPrefs.SetFloat("CrownPosZ" + gameObject.name, transform.position.z);

        Player.platformQuests++;
        PlayerPrefs.SetInt("platformQuests", Player.platformQuests);

        crownEffect.Play();
        playerCrown.SetActive(true);
        pickUpAudioSource.Play();
    }

    public void QuestCompleted()
    {
        AddToJournal.questDone = true;
        PlayerPrefs.SetInt("questTheCrown", 1);

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
