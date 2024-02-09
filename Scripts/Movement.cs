using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public int questCount;
    public float playerSpeed;
    public float dashSpeed, dashCooldown, dashTime;
    public float beforeMudDashCooldown, beforeMudPlayerSpeed, beforeMudDashSpeed;

    private float nextDashTime, cooldownTimer, dashStartTime, fillAmountDash = 0;
    public static bool wasdPressed = false, isDashed;

    public ParticleSystem dashEffect;
    public Image dashImage;

    public TPS TPS;
    public Potions Potions;

    public AudioClip dashSound, dashReadySound;
    public AudioSource dashAudioSource, dashReadySource;

    public bool mouseVisible;

    private void Start()
    {
        // Dash Cooldown
        if (PlayerPrefs.GetFloat("playerDashCooldown") == 0)
            dashCooldown = PlayerPrefs.GetFloat("playerDashCooldown", 2);
        else
            dashCooldown = PlayerPrefs.GetFloat("playerDashCooldown");

        dashAudioSource.clip = dashSound;
        dashReadySource.clip = dashReadySound;

        dashImage.fillAmount = 1f;
        beforeMudPlayerSpeed = playerSpeed;
        beforeMudDashSpeed = dashSpeed;
    }

    [System.Obsolete]
    void Update()
    {
        float leftRight = Input.GetAxisRaw("Horizontal");
        float forwardBackward = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(leftRight, 0.0f, forwardBackward) * playerSpeed * Time.deltaTime;

        transform.Translate(movement, Space.Self);

        #region Slow 
        if (Input.GetKey(KeyCode.LeftControl))
        {
            playerSpeed = 2;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            playerSpeed = beforeMudPlayerSpeed;
        }
        #endregion

        #region Dash
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (dashImage.fillAmount == 1f)
            {
                dashAudioSource.Play();
            }

            if (Potions.staminaTaken)
            {
                dashEffect.startColor = Color.green;
                dashEffect.startSize = 0.85f;
            }
            else
            {
                dashEffect.startColor = Color.white;
                dashEffect.startSize = 0.5f;
            }

            StartCoroutine(Dash());
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            wasdPressed = true;
        }
        else
        {
            wasdPressed = false;
        }

        if (isDashed)
        {
            DashCooldown();
        }


        #endregion
    }

    IEnumerator Dash()
    {
        dashStartTime = Time.time;

        #region Dash to Forward
        if (Time.time > nextDashTime)
        {
            if (Input.GetKey(KeyCode.W))
            {
                while (Time.time < dashStartTime + dashTime)
                {
                    transform.Translate(Vector3.forward * dashSpeed);
                    dashEffect.Play();
                    cooldownTimer = dashCooldown;
                    fillAmountDash = 0;
                    isDashed = true;
                    nextDashTime = Time.time + dashCooldown;
                    yield return null;
                }
            }
        }
        #endregion

        #region Dash to Back
        if (Time.time > nextDashTime)
        {
            if (Input.GetKey(KeyCode.S))
            {
                while (Time.time < dashStartTime + dashTime)
                {
                    transform.Translate(Vector3.back * dashSpeed);
                    dashEffect.Play();
                    cooldownTimer = dashCooldown;
                    fillAmountDash = 0;
                    isDashed = true;
                    nextDashTime = Time.time + dashCooldown;
                    yield return null;
                }
            }
        }
        #endregion

        #region Dash to Right
        if (Time.time > nextDashTime)
        {
            if (Input.GetKey(KeyCode.D))
            {
                while (Time.time < dashStartTime + dashTime)
                {
                    transform.Translate(Vector3.right * dashSpeed);
                    dashEffect.Play();
                    cooldownTimer = dashCooldown;
                    fillAmountDash = 0;
                    isDashed = true;
                    nextDashTime = Time.time + dashCooldown;
                    yield return null;
                }
            }
        }
        #endregion

        #region Dash to Left
        if (Time.time > nextDashTime)
        {
            if (Input.GetKey(KeyCode.A))
            {
                while (Time.time < dashStartTime + dashTime)
                {
                    transform.Translate(Vector3.left * dashSpeed);
                    dashEffect.Play();
                    cooldownTimer = dashCooldown;
                    fillAmountDash = 0;
                    isDashed = true;
                    nextDashTime = Time.time + dashCooldown;
                    yield return null;
                }
            }
        }
        #endregion

        #region No Key Pressed Dash
        if (Time.time > nextDashTime)
        {
            if (wasdPressed == false)
            {
                while (Time.time < dashStartTime + dashTime)
                {
                    transform.Translate(Vector3.forward * dashSpeed);
                    dashEffect.Play();
                    cooldownTimer = dashCooldown;
                    fillAmountDash = 0;
                    isDashed = true;
                    nextDashTime = Time.time + dashCooldown;
                    yield return null;
                }
            }
        }
        #endregion
    }
    public void DashCooldown()
    {
        cooldownTimer -= Time.deltaTime;
        fillAmountDash += Time.deltaTime;
        if (cooldownTimer <= 0.0f)
        {
            isDashed = false;
            dashImage.fillAmount = 1.0f;
            dashReadySource.Play();
        }
        else
        {
            dashImage.fillAmount = fillAmountDash / dashCooldown;
        }
    }
}
