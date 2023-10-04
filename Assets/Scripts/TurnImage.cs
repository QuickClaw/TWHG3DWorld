using UnityEngine;

public class TurnImage : MonoBehaviour
{
    public Vector3 UIElement;

    void Update()
    {
        transform.Rotate(UIElement);
    }
}
