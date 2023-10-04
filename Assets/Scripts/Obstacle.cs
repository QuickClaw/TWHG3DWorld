using UnityEngine;
using Steamworks;

public class Obstacle : MonoBehaviour
{
    public Movement Movement;
    public Transform player;
    public Potions Potions;
    public Player Player;
    public CameraShake CameraShake;
    public Invisible Invisible;
    public PauseMenu PauseMenu;
    public StarPickup StarPickup;
    public HeartPickup HeartPickup;

    public bool isDead;
    public float respawnTime;
    public ParticleSystem deathEffect;
    public AudioSource deathAudioSource;
    public Vector3 respawnLocation;

    public float shakeDuration, shakeMagnitude;

    void Start()
    {
        Movement.enabled = true;
        respawnLocation = player.transform.position;

        PauseMenu.deathCount.text = Player.death.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Potions.shieldTaken)
        {
            if (other.tag is "Obstacle" || other.tag is "Enemy")
            {
                Potions.ShieldExpire();
            }
        }
        else
        {
            if (isDead == false)
            {
                if (other.tag is "Obstacle" || other.tag is "Enemy")
                {
                    StartCoroutine(CameraShake.Shake(shakeDuration, shakeMagnitude));

                    Player.death += 1;
                    PlayerPrefs.SetInt("playerDeath", Player.death);

                    PauseMenu.deathCount.text = Player.death.ToString();

                    if (Player.heart == 0)
                    {
                        Player.heart = 0;
                    }
                    else
                    {
                        Player.heart -= 1;
                    }
                        
                    if (Player.heart == 0)
                    {
                        if (Player.star < 2)
                        {
                            Player.star = 0;
                        }
                        else
                        {
                            Player.star -= 1;
                        }                           
                    }
                    PlayerPrefs.SetInt("playerHeart", Player.heart);
                    PlayerPrefs.SetInt("playerStar", Player.star);

                    StarPickup.txtStarCount.text = Player.star.ToString();
                    HeartPickup.txtHeartCount.text = Player.heart.ToString();

                    Movement.enabled = false;
                    isDead = true;

                    deathEffect.Play();
                    deathAudioSource.Play();

                    Invoke("Respawn", respawnTime);

                    if (Player.sceneName == "Invisible")
                    {
                        foreach (GameObject enemy in Invisible.Enemies)
                            enemy.GetComponent<MeshRenderer>().enabled = true;

                        Invisible.enemyVisible = true;
                    }

                    if (Player.death == 10)
                    {
                        SteamUserStats.SetAchievement("achievement_11");
                        SteamUserStats.StoreStats();
                    }

                    if (Player.death == 50)
                    {
                        SteamUserStats.SetAchievement("achievement_12");
                        SteamUserStats.StoreStats();
                    }

                    if (Player.death == 100)
                    {
                        SteamUserStats.SetAchievement("achievement_13");
                        SteamUserStats.StoreStats();
                    }

                    if (Player.death == 200)
                    {
                        SteamUserStats.SetAchievement("achievement_14");
                        SteamUserStats.StoreStats();
                    }

                    if (Player.death == 500)
                    {
                        SteamUserStats.SetAchievement("achievement_15");
                        SteamUserStats.StoreStats();
                    }

                    if (Player.death == 1000)
                    {
                        SteamUserStats.SetAchievement("achievement_16");
                        SteamUserStats.StoreStats();
                    }

                    if (Player.death == 2500)
                    {
                        SteamUserStats.SetAchievement("achievement_17");
                        SteamUserStats.StoreStats();
                    }

                    if (Player.death == 5000)
                    {
                        SteamUserStats.SetAchievement("achievement_18");
                        SteamUserStats.StoreStats();
                    }
                }
            }
        }
    }

    void Respawn()
    {
        player.transform.position = respawnLocation;
        isDead = false;
        Movement.enabled = true;
        //Movement.playerSpeed = Movement.beforeMudPlayerSpeed;
    }
}
