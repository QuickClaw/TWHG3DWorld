using UnityEngine;
using TMPro;

public class RedMaze : MonoBehaviour
{
    // Class alýnacak objeye atanmalý

    public Player Player;
    public Sign Sign;
    public Experience Experience;
    public AddToJournal AddToJournal;
    public Obstacle Obstacle;
    public Maze Maze;
    public StarPickup StarPickup;
    public PauseMenu PauseMenu;

    public AudioSource questAudioSource;
    public AudioClip questCompletedSound;

    public ParticleSystem questTextParticle;
    public AudioSource pickUpAudioSource;
    public bool questDone;
    public TMP_Text starsRedMazeCount;

    public GameObject txtStarsRedMazeSetActive;

    public TMP_Text expGain, starGain;
    public Animation expGainAnim, starGainAnim;

    public GameObject mazeCollider;

    public Vector3 starRedMazeLocation;

    private void Awake()
    {
        starRedMazeLocation = transform.position;

        questAudioSource.clip = questCompletedSound;
        txtStarsRedMazeSetActive.SetActive(false);

        if (PlayerPrefs.HasKey("redMazeTextBool"))
        {
            Player.starRedMaze = PlayerPrefs.GetInt("starsRedMaze");
            starsRedMazeCount.text = Player.starRedMaze.ToString() + "/53";
            txtStarsRedMazeSetActive.SetActive(true);
        }

        if (PlayerPrefs.HasKey("questDoneRedMaze"))
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

            txtStarsRedMazeSetActive.SetActive(false);
            mazeCollider.SetActive(false);
            Maze.txtInMaze.gameObject.SetActive(false);
            Maze.inMaze = false;
        }
    }

    private void Update()
    {
        if (Player.starRedMaze == 53)
        {
            txtStarsRedMazeSetActive.SetActive(false);
        }

        // Maze içinde ölürsek starlar eski yerlerine gelir ve 0 lanýr
        if (Maze.inMaze && Obstacle.isDead)
        {
            Player.playerStarsTheDesert -= Player.starRedMaze;
            PlayerPrefs.SetInt("starsTheDesert", Player.playerStarsTheDesert);

            Player.remainingStarsTheDesert += Player.starRedMaze;
            PlayerPrefs.SetInt("remainingStarsTheDesert", Player.remainingStarsTheDesert);

            transform.position = starRedMazeLocation;
            starsRedMazeCount.text = "0/53";

            Player.star -= Player.starRedMaze;
            PlayerPrefs.SetInt("starsRedMaze", Player.starRedMaze);

            if (Player.star <= -1)
            {
                Player.star = 0;
            }

            StarPickup.txtStarCount.text = Player.star.ToString();

            PlayerPrefs.SetInt("playerStar", Player.star);

            Player.starRedMaze = 0;
            PlayerPrefs.SetInt("starsRedMaze", Player.starRedMaze);

            PlayerPrefs.DeleteKey("StarPosX" + gameObject.name);
            PlayerPrefs.DeleteKey("StarPosY" + gameObject.name);
            PlayerPrefs.DeleteKey("StarPosZ" + gameObject.name);

            if (PlayerPrefs.HasKey("StarPosX") == false)
            {
                transform.position = StarPickup.posStar;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Menzile girdiðin an objeyi alýrsýn
        if (other.tag is "Player")
        {
            if (Player.starRedMaze == 52)
            {
                QuestCompleted();

                questTextParticle.Play();
                questAudioSource.Play();

                Sign.done.SetActive(true);
                Sign.doneIcon.SetActive(true);
                Sign.signIcon.SetActive(false);

                Sign.doneIconMinimap.SetActive(true);
                Sign.signIconMinimap.SetActive(false);

                txtStarsRedMazeSetActive.SetActive(false);
                mazeCollider.SetActive(false);
                Maze.txtInMaze.gameObject.SetActive(false);
                Maze.inMaze = false;

                PlayExpStarTextAnim();
            }

            Player.starRedMaze++;
            PlayerPrefs.SetInt("starsRedMaze", Player.starRedMaze);
            PlayerPrefs.SetInt("redMazeTextBool", 1); // Text active için bool görevi görür        

            if (Player.starRedMaze < 52)
            {
                txtStarsRedMazeSetActive.SetActive(true);
                starsRedMazeCount.text = Player.starRedMaze.ToString() + "/53";
            }

            pickUpAudioSource.Play();
        }
    }

    public void QuestCompleted()
    {
        AddToJournal.questDone = true;
        PlayerPrefs.SetInt("questDoneRedMaze", 1);

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
