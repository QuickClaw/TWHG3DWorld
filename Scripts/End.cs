using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    public TPS TPS;
    public Movement Movement;
    public Player Player;
    public AddToJournal AddToJournal;

    public GameObject endPanel;

    public TMP_Text txtRegionReward, deactivateText;
    public TMP_Text txtRemainingStarsParent, txtRemainingStarsChild;

    public Collider endCollider;

    public Button endPanelButton;

    public static int buildIndex = 0;
    public int regionReward;

    public bool isEndPanelActive;
    public bool endPanelOpened;

    public Animation cursorInfoAnim;

    void Start()
    {
        buildIndex = SceneManager.GetActiveScene().buildIndex;

        Player.remainingStarsTheForest = PlayerPrefs.GetInt("remainingStarsTheForest");
        Player.remainingStarsSnowy = PlayerPrefs.GetInt("remainingStarsSnowy");
        Player.remainingStarsTheDesert = PlayerPrefs.GetInt("remainingStarsTheDesert");
        Player.remainingStarsFoggy = PlayerPrefs.GetInt("remainingStarsFoggy");
        Player.remainingStarsInvisible = PlayerPrefs.GetInt("remainingStarsInvisible");

        endPanelOpened = false;
        endPanel.SetActive(false);
        isEndPanelActive = false;
        txtRegionReward.text = regionReward.ToString();
    }

    private void Update()
    {
        #region The Forest
        if (Player.sceneName == "The Forest")
        {
            if (endPanelOpened == false)
            {
                if (Player.questTheForest == 6)
                    deactivateText.gameObject.SetActive(true);
                else
                    deactivateText.gameObject.SetActive(false);
            }
        }
        #endregion

        #region Snowy
        if (Player.sceneName == "Snowy")
        {
            if (endPanelOpened == false)
            {
                if (Player.questSnowy == 8)
                    deactivateText.gameObject.SetActive(true);
                else
                    deactivateText.gameObject.SetActive(false);
            }
        }
        #endregion

        #region The Desert
        if (Player.sceneName == "The Desert")
        {
            if (endPanelOpened == false)
            {
                if (Player.questTheDesert == 12)
                    deactivateText.gameObject.SetActive(true);
                else
                    deactivateText.gameObject.SetActive(false);
            }
        }
        #endregion

        #region Foggy
        if (Player.sceneName == "Foggy")
        {
            if (endPanelOpened == false)
            {
                if (Player.questFoggy == 3)
                    deactivateText.gameObject.SetActive(true);
                else
                    deactivateText.gameObject.SetActive(false);
            }
        }
        #endregion

        #region Invisible
        if (Player.sceneName == "Invisible")
        {
            if (endPanelOpened == false)
            {
                if (Player.questInvisible == 3)
                    deactivateText.gameObject.SetActive(true);
                else
                    deactivateText.gameObject.SetActive(false);
            }
        }
        #endregion
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag is "Player")
        {
            deactivateText.gameObject.SetActive(false);

            endPanel.SetActive(true);

            isEndPanelActive = true;
            endPanelOpened = true;

            endPanelButton.gameObject.SetActive(true);

            // Haritaya göre kalan yýldýz sayýsý
            #region The Forest
            if (Player.sceneName == "The Forest")
            {
                if (Player.remainingStarsTheForest <= 0)
                {
                    txtRemainingStarsParent.gameObject.SetActive(false);
                    txtRemainingStarsChild.gameObject.SetActive(false);
                }
                else
                {
                    txtRemainingStarsChild.text = Player.remainingStarsTheForest.ToString();
                    txtRemainingStarsParent.gameObject.SetActive(true);
                    txtRemainingStarsChild.gameObject.SetActive(true);
                }
            }
            #endregion

            #region Snowy
            if (Player.sceneName == "Snowy")
            {
                if (Player.remainingStarsSnowy <= 0)
                {
                    txtRemainingStarsParent.gameObject.SetActive(false);
                    txtRemainingStarsChild.gameObject.SetActive(false);
                }
                else
                {
                    txtRemainingStarsChild.text = Player.remainingStarsSnowy.ToString();
                    txtRemainingStarsParent.gameObject.SetActive(true);
                    txtRemainingStarsChild.gameObject.SetActive(true);
                }
            }
            #endregion

            #region The Desert
            if (Player.sceneName == "The Desert")
            {
                if (Player.remainingStarsTheDesert <= 0)
                {
                    txtRemainingStarsParent.gameObject.SetActive(false);
                    txtRemainingStarsChild.gameObject.SetActive(false);
                }
                else
                {
                    txtRemainingStarsChild.text = Player.remainingStarsTheDesert.ToString();
                    txtRemainingStarsParent.gameObject.SetActive(true);
                    txtRemainingStarsChild.gameObject.SetActive(true);
                }
            }
            #endregion

            #region Foggy
            if (Player.sceneName == "Foggy")
            {
                if (Player.remainingStarsFoggy <= 0)
                {
                    txtRemainingStarsParent.gameObject.SetActive(false);
                    txtRemainingStarsChild.gameObject.SetActive(false);
                }
                else
                {
                    txtRemainingStarsChild.text = Player.remainingStarsFoggy.ToString();
                    txtRemainingStarsParent.gameObject.SetActive(true);
                    txtRemainingStarsChild.gameObject.SetActive(true);
                }
            }
            #endregion

            #region Invisible
            if (Player.sceneName == "Invisible")
            {
                if (Player.remainingStarsInvisible <= 0)
                {
                    txtRemainingStarsParent.gameObject.SetActive(false);
                    txtRemainingStarsChild.gameObject.SetActive(false);
                }
                else
                {
                    txtRemainingStarsChild.text = Player.remainingStarsInvisible.ToString();
                    txtRemainingStarsParent.gameObject.SetActive(true);
                    txtRemainingStarsChild.gameObject.SetActive(true);
                }
            }
            #endregion

            TPS.enabled = false;
            Movement.enabled = false;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag is "Player")
        {
            endCollider.enabled = false;
            deactivateText.gameObject.SetActive(false);
        }
    }

    public void OpenEndPanel()
    {
        deactivateText.gameObject.SetActive(false);
        endPanel.SetActive(true);
        isEndPanelActive = true;
        TPS.enabled = false;
        Movement.enabled = false;

        // Haritaya göre kalan yýldýz sayýsý
        #region The Forest
        if (Player.sceneName == "The Forest")
        {
            if (Player.remainingStarsTheForest <= 0)
            {
                txtRemainingStarsChild.gameObject.SetActive(false);
            }
            else
            {
                txtRemainingStarsChild.text = Player.remainingStarsTheForest.ToString();
                txtRemainingStarsChild.gameObject.SetActive(true);
            }
        }
        #endregion

        #region Snowy
        if (Player.sceneName == "Snowy")
        {
            if (Player.remainingStarsSnowy <= 0)
            {
                txtRemainingStarsChild.gameObject.SetActive(false);
            }
            else
            {
                txtRemainingStarsChild.text = Player.remainingStarsSnowy.ToString();
                txtRemainingStarsChild.gameObject.SetActive(true);
            }
        }
        #endregion

        #region The Desert
        if (Player.sceneName == "The Desert")
        {
            if (Player.remainingStarsTheDesert <= 0)
            {
                txtRemainingStarsChild.gameObject.SetActive(false);
            }
            else
            {
                txtRemainingStarsChild.text = Player.remainingStarsTheDesert.ToString();
                txtRemainingStarsChild.gameObject.SetActive(true);
            }
        }
        #endregion

        #region Foggy
        if (Player.sceneName == "Foggy")
        {
            if (Player.remainingStarsFoggy <= 0)
            {
                txtRemainingStarsChild.gameObject.SetActive(false);
            }
            else
            {
                txtRemainingStarsChild.text = Player.remainingStarsFoggy.ToString();
                txtRemainingStarsChild.gameObject.SetActive(true);
            }
        }
        #endregion

        #region Invisible
        if (Player.sceneName == "Invisible")
        {
            if (Player.remainingStarsInvisible <= 0)
            {
                txtRemainingStarsChild.gameObject.SetActive(false);
            }
            else
            {
                txtRemainingStarsChild.text = Player.remainingStarsInvisible.ToString();
                txtRemainingStarsChild.gameObject.SetActive(true);
            }
        }
        #endregion

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseEndPanel()
    {
        endPanel.SetActive(false);
        isEndPanelActive = false;
        TPS.enabled = true;
        Movement.enabled = true;
    }

    public void NextRegion()
    {
        Player.star += regionReward;
        PlayerPrefs.SetInt("playerStar", Player.star);

        int saveIndex = PlayerPrefs.GetInt("saveIndex");

        if (buildIndex > saveIndex)
            PlayerPrefs.SetInt("saveIndex", buildIndex);

        PlayerPrefs.SetInt("buildIndex", buildIndex);
    }

    public void ContinueToExplore()
    {
        CloseEndPanel();
        cursorInfoAnim.Play();
        deactivateText.gameObject.SetActive(false);
    }
}
