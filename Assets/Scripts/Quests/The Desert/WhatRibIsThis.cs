using UnityEngine;
using TMPro;

public class WhatRibIsThis : MonoBehaviour
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

    public TMP_Text starsRibCount;
    public GameObject txtStarRibSetActive;

    public TMP_Text expGain, starGain;
    public Animation expGainAnim, starGainAnim;

    private void Awake()
    {
        questAudioSource.clip = questCompletedSound;
        txtStarRibSetActive.SetActive(false);

        if (PlayerPrefs.HasKey("whatRibIsThisTextBool"))
        {
            Player.starRibCount = PlayerPrefs.GetInt("starsWhatRibIsThis");
            starsRibCount.text = Player.starRibCount.ToString() + "/12";
            txtStarRibSetActive.SetActive(true);
        }

        if (PlayerPrefs.HasKey("questDoneWhatRibIsThis"))
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

            txtStarRibSetActive.SetActive(false);
        }
    }

    private void Update()
    {
        if (Player.starRibCount == 12)
        {
            txtStarRibSetActive.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Menzile girdiðin an objeyi alýrsýn
        if (other.tag is "Player")
        {
            if (Player.starRibCount == 11)
            {
                QuestCompleted();

                questTextParticle.Play();
                questAudioSource.Play();

                Sign.done.SetActive(true);
                Sign.doneIcon.SetActive(true);
                Sign.signIcon.SetActive(false);

                Sign.doneIconMinimap.SetActive(true);
                Sign.signIconMinimap.SetActive(false);

                txtStarRibSetActive.SetActive(false);

                PlayExpStarTextAnim();
            }

            pickUpAudioSource.Play();

            Player.starRibCount++;
            PlayerPrefs.SetInt("starsWhatRibIsThis", Player.starRibCount);
            PlayerPrefs.SetInt("whatRibIsThisTextBool", 1); // Text active için bool görevi görür

            starsRibCount.text = Player.starRibCount.ToString() + "/12";
            txtStarRibSetActive.SetActive(true);
        }
    }

    public void QuestCompleted()
    {
        AddToJournal.questDone = true;
        PlayerPrefs.SetInt("questDoneWhatRibIsThis", 1);

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
