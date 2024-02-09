using UnityEngine;

public class LockRotation : MonoBehaviour
{
    void Update()
    {
        transform.rotation = Quaternion.Euler(90, 0, 0);
    }
}