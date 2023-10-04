using UnityEngine;
using TMPro;

public class LonelySnowman : MonoBehaviour
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

    public TMP_Text txtPresentsCount;
    public GameObject presentsSetActive;

    public TMP_Text expGain, starGain;
    public Animation expGainAnim, starGainAnim;

    public Vector3 posPresent;

    private void Awake()
    {
        Player.presents = PlayerPrefs.GetInt("presents");

        posPresent = transform.position;

        questAudioSource.clip = questCompletedSound;
        presentsSetActive.SetActive(false);

        if (PlayerPrefs.HasKey("presentTextBool"))
        {
            txtPresentsCount.text = Player.presents.ToString() + "/4";
            presentsSetActive.SetActive(true);
        }

        if (PlayerPrefs.HasKey("questLonelySnowman"))
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

            presentsSetActive.SetActive(false);
        }

        if (PlayerPrefs.HasKey("PosPresentX" + gameObject.name) == true)
        {
            float x = PlayerPrefs.GetFloat("PosPresentX" + gameObject.name);
            float y = PlayerPrefs.GetFloat("PosPresentY" + gameObject.name);
            float z = PlayerPrefs.GetFloat("PosPresentZ" + gameObject.name);

            transform.position = new Vector3(x, y, z);
        }
        else
            transform.position = posPresent;
    }

    private void Update()
    {
        if (Player.presents == 4)
        {
            presentsSetActive.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Menzile girdiðin an objeyi alýrsýn

        if (other.tag is "Player")
        {
            if (Player.presents == 3)
            {
                QuestCompleted();

                questTextParticle.Play();
                questAudioSource.Play();

                Sign.done.SetActive(true);
                Sign.doneIcon.SetActive(true);
                Sign.signIcon.SetActive(false);

                Sign.doneIconMinimap.SetActive(true);
                Sign.signIconMinimap.SetActive(false);

                presentsSetActive.SetActive(false);

                PlayExpStarTextAnim();
            }

            transform.position = new Vector3(0, 5000, 0);
            PlayerPrefs.SetFloat("PosPresentX" + gameObject.name, transform.position.x);
            PlayerPrefs.SetFloat("PosPresentY" + gameObject.name, transform.position.y);
            PlayerPrefs.SetFloat("PosPresentZ" + gameObject.name, transform.position.z);

            pickUpAudioSource.Play();

            Player.presents++;
            PlayerPrefs.SetInt("presents", Player.presents);
            PlayerPrefs.SetInt("presentTextBool", 1); // Text active için bool görevi görür

            txtPresentsCount.text = Player.presents.ToString() + "/4";
            presentsSetActive.SetActive(true);
        }
    }

    public void QuestCompleted()
    {
        AddToJournal.questDone = true;
        PlayerPrefs.SetInt("questLonelySnowman", 1);

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
