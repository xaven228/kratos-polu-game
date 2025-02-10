using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restartsmert : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // ���������, �������� �� ����� ������� � ���� ��������
        if (other.CompareTag("Player"))
        {
            // ������������� ����
            RestartGame();
        }

    }

    void RestartGame()
    {
        // ����� ����� �������� ����� ������, ������� ������ ���� ��������� ����� ������������ ����
        // ��������, �������� ��������, �������� ������� ������ � �. �.

        // ������������� �����
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

}
