using UnityEngine;

public class OpenLight : MonoBehaviour
{
    public GameObject playerLight;
    public bool isLightOn;

    void Update()
    {
        if (isLightOn == false)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                playerLight.SetActive(true);
                isLightOn = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                playerLight.SetActive(false);
                isLightOn = false;
            }
        }
    }
}
