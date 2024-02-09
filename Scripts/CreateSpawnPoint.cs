using UnityEngine;
using TMPro;

public class CreateSpawnPoint : MonoBehaviour
{
    public Obstacle Obstacle;

    public TMP_Text txtSpawnPointInfo;
    public Animation spawnPointInfo;
    public AudioSource spawnAudioSource;

    public GameObject spawnPointIcon;
    public GameObject spawnPointIconMinimap;

    void Start()
    {
        if (Player.sceneName is "The Forest")
        {
            if (PlayerPrefs.HasKey("PlayerPosX_TheForest") == true)
            {
                float x = PlayerPrefs.GetFloat("PlayerPosX_TheForest");
                float y = PlayerPrefs.GetFloat("PlayerPosY_TheForest");
                float z = PlayerPrefs.GetFloat("PlayerPosZ_TheForest");

                Obstacle.player.transform.position = new Vector3(x, y, z);
            }
        }

        if (Player.sceneName is "Snowy")
        {
            if (PlayerPrefs.HasKey("PlayerPosX_Snowy") == true)
            {
                float x = PlayerPrefs.GetFloat("PlayerPosX_Snowy");
                float y = PlayerPrefs.GetFloat("PlayerPosY_Snowy");
                float z = PlayerPrefs.GetFloat("PlayerPosZ_Snowy");

                Obstacle.player.transform.position = new Vector3(x, y, z);
            }
        }

        if (Player.sceneName is "The Desert")
        {
            if (PlayerPrefs.HasKey("PlayerPosX_TheDesert") == true)
            {
                float x = PlayerPrefs.GetFloat("PlayerPosX_TheDesert");
                float y = PlayerPrefs.GetFloat("PlayerPosY_TheDesert");
                float z = PlayerPrefs.GetFloat("PlayerPosZ_TheDesert");

                Obstacle.player.transform.position = new Vector3(x, y, z);
            }
        }

        if (Player.sceneName is "Foggy")
        {
            if (PlayerPrefs.HasKey("PlayerPosX_Foggy") == true)
            {
                float x = PlayerPrefs.GetFloat("PlayerPosX_Foggy");
                float y = PlayerPrefs.GetFloat("PlayerPosY_Foggy");
                float z = PlayerPrefs.GetFloat("PlayerPosZ_Foggy");

                Obstacle.player.transform.position = new Vector3(x, y, z);
            }
        }

        if (Player.sceneName is "Invisible")
        {
            if (PlayerPrefs.HasKey("PlayerPosX_Invisible") == true)
            {
                float x = PlayerPrefs.GetFloat("PlayerPosX_Invisible");
                float y = PlayerPrefs.GetFloat("PlayerPosY_Invisible");
                float z = PlayerPrefs.GetFloat("PlayerPosZ_Invisible");

                Obstacle.player.transform.position = new Vector3(x, y, z);
            }
        }

        txtSpawnPointInfo.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag is "Player")
        {
            if (Player.sceneName is "The Forest")
            {
                Obstacle.respawnLocation = transform.position;
                PlayerPrefs.SetFloat("PlayerPosX_TheForest", transform.position.x);
                PlayerPrefs.SetFloat("PlayerPosY_TheForest", transform.position.y);
                PlayerPrefs.SetFloat("PlayerPosZ_TheForest", transform.position.z);

                txtSpawnPointInfo.gameObject.SetActive(true);

                spawnPointInfo.Play();
                spawnAudioSource.Play();

                spawnPointIcon.transform.position = transform.position;
                spawnPointIconMinimap.transform.position = transform.position;
            }

            if (Player.sceneName is "Snowy")
            {
                Obstacle.respawnLocation = transform.position;
                PlayerPrefs.SetFloat("PlayerPosX_Snowy", transform.position.x);
                PlayerPrefs.SetFloat("PlayerPosY_Snowy", transform.position.y);
                PlayerPrefs.SetFloat("PlayerPosZ_Snowy", transform.position.z);

                txtSpawnPointInfo.gameObject.SetActive(true);

                spawnPointInfo.Play();
                spawnAudioSource.Play();

                spawnPointIcon.transform.position = transform.position;
                spawnPointIconMinimap.transform.position = transform.position;
            }

            if (Player.sceneName is "The Desert")
            {
                Obstacle.respawnLocation = transform.position;
                PlayerPrefs.SetFloat("PlayerPosX_TheDesert", transform.position.x);
                PlayerPrefs.SetFloat("PlayerPosY_TheDesert", transform.position.y);
                PlayerPrefs.SetFloat("PlayerPosZ_TheDesert", transform.position.z);

                txtSpawnPointInfo.gameObject.SetActive(true);

                spawnPointInfo.Play();
                spawnAudioSource.Play();

                spawnPointIcon.transform.position = transform.position;
                spawnPointIconMinimap.transform.position = transform.position;
            }

            if (Player.sceneName is "Foggy")
            {
                Obstacle.respawnLocation = transform.position;
                PlayerPrefs.SetFloat("PlayerPosX_Foggy", transform.position.x);
                PlayerPrefs.SetFloat("PlayerPosY_Foggy", transform.position.y);
                PlayerPrefs.SetFloat("PlayerPosZ_Foggy", transform.position.z);

                txtSpawnPointInfo.gameObject.SetActive(true);

                spawnPointInfo.Play();
                spawnAudioSource.Play();

                spawnPointIcon.transform.position = transform.position;
                spawnPointIconMinimap.transform.position = transform.position;
            }

            if (Player.sceneName is "Invisible")
            {
                Obstacle.respawnLocation = transform.position;
                PlayerPrefs.SetFloat("PlayerPosX_Invisible", transform.position.x);
                PlayerPrefs.SetFloat("PlayerPosY_Invisible", transform.position.y);
                PlayerPrefs.SetFloat("PlayerPosZ_Invisible", transform.position.z);

                txtSpawnPointInfo.gameObject.SetActive(true);

                spawnPointInfo.Play();
                spawnAudioSource.Play();

                spawnPointIcon.transform.position = transform.position;
                spawnPointIconMinimap.transform.position = transform.position;
            }
        }        
    }
}
