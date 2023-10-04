using UnityEngine;
using TMPro;

public class MudDebuff : MonoBehaviour
{
    public Movement Movement;
    public Potions Potions;
    public GameObject mudDebuff;
    public static bool inMud = false;
    public TMP_Text txtMudDebuff;

    float debuffPlayerSpeed, debuffDashSpeed, debuffDashCooldown;
    public float debuffPlayerSpeedPercentage, debuffDashSpeedPercentage;

    private void Start()
    {
        mudDebuff.SetActive(false);

        debuffPlayerSpeed = Movement.playerSpeed - (Movement.playerSpeed * debuffPlayerSpeedPercentage) / 100;
        debuffDashSpeed = Movement.dashSpeed - (Movement.dashSpeed * debuffDashSpeedPercentage) / 100;

        txtMudDebuff.text = "Player speed decreased by %" + debuffPlayerSpeedPercentage + "\n" + "Dash speed decreased by %" + debuffDashSpeedPercentage;
    }

    // Pot al�nmam��ken �amura girilirse karakter yava�layacak
    private void OnTriggerEnter(Collider other)
    {
        if (Potions.mudResistanceTaken == false)
        {
            if (other.tag is "Player")
            {
                inMud = true;
                mudDebuff.SetActive(true);

                Movement.playerSpeed = debuffPlayerSpeed;
                Movement.dashSpeed = debuffDashSpeed;
            }
        }
    }

    // �amurdan ��k�nca karakter h�z� normal olacak
    private void OnTriggerExit(Collider other)
    {
        if (other.tag is "Player")
        {
            inMud = false;
            Movement.playerSpeed = Movement.beforeMudPlayerSpeed;
            Movement.dashSpeed = Movement.beforeMudDashSpeed;
            mudDebuff.SetActive(false);
        }
    }

    // �amurdayken pot s�resi biterse karakter yava�layacak
    private void OnTriggerStay(Collider other)
    {
        if (Potions.mudResistanceTaken == false)
        {
            if (other.tag is "Player")
            {
                inMud = true;
                Movement.playerSpeed = debuffPlayerSpeed;
                Movement.dashSpeed = debuffDashSpeed;
                mudDebuff.SetActive(true);
            }
        }

        // �amurdayken pot al�n�rsa karakter h�z� normal olacak
        if (Potions.mudResistanceTaken == true)
        {
            if (other.tag is "Player")
            {
                inMud = true;
                Movement.playerSpeed = Movement.beforeMudPlayerSpeed;
                Movement.dashSpeed = Movement.beforeMudDashSpeed;
                mudDebuff.SetActive(false);
            }
        }
    }
}
