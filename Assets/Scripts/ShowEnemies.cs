using UnityEngine;
using TMPro;

public class ShowEnemies : MonoBehaviour
{
    public Obstacle Obstacle;
    public Invisible Invisible;

    public TMP_Text txtShowEnemy;
    public AudioSource showEnemyAudioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag is "Player")
        {
            foreach (GameObject enemy in Invisible.Enemies)
            {
                enemy.GetComponent<MeshRenderer>().enabled = true;
            }

            Invisible.enemyVisible = true;
            showEnemyAudioSource.Play();
            txtShowEnemy.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag is "Player")
        {
            foreach (GameObject enemy in Invisible.Enemies)
            {
                enemy.GetComponent<MeshRenderer>().enabled = false;
            }

            Invisible.enemyVisible = false;
            txtShowEnemy.gameObject.SetActive(true);
        }
    }
}
