using UnityEngine;
using TMPro;

public class ColdEnemy : MonoBehaviour
{
    // Class alýnacak objeye atanmalý

    public Player Player;
    public Sign Sign;
    public Experience Experience;
    public Collider chestAnimCollider;
    public AddToJournal AddToJournal;
    public PauseMenu PauseMenu;
    public StarPickup StarPickup;

    public AudioSource questAudioSource;
    public AudioClip questCompletedSound;

    public ParticleSystem questTextParticle;
    public AudioSource chestAudioSource;
    public bool questDone;

    public TMP_Text expGain, starGain;
    public Animation expGainAnim, starGainAnim;
    public Animation chestAnim;

    private void Awake()
    {
        questAudioSource.clip = questCompletedSound;

        if (PlayerPrefs.HasKey("questDoneColdEnemy"))
        {
            AddToJournal.questDone = true;

            chestAnim.Play();
            chestAnimCollider.enabled = false;

            Sign.done.SetActive(true);
            Sign.doneIcon.SetActive(true);
            Sign.signIcon.SetActive(false);

            Sign.doneIconMinimap.SetActive(true);
            Sign.signIconMinimap.SetActive(false);

            AddToJournal.questTakenIcon.SetActive(false);
            AddToJournal.questTakenIconMinimap.SetActive(false);
            AddToJournal.journalCollider.enabled = false;

            AddToJournal.journalCollider.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Menzile girdiðin an sandýðý açarsýn
        if (other.tag is "Player")
        {
            QuestCompleted();

            chestAudioSource.Play();

            Sign.done.SetActive(true);
            Sign.doneIcon.SetActive(true);
            Sign.signIcon.SetActive(false);

            Sign.doneIconMinimap.SetActive(true);
            Sign.signIconMinimap.SetActive(false);

            questTextParticle.Play();
            questAudioSource.Play();
            chestAnim.Play();
            chestAnimCollider.enabled = false;

            PlayExpStarTextAnim();
        }
    }

    public void QuestCompleted()
    {
        AddToJournal.questDone = true;
        PlayerPrefs.SetInt("questDoneColdEnemy", 1);

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
