using UnityEngine;
using TMPro;

public class LightProtect : MonoBehaviour
{
    public TheLight TheLight;
    public TMP_Text txtLightProtect;
    public bool inLight;

    private void Start()
    {

    }

    private void Update()
    {
        if (inLight)
        {
            for (int i = 0; i < TheLight.Follow.Length; i++)
            {
                TheLight.Follow[i].enabled = false;
            }
        }
        else
        {
            for (int i = 0; i < TheLight.Follow.Length; i++)
            {
                TheLight.Follow[i].enabled = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag is "Player")
        {
            txtLightProtect.gameObject.SetActive(true);
            inLight = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag is "Player")
        {
            txtLightProtect.gameObject.SetActive(false);
            inLight = false;
        }
    }
}
