using UnityEngine;
using TMPro;

public class TheBook : MonoBehaviour
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

    public TMP_Text expGain, starGain;
    public Animation expGainAnim, starGainAnim;

    public Vector3 posBook;

    private void Awake()
    {
        posBook = transform.position;

        questAudioSource.clip = questCompletedSound;

        if (PlayerPrefs.HasKey("questDoneTheBook"))
        {
            AddToJournal.questDone = true;         

            Sign.done.SetActive(true);
            Sign.doneIcon.SetActive(true);
            Sign.signIcon.SetActive(false);

            AddToJournal.questTakenIcon.SetActive(false);
            AddToJournal.questTakenIconMinimap.SetActive(false);

            AddToJournal.journalCollider.enabled = false;
        }

        if (PlayerPrefs.HasKey("BookPosX" + gameObject.name) == true)
        {
            float x = PlayerPrefs.GetFloat("BookPosX" + gameObject.name);
            float y = PlayerPrefs.GetFloat("BookPosY" + gameObject.name);
            float z = PlayerPrefs.GetFloat("BookPosZ" + gameObject.name);

            transform.position = new Vector3(x, y, z);
        }
        else
            transform.position = posBook;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Menzile girdiðin an objeyi alýrsýn
        if (other.tag is "Player")
        {
            QuestCompleted();

            transform.position = new Vector3(0, 5000, 0);
            PlayerPrefs.SetFloat("BookPosX" + gameObject.name, transform.position.x);
            PlayerPrefs.SetFloat("BookPosY" + gameObject.name, transform.position.y);
            PlayerPrefs.SetFloat("BookPosZ" + gameObject.name, transform.position.z);

            pickUpAudioSource.Play();

            Sign.done.SetActive(true);
            Sign.doneIcon.SetActive(true);
            Sign.signIcon.SetActive(false);

            Sign.doneIconMinimap.SetActive(true);
            Sign.signIconMinimap.SetActive(false);

            questTextParticle.Play();
            questAudioSource.Play();

            PlayExpStarTextAnim();
        }
    }

    public void QuestCompleted()
    {
        AddToJournal.questDone = true;
        PlayerPrefs.SetInt("questDoneTheBook", 1);

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
