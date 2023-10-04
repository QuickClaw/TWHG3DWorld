using UnityEngine;
using TMPro;

public class Boxes : MonoBehaviour
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
    public TMP_Text boxesCount;
    public GameObject boxesSetActive;

    public TMP_Text expGain, starGain;
    public Animation expGainAnim, starGainAnim;

    public Vector3 posBox;

    private void Awake()
    {
        posBox = transform.position;

        questAudioSource.clip = questCompletedSound;
        boxesSetActive.SetActive(false);

        if (PlayerPrefs.HasKey("boxesTextBool"))
        {
            Player.boxCount = PlayerPrefs.GetInt("boxes");
            boxesCount.text = Player.boxCount.ToString() + "/5";
            boxesSetActive.SetActive(true);
        }

        if (PlayerPrefs.HasKey("questDoneBoxes"))
        {
            AddToJournal.questDone = true;

            Sign.done.SetActive(true);
            Sign.doneIcon.SetActive(true);
            Sign.signIcon.SetActive(false);

            Sign.doneIconMinimap.SetActive(true);
            Sign.signIconMinimap.SetActive(false);

            AddToJournal.questTakenIcon.SetActive(false);
            AddToJournal.questTakenIconMinimap.SetActive(false);

            boxesSetActive.SetActive(false);
        }

        if (PlayerPrefs.HasKey("PosBoxX" + gameObject.name))
        {
            float x = PlayerPrefs.GetFloat("PosBoxX" + gameObject.name);
            float y = PlayerPrefs.GetFloat("PosBoxY" + gameObject.name);
            float z = PlayerPrefs.GetFloat("PosBoxZ" + gameObject.name);

            transform.position = new Vector3(x, y, z);
        }
    }

    private void Update()
    {
        if (Player.boxCount == 5)
        {
            boxesSetActive.SetActive(false);
        }      
    }

    private void OnTriggerEnter(Collider other)
    {
        // Menzile girdiðin an objeyi alýrsýn
        if (other.tag is "Player")
        {
            if (Player.boxCount == 4)
            {
                QuestCompleted();

                questTextParticle.Play();
                questAudioSource.Play();

                Sign.done.SetActive(true);
                Sign.doneIcon.SetActive(true);
                Sign.signIcon.SetActive(false);
                boxesSetActive.SetActive(false);

                PlayExpStarTextAnim();
            }

            transform.position = new Vector3(0, 5000, 0);
            PlayerPrefs.SetFloat("PosBoxX" + gameObject.name, transform.position.x);
            PlayerPrefs.SetFloat("PosBoxY" + gameObject.name, transform.position.y);
            PlayerPrefs.SetFloat("PosBoxZ" + gameObject.name, transform.position.z);

            pickUpAudioSource.Play();

            Player.boxCount++;
            PlayerPrefs.SetInt("boxes", Player.boxCount);
            PlayerPrefs.SetInt("boxesTextBool", 1); // Text active için bool görevi görür

            boxesCount.text = Player.boxCount.ToString() + "/5";
            boxesSetActive.SetActive(true);
        }
    }

    public void QuestCompleted()
    {
        AddToJournal.questDone = true;
        PlayerPrefs.SetInt("questDoneBoxes", 1);

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
