using UnityEngine;

public class Map : MonoBehaviour
{
    public PauseMenu PauseMenu;

    public float interpolatedTime = 0;
    public Transform player;
    public Transform mapPlayer;

    public Camera mapCamera;

    public GameObject map;

    private void Update()
    {
        if (PauseMenu.isMapOpen == false)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                map.SetActive(true);
                PauseMenu.isMapOpen = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                map.SetActive(false);
                PauseMenu.isMapOpen = false;
            }
        }
    }

    private void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y = mapPlayer.position.y;
        mapPlayer.position = newPosition;
    }
}
