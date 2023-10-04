using UnityEngine;
using TMPro;

public class Potions : MonoBehaviour
{
    public PotionSelect Potion = new PotionSelect();
    public Movement Movement;
    public MudDebuff MudDebuff;
    public ParticleSystem potionEffect, shieldEffect, mudResistanceEffect, staminaEffect, dashCdTextEffect, mudResistanceTextEffect, shieldTextEffect;
    public AudioClip potionDownSound, shieldSound, mudResistanceSound, staminaSound;
    public AudioSource potionDownAudioSource, shieldAudioSource, mudResistanceAudioSource, staminaAudioSource;

    public TMP_Text staminaDuration, shieldDuration, mudResistanceDuration;
    public TMP_Text txtMinimapMudPotionTime, txtMinimapShieldPotionTime, txtMinimapStaminaPotionTime;

    public bool staminaTaken, mudResistanceTaken, shieldTaken;
    public float resetTime, dashCdBuffPercentage;
    public float staminaPotionDuration, mudResistancePotionDuration, shieldPotionDuration;
    public float staminaUsing, mudResistanceUsing, shieldUsing;
    private float beforeDashBuff;

    public float prebuffDashCd;

    public enum PotionSelect
    {
        Stamina,
        MudResistance,
        Shield
    }

    void Start()
    {
        potionDownAudioSource.clip = potionDownSound;
        shieldAudioSource.clip = shieldSound;
        mudResistanceAudioSource.clip = mudResistanceSound;
        staminaAudioSource.clip = staminaSound;

        staminaTaken = false;
        mudResistanceTaken = false;
        shieldTaken = false;

        staminaUsing = staminaPotionDuration;
        mudResistanceUsing = mudResistancePotionDuration;
        shieldUsing = shieldPotionDuration;

        prebuffDashCd = Movement.dashCooldown;
    }

    void Update()
    {
        // Alýnan potion süresini her saniye azaltýr
        if (mudResistanceTaken)
        {
            txtMinimapMudPotionTime.text = mudResistanceUsing.ToString("F0");
            mudResistanceDuration.text = mudResistanceUsing.ToString("F0");
            mudResistanceUsing -= Time.deltaTime;
            resetTime = mudResistancePotionDuration;
        }

        if (staminaTaken)
        {
            txtMinimapStaminaPotionTime.text = staminaUsing.ToString("F0");
            staminaDuration.text = staminaUsing.ToString("F0");
            staminaUsing -= Time.deltaTime;
            resetTime = staminaPotionDuration;
        }

        if (shieldTaken)
        {
            txtMinimapShieldPotionTime.text = shieldUsing.ToString("F0");
            shieldDuration.text = shieldUsing.ToString("F0");
            shieldUsing -= Time.deltaTime;
            resetTime = shieldPotionDuration;
        }
        else
        {
            CancelInvoke("ShieldExpire");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Potion == PotionSelect.Stamina)
        {
            if (staminaTaken == false)
            {
                staminaTaken = true;
                staminaUsing = staminaPotionDuration;

                potionEffect.Play();
                staminaEffect.Play();
                staminaAudioSource.Play();
                dashCdTextEffect.Play();

                beforeDashBuff = (Movement.dashCooldown * dashCdBuffPercentage) / 100;
                Movement.dashCooldown -= (Movement.dashCooldown * dashCdBuffPercentage) / 100;

                Invoke("StaminaExpire", staminaPotionDuration);
            }
            else
            {
                CancelInvoke("StaminaExpire");
                staminaDuration.text = staminaPotionDuration.ToString();
                staminaUsing = staminaPotionDuration;

                staminaAudioSource.Play();
                potionEffect.Play();

                Invoke("StaminaExpire", staminaPotionDuration);
            }
        }

        if (Potion == PotionSelect.MudResistance)
        {
            if (mudResistanceTaken == false)
            {
                mudResistanceTaken = true;
                mudResistanceUsing = mudResistancePotionDuration;

                mudResistanceAudioSource.Play();
                potionEffect.Play();
                mudResistanceEffect.Play();
                mudResistanceTextEffect.Play();

                Invoke("MudResistanceExpire", mudResistancePotionDuration);
            }
            else
            {
                CancelInvoke("MudResistanceExpire");
                mudResistanceDuration.text = mudResistanceUsing.ToString();
                mudResistanceUsing = mudResistancePotionDuration;

                mudResistanceAudioSource.Play();
                potionEffect.Play();

                Invoke("MudResistanceExpire", mudResistancePotionDuration);
            }
        }

        if (Potion == PotionSelect.Shield)
        {
            if (shieldTaken == false)
            {
                shieldTaken = true;
                shieldUsing = shieldPotionDuration;

                shieldAudioSource.Play();
                potionEffect.Play();
                shieldEffect.Play();
                shieldTextEffect.Play();

                Invoke("ShieldExpire", shieldPotionDuration);
            }
            else
            {
                CancelInvoke("ShieldExpire");
                shieldUsing = shieldPotionDuration;
                shieldDuration.text = shieldUsing.ToString();

                shieldAudioSource.Play();
                potionEffect.Play();

                Invoke("ShieldExpire", shieldPotionDuration);
            }
        }
    }

    public void StaminaExpire()
    {
        if (staminaTaken)
        {
            Movement.dashCooldown += beforeDashBuff;
            staminaTaken = false;

            staminaDuration.text = "0";
            txtMinimapStaminaPotionTime.text = "";
            staminaEffect.Stop();
            potionDownAudioSource.Play();
        }
    }
    public void MudResistanceExpire()
    {
        if (mudResistanceTaken)
        {
            mudResistanceTaken = false;

            mudResistanceDuration.text = "0";
            txtMinimapMudPotionTime.text = "";

            mudResistanceEffect.Stop();
            potionDownAudioSource.Play();
        }
    }
    public void ShieldExpire()
    {
        if (shieldTaken)
        {
            shieldTaken = false;

            shieldDuration.text = "0";
            txtMinimapShieldPotionTime.text = "";

            shieldEffect.Stop();
            potionDownAudioSource.Play();
        }
    }
}
