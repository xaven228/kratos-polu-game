using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyObject : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Проверяем, является ли объект, с которым произошло столкновение, тем, который мы хотим уничтожить
        if (collision.gameObject.CompareTag("Thunder"))
        {

                // Уничтожаем объект, с которым столкнулись
                Destroy(collision.gameObject);
        }
    }
}
