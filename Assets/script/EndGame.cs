using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{

    // Метод для уничтожения игрока
    public void DestroyPlayer()
    {
        if (gameObject != null)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Игрок умер!");

        // Запускаем эффект разрушения

        // Уничтожаем объект игрока
        Destroy(gameObject);

        // Запускаем корутину для задержки перед переходом на другую сцену
    }


    private void OnCollisionEnter(Collision collision)
    {
        // Проверяем, является ли объект, с которым столкнулся игрок, врагом
        if (collision.gameObject.CompareTag("Thunder"))
        {
            DestroyPlayer(); // Вызываем метод DestroyPlayer
            SceneManager.LoadScene(2);
        }
    }

}
