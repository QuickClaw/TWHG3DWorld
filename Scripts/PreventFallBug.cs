using UnityEngine;

public class PreventFallBug : MonoBehaviour
{
    public Transform player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag is "Player")
        {
            player.transform.position = new Vector3(player.transform.position.x, -0.089f, player.transform.position.z);
        }
    }
}
