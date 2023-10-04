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

    // Pot alýnmamýþken çamura girilirse karakter yavaþlayacak
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

    // Çamurdan çýkýnca karakter hýzý normal olacak
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

    // Çamurdayken pot süresi biterse karakter yavaþlayacak
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

        // Çamurdayken pot alýnýrsa karakter hýzý normal olacak
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
