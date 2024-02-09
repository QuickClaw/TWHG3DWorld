using UnityEngine;
using TMPro;

public class TheLight : MonoBehaviour
{
    // Class alýnacak objeye atanmalý

    public Player Player;
    public Sign Sign;
    public Experience Experience;
    public AddToJournal AddToJournal;
    public OpenLight OpenLight;
    public Maze Maze;
    public Darkness Darkness;
    public TheBigLastStar TheBigLastStar;
    public PauseMenu PauseMenu;
    public StarPickup StarPickup;

    public AudioSource questAudioSource;
    public AudioClip questCompletedSound;

    public ParticleSystem questTextParticle;
    public AudioSource pickUpAudioSource;
    public bool questDone;

    public TMP_Text expGain, starGain;
    public Animation expGainAnim, starGainAnim;

    public GameObject playerLight;

    public GameObject[] Objects;
    public Follow[] Follow;
    public LightProtect[] LightProtect;

    public AudioSource horrorMusic;
    public Vector3 posLight;

    private void Awake()
    {
        posLight = transform.position;

        questAudioSource.clip = questCompletedSound;

        if (PlayerPrefs.HasKey("questDoneTheLight"))
        {
            AddToJournal.questDone = true;

            Sign.done.SetActive(true);
            Sign.doneIcon.SetActive(true);
            Sign.signIcon.SetActive(false);

            AddToJournal.questTakenIcon.SetActive(false);
            AddToJournal.questTakenIconMinimap.SetActive(false);

            AddToJournal.journalCollider.enabled = false;

            OpenLight.enabled = true;
            OpenLight.isLightOn = true;
            playerLight.SetActive(true);

            if (PlayerPrefs.HasKey("questDoneDarkness") == false)
            {
                for (int i = 0; i < Objects.Length; i++)
                    Objects[i].SetActive(true);

                for (int i = 0; i < Follow.Length; i++)
                    Follow[i].enabled = true;

                for (int i = 0; i < LightProtect.Length; i++)
                    LightProtect[i].enabled = true;

                horrorMusic.gameObject.SetActive(true);
                horrorMusic.Play();
            }

            if (PlayerPrefs.HasKey("questDoneDarkness") && PlayerPrefs.HasKey("questDoneTheBigLastStar") == false)
            {
                for (int i = 0; i < Objects.Length; i++)
                    Objects[i].SetActive(false);

                for (int i = 0; i < Follow.Length; i++)
                    Follow[i].enabled = true;

                for (int i = 0; i < LightProtect.Length; i++)
                    LightProtect[i].enabled = true;

                horrorMusic.gameObject.SetActive(true);
                horrorMusic.Play();

                Maze.inMaze = false;
                Maze.txtInMaze.gameObject.SetActive(false);

                Darkness.txtPumpkinSetActive.SetActive(false);
                Darkness.mazeCollider.SetActive(false);

                Darkness.theBigLastStar.SetActive(true);
                Darkness.txtTheBigLastStar.SetActive(true);
            }

            if (PlayerPrefs.HasKey("questDoneDarkness") && PlayerPrefs.HasKey("questDoneTheBigLastStar"))
            {
                for (int i = 0; i < Objects.Length; i++)
                    Objects[i].SetActive(false);

                for (int i = 0; i < Follow.Length; i++)
                    Follow[i].enabled = false;

                for (int i = 0; i < LightProtect.Length; i++)
                    LightProtect[i].enabled = false;

                horrorMusic.gameObject.SetActive(false);
            }
        }

        if (PlayerPrefs.HasKey("LightPosX" + gameObject.name))
        {
            float x = PlayerPrefs.GetFloat("LightPosX" + gameObject.name);
            float y = PlayerPrefs.GetFloat("LightPosY" + gameObject.name);
            float z = PlayerPrefs.GetFloat("LightPosZ" + gameObject.name);

            transform.position = new Vector3(x, y, z);
        }
        else
            transform.position = posLight;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Menzile girdiðin an objeyi alýrsýn
        if (other.tag is "Player")
        {
            QuestCompleted();

            for (int i = 0; i < Objects.Length; i++)
            {
                Objects[i].SetActive(true);
            }

            for (int i = 0; i < Follow.Length; i++)
            {
                Follow[i].enabled = true;
            }

            for (int i = 0; i < LightProtect.Length; i++)
            {
                LightProtect[i].enabled = true;
            }

            OpenLight.enabled = true;
            OpenLight.isLightOn = true;
            playerLight.SetActive(true);

            horrorMusic.gameObject.SetActive(true);
            horrorMusic.Play();

            transform.position = new Vector3(0, 5000, 0);
            PlayerPrefs.SetFloat("LightPosX" + gameObject.name, transform.position.x);
            PlayerPrefs.SetFloat("LightPosY" + gameObject.name, transform.position.y);
            PlayerPrefs.SetFloat("LightPosZ" + gameObject.name, transform.position.z);

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
        PlayerPrefs.SetInt("questDoneTheLight", 1);

        Player.questFoggy++;
        PlayerPrefs.SetInt("completedQuestsFoggy", Player.questFoggy);

        Player.experience += Sign.exp;

        Player.star += Sign.star;
        PlayerPrefs.SetInt("playerStar", Player.star);

        StarPickup.txtStarCount.text = Player.star.ToString();
        PauseMenu.questCount.text = Player.questFoggy.ToString() + "/" + AddToJournal.questNames.Count;

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
