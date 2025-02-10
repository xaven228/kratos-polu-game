using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{

    // ����� ��� ����������� ������
    public void DestroyPlayer()
    {
        if (gameObject != null)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("����� ����!");

        // ��������� ������ ����������

        // ���������� ������ ������
        Destroy(gameObject);

        // ��������� �������� ��� �������� ����� ��������� �� ������ �����
    }


    private void OnCollisionEnter(Collision collision)
    {
        // ���������, �������� �� ������, � ������� ���������� �����, ������
        if (collision.gameObject.CompareTag("Thunder"))
        {
            DestroyPlayer(); // �������� ����� DestroyPlayer
            SceneManager.LoadScene(2);
        }
    }

}
