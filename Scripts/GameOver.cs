using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour, IPointerClickHandler
{
    public Animation finishAnim;

    public void LastScene()
    {     
        SceneManager.LoadScene(6);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        finishAnim.Play();
        Invoke("LastScene", 6);
    }
}
