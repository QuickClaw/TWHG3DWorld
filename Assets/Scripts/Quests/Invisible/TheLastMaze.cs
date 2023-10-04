using UnityEngine;
using TMPro;

public class TheLastMaze : MonoBehaviour
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

    public GameObject txtTheLastMazeSetActive;
    public TMP_Text starsTheLastMazeCount;

    public TMP_Text expGain, starGain;
    public Animation expGainAnim, starGainAnim;

    public GameObject mazeCollider;

    public Vector3 starTheLastMazeLocation;

    private void Awake()
    {
        starTheLastMazeLocation = transform.position;

        questAudioSource.clip = questCompletedSound;
        txtTheLastMazeSetActive.SetActive(false);

        if (PlayerPrefs.HasKey("theLastMazeTextBool"))
        {
            Player.theLastMazeStars = PlayerPrefs.GetInt("starsTheLastMaze");
            starsTheLastMazeCount.text = Player.theLastMazeStars.ToString() + "/13";
            txtTheLastMazeSetActive.SetActive(true);
        }

        if (PlayerPrefs.HasKey("questDoneTheLastMaze"))
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

            txtTheLastMazeSetActive.SetActive(false);
            mazeCollider.SetActive(false);
            Maze.txtInMaze.gameObject.SetActive(false);
            Maze.inMaze = false;
        }
    }

    private void Update()
    {
        if (Player.theLastMazeStars == 13)
        {
            txtTheLastMazeSetActive.SetActive(false);
            mazeCollider.SetActive(false);
            Maze.inMaze = false;
            Maze.txtInMaze.gameObject.SetActive(false);
        }

        // Maze içinde ölürsek starlar eski yerlerine gelir ve 0 lanýr
        if (Maze.inMaze && Obstacle.isDead)
        {
            Player.playerStarsInvisible -= Player.theLastMazeStars;
            PlayerPrefs.SetInt("starsInvisible", Player.playerStarsInvisible);

            Player.remainingStarsInvisible += Player.theLastMazeStars;
            PlayerPrefs.SetInt("remainingStarsInvisible", Player.remainingStarsInvisible);

            transform.position = starTheLastMazeLocation;
            starsTheLastMazeCount.text = "0/13";

            Player.star -= Player.theLastMazeStars;
            PlayerPrefs.SetInt("starsTheLastMaze", Player.theLastMazeStars);

            if (Player.star <= -1)
            {
                Player.star = 0;
            }

            StarPickup.txtStarCount.text = Player.star.ToString();

            PlayerPrefs.SetInt("playerStar", Player.star);

            Player.theLastMazeStars = 0;
            PlayerPrefs.SetInt("starsTheLastMaze", Player.theLastMazeStars);

            PlayerPrefs.DeleteKey("StarPosX" + StarPickup.gameObject.name);
            PlayerPrefs.DeleteKey("StarPosY" + StarPickup.gameObject.name);
            PlayerPrefs.DeleteKey("StarPosZ" + StarPickup.gameObject.name);

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
            if (Player.theLastMazeStars == 12)
            {
                QuestCompleted();

                questTextParticle.Play();
                questAudioSource.Play();

                Sign.done.SetActive(true);
                Sign.doneIcon.SetActive(true);
                Sign.signIcon.SetActive(false);

                Sign.doneIconMinimap.SetActive(true);
                Sign.signIconMinimap.SetActive(false);

                txtTheLastMazeSetActive.SetActive(false);
                mazeCollider.SetActive(false);
                Maze.txtInMaze.gameObject.SetActive(false);
                Maze.inMaze = false;

                PlayExpStarTextAnim();
            }

            Player.theLastMazeStars++;
            PlayerPrefs.SetInt("starsTheLastMaze", Player.theLastMazeStars);
            PlayerPrefs.SetInt("theLastMazeTextBool", 1); // Text active için bool görevi görür    

            if (Player.theLastMazeStars < 12)
            {
                starsTheLastMazeCount.text = Player.theLastMazeStars.ToString() + "/13";
                txtTheLastMazeSetActive.SetActive(true);
            }

            pickUpAudioSource.Play();                 
        }
    }

    public void QuestCompleted()
    {
        AddToJournal.questDone = true;
        PlayerPrefs.SetInt("questDoneTheLastMaze", 1);

        Player.questInvisible++;
        PlayerPrefs.SetInt("completedQuestsInvisible", Player.questInvisible);

        Player.experience += Sign.exp;

        Player.star += Sign.star;
        PlayerPrefs.SetInt("playerStar", Player.star);

        StarPickup.txtStarCount.text = Player.star.ToString();
        PauseMenu.questCount.text = Player.questInvisible.ToString() + "/" + AddToJournal.questNames.Count;

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
