using System.Collections;
using UnityEngine;
using UnityEngine.Experimental;

public class SpawnMolnii : MonoBehaviour
{
    public GameObject objectToSpawn; // ������ �������, ������� ����� ��������
    public float minSpawnInterval = 1f; // ����������� �������� ������
    public float maxSpawnInterval = 5f; // ������������ �������� ������
    public float spawnDistance = 2f; // ���������� �� ������� ����, ��� ����� ���������� ������
    public int minObjectsToSpawn = 1; // ����������� ���������� �������� ��� ������
    public int maxObjectsToSpawn = 5; // ������������ ���������� �������� ��� ������
    public float additionalFallForce = 10f; // �������������� ���� ��� ��������� �������

    private void Start()
    {
        // ��������� �������� ������
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            // ��������� ��������� �������� ������
            float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(spawnInterval);

            // ���������� ��������� ���������� �������� ��� ������
            int numberOfObjectsToSpawn = Random.Range(minObjectsToSpawn, maxObjectsToSpawn + 1);

            // ������� ��������� ��������
            for (int i = 0; i < numberOfObjectsToSpawn; i++)
            {
                // ��������� ������� ������ ��� ��������
                Vector3 spawnPosition = transform.position - new Vector3(0, spawnDistance, 0) + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));

                // ������� ������
                GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

                // �������� ��������� Rigidbody
                Rigidbody rb = spawnedObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    // ��������� �������������� ���� ����
                    rb.AddForce(Vector3.down * additionalFallForce, ForceMode.Impulse);
                }
            }
        }
    }
}
