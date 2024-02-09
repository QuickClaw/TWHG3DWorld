using UnityEngine;

public class Follow : MonoBehaviour
{
    public Obstacle Obstacle;

    public Transform target;

    public float speed;
    public float howFarEnemySpawn;

    void Update()
    {
        transform.LookAt(target.position);
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);

        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));

        if (Obstacle.isDead)
        {
            transform.position = new Vector3(target.position.x + howFarEnemySpawn, 0, 0);
        }
    }
}
