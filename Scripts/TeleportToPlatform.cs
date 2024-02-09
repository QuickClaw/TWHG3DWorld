using UnityEngine;

public class TeleportToPlatform : MonoBehaviour
{
    public Player Player;
    public Movement Movement;

    public GameObject player;
    public AudioSource teleportPlatformAudioSource;
    public ParticleSystem teleportEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Invoke("TeleportPlatform", 1.5f);
            Movement.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Movement.enabled = true;
        }
    }

    public void TeleportPlatform()
    {
        player.transform.position = new Vector3(-199.67f, 60.313f, 113.668f);
        teleportPlatformAudioSource.Play();
        teleportEffect.Play();
    }
}
