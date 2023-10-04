using UnityEngine;
using TMPro;

public class Sign : MonoBehaviour
{
    public string questTitle, questDesc, difficulty;
    public int exp, star;

    public TMP_Text txtTitle, txtDesc, txtExp, txtStar, txtDifficulty;

    public GameObject done, doneIcon, signIcon;
    public GameObject doneIconMinimap, signIconMinimap;

    void Start()
    {
        txtTitle.text = questTitle;
        txtDesc.text = questDesc;
        txtExp.text = exp.ToString();
        txtStar.text = star.ToString();
        txtDifficulty.text = difficulty;
    }
}
