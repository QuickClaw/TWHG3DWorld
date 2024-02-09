using UnityEngine;
using UnityEngine.EventSystems;

public class HoverUIElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject infoPanel;

    private void Start()
    {
        infoPanel.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        infoPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        infoPanel.SetActive(false);
    }
}