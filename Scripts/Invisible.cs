using UnityEngine;

public class Invisible : MonoBehaviour
{
    public GameObject[] Enemies;
    public bool enemyVisible;

    private void Awake()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Obstacle");
    }
}
