using UnityEngine;
using TMPro;

public class TheBigMud : MonoBehaviour
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

    public TMP_Text mudBones;
    public GameObject txtMudBonesSetActive;
    public GameObject bones;

    public TMP_Text expGain, starGain;
    public Animation expGainAnim, starGainAnim;

    Vector3 posMudBone;

    private void Awake()
    {
        posMudBone = transform.position;

        questAudioSource.clip = questCompletedSound;
        txtMudBonesSetActive.SetActive(false);

        if (PlayerPrefs.HasKey("theBigMudTextBool"))
        {
            Player.mudBones = PlayerPrefs.GetInt("mudBones");
            mudBones.text = Player.mudBones.ToString() + "/6";
            txtMudBonesSetActive.SetActive(true);
        }

        if (PlayerPrefs.HasKey("questDoneMudBones"))
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

            txtMudBonesSetActive.SetActive(false);
        }

        if (PlayerPrefs.HasKey("MudBonesPosX" + gameObject.name) == true)
        {
            float x = PlayerPrefs.GetFloat("MudBonesPosX" + gameObject.name);
            float y = PlayerPrefs.GetFloat("MudBonesPosY" + gameObject.name);
            float z = PlayerPrefs.GetFloat("MudBonesPosZ" + gameObject.name);

            transform.position = new Vector3(x, y, z);
        }
        else
            transform.position = posMudBone;
    }

    private void Update()
    {
        if (Player.mudBones == 6)
            txtMudBonesSetActive.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Menzile girdiðin an objeyi alýrsýn
        if (other.tag is "Player")
        {
            if (Player.mudBones == 5)
            {
                QuestCompleted();

                questTextParticle.Play();
                questAudioSource.Play();

                Sign.done.SetActive(true);
                Sign.doneIcon.SetActive(true);
                Sign.signIcon.SetActive(false);

                Sign.doneIconMinimap.SetActive(true);
                Sign.signIconMinimap.SetActive(false);

                txtMudBonesSetActive.SetActive(false);

                PlayExpStarTextAnim();
            }

            transform.position = new Vector3(0, 5000, 0);
            PlayerPrefs.SetFloat("MudBonesPosX" + gameObject.name, transform.position.x);
            PlayerPrefs.SetFloat("MudBonesPosY" + gameObject.name, transform.position.y);
            PlayerPrefs.SetFloat("MudBonesPosZ" + gameObject.name, transform.position.z);

            pickUpAudioSource.Play();

            Player.mudBones++;
            PlayerPrefs.SetInt("mudBones", Player.mudBones);
            PlayerPrefs.SetInt("theBigMudTextBool", 1); // Text active için bool görevi görür

            mudBones.text = Player.mudBones.ToString() + "/6";
            txtMudBonesSetActive.SetActive(true);
        }
    }

    public void QuestCompleted()
    {
        AddToJournal.questDone = true;
        PlayerPrefs.SetInt("questDoneMudBones", 1);

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
