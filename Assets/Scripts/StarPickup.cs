using UnityEngine;
using TMPro;

public class StarPickup : MonoBehaviour
{
    public Player Player;
    public Experience Experience;

    public TMP_Text txtStarCount;

    public ParticleSystem starEffect;
    public AudioClip starPickupSound;
    public AudioSource starAudioSouce;

    public Vector3 posStar;

    void Start()
    {
        txtStarCount.text = Player.star.ToString();
        posStar = transform.position;

        starAudioSouce.clip = starPickupSound;

        if (PlayerPrefs.HasKey("StarPosX" + gameObject.name) == true)
        {
            float x = PlayerPrefs.GetFloat("StarPosX" + gameObject.name);
            float y = PlayerPrefs.GetFloat("StarPosY" + gameObject.name);
            float z = PlayerPrefs.GetFloat("StarPosZ" + gameObject.name);

            transform.position = new Vector3(x, y, z);
        }
        else
            transform.position = posStar;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag is "Player")
        {
            transform.position = new Vector3(0, 5000, 0);
            PlayerPrefs.SetFloat("StarPosX" + gameObject.name, transform.position.x);
            PlayerPrefs.SetFloat("StarPosY" + gameObject.name, transform.position.y);
            PlayerPrefs.SetFloat("StarPosZ" + gameObject.name, transform.position.z);

            Player.star++;
            PlayerPrefs.SetInt("playerStar", Player.star);

            txtStarCount.text = Player.star.ToString();

            Experience.EarnExp(1);

            starEffect.Play();
            starAudioSouce.Play();

            // Haritaya göre yýldýz sayýsý
            if (Player.sceneName == "The Forest")
            {
                Player.playerStarsTheForest++;
                PlayerPrefs.SetInt("starsTheForest", Player.playerStarsTheForest);

                Player.remainingStarsTheForest = Player.allStarsTheForest - Player.playerStarsTheForest;
                PlayerPrefs.SetInt("remainingStarsTheForest", Player.remainingStarsTheForest);
            }

            if (Player.sceneName == "Snowy")
            {
                Player.playerStarsSnowy++;
                PlayerPrefs.SetInt("starsSnowy", Player.playerStarsSnowy);

                Player.remainingStarsSnowy = Player.allStarsSnowy - Player.playerStarsSnowy;
                PlayerPrefs.SetInt("remainingStarsSnowy", Player.remainingStarsSnowy);
            }

            if (Player.sceneName == "The Desert")
            {
                Player.playerStarsTheDesert++;
                PlayerPrefs.SetInt("starsTheDesert", Player.playerStarsTheDesert);

                Player.remainingStarsTheDesert = Player.allStarsTheDesert - Player.playerStarsTheDesert;
                PlayerPrefs.SetInt("remainingStarsTheDesert", Player.remainingStarsTheDesert);
            }

            if (Player.sceneName == "Foggy")
            {
                Player.playerStarsFoggy++;
                PlayerPrefs.SetInt("starsFoggy", Player.playerStarsFoggy);

                Player.remainingStarsFoggy = Player.allStarsFoggy - Player.playerStarsFoggy;
                PlayerPrefs.SetInt("remainingStarsFoggy", Player.remainingStarsFoggy);
            }

            if (Player.sceneName == "Invisible")
            {
                Player.playerStarsInvisible++;
                PlayerPrefs.SetInt("starsInvisible", Player.playerStarsInvisible);

                Player.remainingStarsInvisible = Player.allStarsInvisible - Player.playerStarsInvisible;
                PlayerPrefs.SetInt("remainingStarsInvisible", Player.remainingStarsInvisible);
            }
        }
    }
}
