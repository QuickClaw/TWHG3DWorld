using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillPanel : MonoBehaviour
{
    public Player Player;
    public Movement Movement;
    public Potions Potions;
    public PauseMenu PauseMenu;
    public TPS TPS;
    public StarPickup StarPickup;

    public Button speedBtn, dashCdBtn, potionDurationBtn;

    public TMP_Text txtSpeed;
    public TMP_Text txtDashCd;
    public TMP_Text txtPotionDuration;

    //Static upgrade level yazýlarý
    public TMP_Text txtSpeedLvl;
    public TMP_Text txtDashCdLvl;
    public TMP_Text txtPotionDurationLvl;

    // Upgrade için gereken yýldýz
    public int speedPrice;
    public int dashCdPrice;
    public int potionDurationPrice;

    // Ne kadar upgradelenecek
    public float speedUpgrade;
    public float dashCdUpgrade;
    public float potionDurationUpgrade;

    // Player attributes
    public TMP_Text currentSpeed;
    public TMP_Text currentDashCd;
    public TMP_Text currentPotionDuration;
    public TMP_Text currentLevel;

    public GameObject skillPanel;
    public bool isSkillPanelOpen;

    void Start()
    {
        #region PlayerPrefs upgrade ayarlarý
        // Speed
        if (PlayerPrefs.HasKey("playerSpeed"))
        {
            Movement.playerSpeed = PlayerPrefs.GetFloat("playerSpeed");
            Movement.beforeMudPlayerSpeed = PlayerPrefs.GetFloat("beforeMudPlayerSpeed");
            currentSpeed.text = Movement.playerSpeed.ToString("f1");
        }
        else
        {
            Movement.playerSpeed = PlayerPrefs.GetFloat("playerSpeed", 5);
            Movement.beforeMudPlayerSpeed = PlayerPrefs.GetFloat("beforeMudPlayerSpeed", 5);
            currentSpeed.text = Movement.playerSpeed.ToString("f1");
        }

        // Dash Cooldown
        if (PlayerPrefs.HasKey("playerDashCooldown"))
        {
            Movement.dashCooldown = PlayerPrefs.GetFloat("playerDashCooldown");
        }
        else
        {
            Movement.dashCooldown = PlayerPrefs.GetFloat("playerDashCooldown", 2);
        }

        // Stamina Potion Duration
        if (PlayerPrefs.HasKey("staminaPotionDuration"))
        {
            Potions.staminaPotionDuration = PlayerPrefs.GetFloat("staminaPotionDuration");
            currentPotionDuration.text = Potions.staminaPotionDuration.ToString() + " sec";
        }
        else
        {
            Potions.staminaPotionDuration = PlayerPrefs.GetFloat("staminaPotionDuration", 30);
            currentPotionDuration.text = Potions.staminaPotionDuration.ToString() + " sec";
        }
        #endregion

        #region PlayerPrefs upgrade level ayarlarý
        // Speed level
        if (PlayerPrefs.HasKey("speedLvl"))
        {
            Player.speedLvl = PlayerPrefs.GetInt("speedLvl");
            txtSpeedLvl.text = "Lv." + Player.speedLvl.ToString();
        }
        else
        {
            Player.speedLvl = PlayerPrefs.GetInt("speedLvl", 1);
            txtSpeedLvl.text = "Lv." + Player.speedLvl.ToString();
        }

        // Dash Cooldown level
        if (PlayerPrefs.HasKey("dashCdLvl"))
        {
            Player.dashCdLvl = PlayerPrefs.GetInt("dashCdLvl");
            txtDashCdLvl.text = "Lv." + Player.dashCdLvl.ToString();
        }
        else
        {
            Player.dashCdLvl = PlayerPrefs.GetInt("dashCdLvl", 1);
            txtDashCdLvl.text = "Lv." + Player.dashCdLvl.ToString();
        }

        // Stamina Potion Duration level
        if (PlayerPrefs.HasKey("potionDurationLvl"))
        {
            Player.potionDurationLvl = PlayerPrefs.GetInt("potionDurationLvl");
            txtPotionDurationLvl.text = "Lv." + Player.potionDurationLvl.ToString();
        }
        else
        {
            Player.potionDurationLvl = PlayerPrefs.GetInt("potionDurationLvl", 1);
            txtPotionDurationLvl.text = "Lv." + Player.potionDurationLvl.ToString();
        }
        #endregion       

        #region PlayerPrefs upgrade price ayarlarý
        // Speed price
        if (PlayerPrefs.HasKey("speedPrice"))
        {
            speedPrice = PlayerPrefs.GetInt("speedPrice");
            txtSpeed.text = speedPrice.ToString();
        }
        else
        {
            speedPrice = PlayerPrefs.GetInt("speedPrice", 10);
            txtSpeed.text = speedPrice.ToString();
        }

        // Dash Cooldown price
        if (PlayerPrefs.HasKey("dashCdPrice"))
        {
            dashCdPrice = PlayerPrefs.GetInt("dashCdPrice");
            txtDashCd.text = dashCdPrice.ToString();
        }
        else
        {
            dashCdPrice = PlayerPrefs.GetInt("dashCdPrice", 10);
            txtDashCd.text = dashCdPrice.ToString();
        }

        // Stamina Potion Duration price
        if (PlayerPrefs.HasKey("potionDurationPrice"))
        {
            potionDurationPrice = PlayerPrefs.GetInt("potionDurationPrice");
            txtPotionDuration.text = potionDurationPrice.ToString();
        }
        else
        {
            potionDurationPrice = PlayerPrefs.GetInt("potionDurationPrice", 7);
            txtPotionDuration.text = potionDurationPrice.ToString();
        }
        #endregion

        #region PlayerPrefs player level ayarlarý
        if (PlayerPrefs.HasKey("playerLevel"))
        {
            Player.level = PlayerPrefs.GetInt("playerLevel");
            currentLevel.text = "Level " + Player.level.ToString();
        }
        else
        {
            Player.level = PlayerPrefs.GetInt("playerLevel", 1);
            currentLevel.text = "Level " + Player.level.ToString();
        }
        #endregion

        skillPanel.SetActive(false);
        isSkillPanelOpen = false;

        currentDashCd.text = Movement.dashCooldown.ToString("f1") + " sec";
    }

    void Update()
    {      
        if (Player.star >= speedPrice && Player.speedLvl < 20)
            speedBtn.interactable = true;
        else
            speedBtn.interactable = false;

        if (Player.star >= dashCdPrice && Potions.staminaTaken == false && Player.dashCdLvl < 20)
            dashCdBtn.interactable = true;
        else
            dashCdBtn.interactable = false;

        if (Player.star >= potionDurationPrice && Player.potionDurationLvl < 20)
            potionDurationBtn.interactable = true;
        else
            potionDurationBtn.interactable = false;

        if (Player.speedLvl >= 20)
        {
            txtSpeed.text = "Max";
            speedBtn.interactable = false;
        }

        if (Player.dashCdLvl >= 10)
        {
            txtDashCd.text = "Max";
            dashCdBtn.interactable = false;
        }

        if (Player.potionDurationLvl >= 10)
        {
            txtPotionDuration.text = "Max";
            potionDurationBtn.interactable = false;
        }

        // Skill Panel açar, kapatýr
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isSkillPanelOpen == false && PauseMenu.isPaused == false)
                OpenSkillPanel();
            else
                CloseSkillPanel();
        }
    }

    public void UpgradeSpeed()
    {
        if (Player.star >= speedPrice)
        {
            speedBtn.interactable = true;

            Movement.playerSpeed += speedUpgrade;
            Movement.beforeMudPlayerSpeed += speedUpgrade;
            PlayerPrefs.SetFloat("playerSpeed", Movement.playerSpeed);
            PlayerPrefs.SetFloat("beforeMudPlayerSpeed", Movement.beforeMudPlayerSpeed);

            Player.speedLvl += 1;
            PlayerPrefs.SetInt("speedLvl", Player.speedLvl);

            if (Player.speedLvl >= 20)
            {
                txtSpeed.text = "Max";
                speedBtn.interactable = false;
            }

            txtSpeedLvl.text = "Lv." + Player.speedLvl.ToString();
            currentSpeed.text = Movement.playerSpeed.ToString("f1");

            Player.star -= speedPrice;
            PlayerPrefs.SetInt("playerStar", Player.star);
            StarPickup.txtStarCount.text = Player.star.ToString();

            speedPrice += 3;
            txtSpeed.text = speedPrice.ToString();
            PlayerPrefs.SetInt("speedPrice", speedPrice);
        }
        else
        {
            speedBtn.interactable = false;
        }
    }

    public void UpgradeDashCd()
    {
        if (Player.star >= dashCdPrice && Potions.staminaTaken == false)
        {
            dashCdBtn.interactable = true;
            Movement.dashCooldown -= dashCdUpgrade;
            Movement.beforeMudDashCooldown -= dashCdUpgrade;
            PlayerPrefs.SetFloat("playerDashCooldown", Movement.dashCooldown);
            Debug.Log(PlayerPrefs.GetFloat("playerDashCooldown"));

            Player.dashCdLvl += 1;
            PlayerPrefs.SetInt("dashCdLvl", Player.dashCdLvl);

            if (Player.dashCdLvl >= 20)
            {
                txtDashCd.text = "Max";
                dashCdBtn.interactable = false;
            }

            txtDashCdLvl.text = "Lv." + Player.dashCdLvl.ToString();
            currentDashCd.text = Movement.dashCooldown.ToString("f1") + " sec";

            Player.star -= dashCdPrice;
            PlayerPrefs.SetInt("playerStar", Player.star);
            StarPickup.txtStarCount.text = Player.star.ToString();

            dashCdPrice += 3;
            txtDashCd.text = dashCdPrice.ToString();
            PlayerPrefs.SetInt("dashCdPrice", dashCdPrice);
        }
        else
        {
            dashCdBtn.interactable = false;
        }
    }

    public void UpgradePotionDuration()
    {
        if (Player.star >= potionDurationPrice)
        {
            Potions.staminaPotionDuration += potionDurationUpgrade;
            PlayerPrefs.SetFloat("staminaPotionDuration", Potions.staminaPotionDuration);

            Player.potionDurationLvl += 1;
            PlayerPrefs.SetInt("potionDurationLvl", Player.potionDurationLvl);

            txtPotionDurationLvl.text = "Lv." + Player.potionDurationLvl.ToString();
            currentPotionDuration.text = Potions.staminaPotionDuration.ToString() + " sec";

            Player.star -= potionDurationPrice;
            PlayerPrefs.SetInt("playerStar", Player.star);
            StarPickup.txtStarCount.text = Player.star.ToString();

            potionDurationPrice += 3;
            txtPotionDuration.text = potionDurationPrice.ToString();
            PlayerPrefs.SetInt("potionDurationPrice", potionDurationPrice);
        }
        else
        {
            potionDurationBtn.interactable = false;
        }
    }

    private void OpenSkillPanel()
    {
        skillPanel.SetActive(true);
        isSkillPanelOpen = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        TPS.enabled = false;

        if (Potions.staminaTaken)
        {
            txtDashCd.fontSize = 12;
            txtDashCd.text = "Potion Up";
        }
        else
        {
            txtDashCd.fontSize = 24;
            txtDashCd.text = dashCdPrice.ToString();
        }
    }

    private void CloseSkillPanel()
    {
        skillPanel.SetActive(false);
        isSkillPanelOpen = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        TPS.enabled = true;
    }
}
