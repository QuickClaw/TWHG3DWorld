using UnityEngine;

public class ExitArena : MonoBehaviour
{
    public Player Player;
    public Movement Movement;

    public Transform playerTransform;
    public AudioSource exitArenaAudioSource;
    public ParticleSystem exitArenaEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Invoke("ExitFromArena", 1.5f);
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

    public void ExitFromArena()
    {
        playerTransform.position = new Vector3(-118.313f, 67.997f, -45.36101f);
        exitArenaAudioSource.Play();
        exitArenaEffect.Play();
    }
}
