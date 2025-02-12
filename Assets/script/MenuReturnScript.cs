using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuReturnScript : MonoBehaviour
{
    public string menuSceneName = "MainMenu"; // Имя сцены меню

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMenu();
        }
    }

    void ReturnToMenu()
    {
        // Показать курсор и разблокировать его
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(menuSceneName);
    }
}