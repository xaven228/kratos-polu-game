using UnityEngine;
using UnityEngine.UI;

public class ExitGameScript : MonoBehaviour
{
    public Button exitButton;

    void Start()
    {
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(ExitGame);
        }
        else
        {
            Debug.LogError("Exit button is not assigned.");
        }
    }

    void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}