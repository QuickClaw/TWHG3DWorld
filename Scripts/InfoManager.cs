using UnityEngine;

public class InfoManager : MonoBehaviour
{
    public Animation InfoAnim;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag is "Player")
        {
            InfoAnim.Play();
        }
    }
}
