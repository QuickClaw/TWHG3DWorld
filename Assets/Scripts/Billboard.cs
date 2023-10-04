using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform cam;

    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
