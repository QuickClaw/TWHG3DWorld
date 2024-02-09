using UnityEngine;

public class CircularMovement : MonoBehaviour
{
    public float radius = 1f;
    public float currentAngle = 0f;
    private float speed = 0f;
    public float timeToCompleteCircle = 1.5f;
    public Vector3 startingPosition;

    void Start()
    {
        startingPosition = transform.position;
    }

    void Awake()
    {
        speed = (Mathf.PI * 2) / timeToCompleteCircle;
    }

    void FixedUpdate()
    {
        speed = (Mathf.PI * 2) / timeToCompleteCircle;
        currentAngle += Time.deltaTime * speed;

        float newX = radius * Mathf.Sin(currentAngle);
        float newZ = radius * Mathf.Cos(currentAngle);
        float y = 0;

        transform.position = startingPosition + new Vector3(newX, y, newZ);
    }
}
