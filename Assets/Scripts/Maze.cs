using UnityEngine;
using TMPro;

public class Maze : MonoBehaviour
{
    public TMP_Text txtInMaze;
    public bool inMaze;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag is "Player")
        {
            inMaze = true;
            txtInMaze.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag is "Player")
        {
            inMaze = false;
            txtInMaze.gameObject.SetActive(false);
        }
    }
}
