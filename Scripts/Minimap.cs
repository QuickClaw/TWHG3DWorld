using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform player;
    public Transform minimapPlayer;

    private void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        //Minimapteki Player iconunun Player rotation a g�re d�nmesini sa�lar
        minimapPlayer.rotation = Quaternion.Euler(0f, 0f, -player.eulerAngles.y - 45f);

        // Minimap d�nmesini sa�lar
        //transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}
