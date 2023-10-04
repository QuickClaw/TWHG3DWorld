using UnityEngine;
using TMPro;

public class Darkness : MonoBehaviour
{
    // Class alýnacak objeye atanmalý

    public Player Player;
    public Sign Sign;
    public Experience Experience;
    public AddToJournal AddToJournal;
    public Obstacle Obstacle;
    public Maze Maze;
    public PauseMenu PauseMenu;
    public StarPickup StarPickup;

    public AudioSource questAudioSource;
    public AudioClip questCompletedSound;

    public ParticleSystem questTextParticle;
    public AudioSource pickUpAudioSource;
    public bool questDone;

    public TMP_Text pumpkinCount;
    public GameObject txtPumpkinSetActive;

    public TMP_Text expGain, starGain;
    public Animation expGainAnim, starGainAnim;

    public GameObject mazeCollider;
    public GameObject theBigLastStar;
    public GameObject txtTheBigLastStar;

    public Vector3 pumpkinLocation;

    private void Awake()
    {
        pumpkinLocation = transform.position;

        questAudioSource.clip = questCompletedSound;
        txtPumpkinSetActive.SetActive(false);

        if (PlayerPrefs.HasKey("pumpkinTextBool"))
        {
            Player.pumpkin = PlayerPrefs.GetInt("pumpkin");
            pumpkinCount.text = Player.pumpkin.ToString() + "/4";
            txtPumpkinSetActive.SetActive(true);
        }

        if (PlayerPrefs.HasKey("questDoneDarkness"))
        {
            AddToJournal.questDone = true;

            Sign.done.SetActive(true);
            Sign.doneIcon.SetActive(true);
            Sign.signIcon.SetActive(false);

            AddToJournal.questTakenIcon.SetActive(false);
            AddToJournal.questTakenIconMinimap.SetActive(false);

            AddToJournal.journalCollider.enabled = false;
        }
    }

    private void Update()
    {
        if (Player.pumpkin == 4)
        {
            txtPumpkinSetActive.SetActive(false);
        }

        // Maze içinde ölürsek starlar eski yerlerine gelir ve 0 lanýr
        if (Maze.inMaze && Obstacle.isDead)
        {
            transform.position = pumpkinLocation;
            pumpkinCount.text = "0/4";

            if (Player.pumpkin <= -1)
                Player.pumpkin = 0;

            PlayerPrefs.SetInt("pumpkin", Player.pumpkin);

            Player.pumpkin = 0;
            PlayerPrefs.SetInt("pumpkin", Player.pumpkin);

            PlayerPrefs.DeleteKey("PosPumpkinX" + gameObject.name);
            PlayerPrefs.DeleteKey("PosPumpkinY" + gameObject.name);
            PlayerPrefs.DeleteKey("PosPumpkinZ" + gameObject.name);

            if (PlayerPrefs.HasKey("PosPumpkinX") == false)
            {
                transform.position = pumpkinLocation;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Menzile girdiðin an objeyi alýrsýn
        if (other.tag is "Player")
        {
            if (Player.pumpkin == 3)
            {
                QuestCompleted();

                questTextParticle.Play();
                questAudioSource.Play();

                Sign.done.SetActive(true);
                Sign.doneIcon.SetActive(true);
                Sign.signIcon.SetActive(false);

                Sign.doneIconMinimap.SetActive(true);
                Sign.signIconMinimap.SetActive(false);

                txtPumpkinSetActive.SetActive(false);
                mazeCollider.SetActive(false);

                Maze.inMaze = false;
                Maze.txtInMaze.gameObject.SetActive(false);
                theBigLastStar.SetActive(true);
                txtTheBigLastStar.SetActive(true);

                PlayExpStarTextAnim();
            }

            transform.position = new Vector3(0, 5000, 0);
            PlayerPrefs.SetFloat("PosPumpkinX" + gameObject.name, transform.position.x);
            PlayerPrefs.SetFloat("PosPumpkinY" + gameObject.name, transform.position.y);
            PlayerPrefs.SetFloat("PosPumpkinZ" + gameObject.name, transform.position.z);

            pickUpAudioSource.Play();

            Player.pumpkin++;
            PlayerPrefs.SetInt("pumpkin", Player.pumpkin);
            PlayerPrefs.SetInt("pumpkinTextBool", 1); // Text active için bool görevi görür

            pumpkinCount.text = Player.pumpkin.ToString() + "/4";
            txtPumpkinSetActive.SetActive(true);
        }
    }

    public void QuestCompleted()
    {
        AddToJournal.questDone = true;
        PlayerPrefs.SetInt("questDoneDarkness", 1);

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

        Player.experience += Sign.exp;

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
