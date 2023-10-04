using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    private static GameObject instance;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
            instance = gameObject;
        else
            Destroy(gameObject);
    }
}
