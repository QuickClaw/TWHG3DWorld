using UnityEngine;
using UnityEngine.UI;

public class Journal : MonoBehaviour
{
    public TPS TPS;

    public GameObject journal, txtQuests;
    public Scrollbar journalScrollbar;

    public bool isJournalOpen;

    void Start()
    {
        journal.SetActive(false);
        txtQuests.SetActive(false);
        isJournalOpen = false;
    }

    void Update()
    {
        // Journal açar, kapatýr
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (isJournalOpen == false)
                OpenJournal();
            else
                CloseJournal();
        }
    }

    private void OpenJournal()
    {
        journal.SetActive(true);
        isJournalOpen = true;
        txtQuests.SetActive(true);
        journalScrollbar.value = 1;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        TPS.enabled = false;
    }

    private void CloseJournal()
    {
        journal.SetActive(false);
        isJournalOpen = false;
        txtQuests.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        TPS.enabled = true;
    }
}
