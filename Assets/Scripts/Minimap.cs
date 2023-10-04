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

        //Minimapteki Player iconunun Player rotation a göre dönmesini saðlar
        minimapPlayer.rotation = Quaternion.Euler(0f, 0f, -player.eulerAngles.y - 45f);

        // Minimap dönmesini saðlar
        //transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}
