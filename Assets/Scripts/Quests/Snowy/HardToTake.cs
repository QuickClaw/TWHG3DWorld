using UnityEngine;
using TMPro;

public class HardToTake : MonoBehaviour
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
    public TMP_Text flagCount;
    public GameObject txtFlagSetActive;

    public TMP_Text expGain, starGain;
    public Animation expGainAnim, starGainAnim;

    public Vector3 posFlag;

    private void Awake()
    {
        Player.flagCount = PlayerPrefs.GetInt("flags");

        posFlag = transform.position;

        questAudioSource.clip = questCompletedSound;
        txtFlagSetActive.SetActive(false);

        if (PlayerPrefs.HasKey("flagTextBool"))
        {
            flagCount.text = Player.flagCount.ToString() + "/5";
            txtFlagSetActive.SetActive(true);
        }

        if (PlayerPrefs.HasKey("questHardToTake"))
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

            txtFlagSetActive.SetActive(false);
        }

        if (PlayerPrefs.HasKey("PosFlagX" + gameObject.name) == true)
        {
            float x = PlayerPrefs.GetFloat("PosFlagX" + gameObject.name);
            float y = PlayerPrefs.GetFloat("PosFlagY" + gameObject.name);
            float z = PlayerPrefs.GetFloat("PosFlagZ" + gameObject.name);

            transform.position = new Vector3(x, y, z);
        }
        else
            transform.position = posFlag;
    }

    private void Update()
    {
        if (Player.flagCount == 5)
        {
            txtFlagSetActive.SetActive(false);
        }
    }    

    private void OnTriggerEnter(Collider other)
    {
        // Menzile girdiðin an objeyi alýrsýn
        if (other.tag is "Player")
        {
            if (Player.flagCount == 4)
            {
                QuestCompleted();

                questTextParticle.Play();
                questAudioSource.Play();

                Sign.done.SetActive(true);
                Sign.doneIcon.SetActive(true);
                Sign.signIcon.SetActive(false);

                Sign.doneIconMinimap.SetActive(true);
                Sign.signIconMinimap.SetActive(false);

                PlayExpStarTextAnim();
            }

            transform.position = new Vector3(0, 5000, 0);
            PlayerPrefs.SetFloat("PosFlagX" + gameObject.name, transform.position.x);
            PlayerPrefs.SetFloat("PosFlagY" + gameObject.name, transform.position.y);
            PlayerPrefs.SetFloat("PosFlagZ" + gameObject.name, transform.position.z);

            pickUpAudioSource.Play();

            Player.flagCount++;
            PlayerPrefs.SetInt("flags", Player.flagCount);
            PlayerPrefs.SetInt("flagTextBool", 1); // Text active için bool görevi görür

            flagCount.text = Player.flagCount.ToString() + "/5";
            txtFlagSetActive.SetActive(true);
        }
    }

    public void QuestCompleted()
    {
        AddToJournal.questDone = true;
        PlayerPrefs.SetInt("questHardToTake", 1);

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
