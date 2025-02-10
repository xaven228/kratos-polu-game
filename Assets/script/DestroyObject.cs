using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyObject : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // ���������, �������� �� ������, � ������� ��������� ������������, ���, ������� �� ����� ����������
        if (collision.gameObject.CompareTag("Thunder"))
        {

                // ���������� ������, � ������� �����������
                Destroy(collision.gameObject);
        }
    }
}
