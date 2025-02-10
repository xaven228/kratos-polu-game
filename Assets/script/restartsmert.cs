using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restartsmert : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Проверяем, коснулся ли игрок объекта с этим скриптом
        if (other.CompareTag("Player"))
        {
            // Перезапускаем игру
            RestartGame();
        }

    }

    void RestartGame()
    {
        // Здесь можно добавить любую логику, которая должна быть выполнена перед перезапуском игры
        // Например, сбросить счётчики, сбросить позицию игрока и т. д.

        // Перезапускаем сцену
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

}
