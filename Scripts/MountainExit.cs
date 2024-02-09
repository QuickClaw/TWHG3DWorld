using UnityEngine;

public class MountainExit : MonoBehaviour
{
    public Player Player;
    public Movement Movement;
    public TheMysteryOfTheMountain1 TheMysteryOfTheMountain1;

    public GameObject player;
    public AudioSource mountainExitAudioSource;

    void Start()
    {
        TheMysteryOfTheMountain1.mountainExitCollider.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Invoke("ExitMountain", 1.5f);
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

    public void ExitMountain()
    {
        player.transform.position = new Vector3(-79.7f, -0.089f, -4.3f);
        mountainExitAudioSource.Play();
    }
}
