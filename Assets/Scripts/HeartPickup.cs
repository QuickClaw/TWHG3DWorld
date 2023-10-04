using UnityEngine;
using TMPro;

public class HeartPickup : MonoBehaviour
{
    public Player Player;

    public TMP_Text txtHeartCount;
    public ParticleSystem heartEffect;

    public AudioSource heartAudioSouce;

    Vector3 posHeart;

    void Start()
    {
        txtHeartCount.text = Player.heart.ToString();
        posHeart = transform.position;      

        if (PlayerPrefs.HasKey("HeartPosX" + gameObject.name) == true)
        {
            float x = PlayerPrefs.GetFloat("HeartPosX" + gameObject.name);
            float y = PlayerPrefs.GetFloat("HeartPosY" + gameObject.name);
            float z = PlayerPrefs.GetFloat("HeartPosZ" + gameObject.name);

            transform.position = new Vector3(x, y, z);
        }
        else
            transform.position = posHeart;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag is "Player")
        {
            transform.position = new Vector3(0, 5000, 0);
            PlayerPrefs.SetFloat("HeartPosX" + gameObject.name, transform.position.x);
            PlayerPrefs.SetFloat("HeartPosY" + gameObject.name, transform.position.y);
            PlayerPrefs.SetFloat("HeartPosZ" + gameObject.name, transform.position.z);

            Player.heart++;
            PlayerPrefs.SetInt("playerHeart", Player.heart);

            txtHeartCount.text = Player.heart.ToString();

            heartEffect.Play();
            heartAudioSouce.Play();
        }
    }
}
