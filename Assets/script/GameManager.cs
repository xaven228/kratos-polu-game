using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
        // Показать курсор и разблокировать его
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // Загрузить сцену проигрыша
        SceneManager.LoadScene("GameOverScene");
    }

    public void WinGame()
    {
        Debug.Log("You Win!");
        // Показать курсор и разблокировать его
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // Загрузить сцену победы
        SceneManager.LoadScene("WinScene");
    }
}