using System.Collections;
using UnityEngine;
using UnityEngine.Experimental;

public class SpawnMolnii : MonoBehaviour
{
    public GameObject objectToSpawn; // Префаб объекта, который нужно спавнить
    public float minSpawnInterval = 1f; // Минимальный интервал спавна
    public float maxSpawnInterval = 5f; // Максимальный интервал спавна
    public float spawnDistance = 2f; // Расстояние от объекта вниз, где будет спавниться объект
    public int minObjectsToSpawn = 1; // Минимальное количество объектов для спавна
    public int maxObjectsToSpawn = 5; // Максимальное количество объектов для спавна
    public float additionalFallForce = 10f; // Дополнительная сила для ускорения падения

    private void Start()
    {
        // Запускаем корутину спавна
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            // Вычисляем случайный интервал спавна
            float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(spawnInterval);

            // Генерируем случайное количество объектов для спавна
            int numberOfObjectsToSpawn = Random.Range(minObjectsToSpawn, maxObjectsToSpawn + 1);

            // Спавним несколько объектов
            for (int i = 0; i < numberOfObjectsToSpawn; i++)
            {
                // Вычисляем позицию спавна под объектом
                Vector3 spawnPosition = transform.position - new Vector3(0, spawnDistance, 0) + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));

                // Спавним объект
                GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

                // Получаем компонент Rigidbody
                Rigidbody rb = spawnedObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    // Применяем дополнительную силу вниз
                    rb.AddForce(Vector3.down * additionalFallForce, ForceMode.Impulse);
                }
            }
        }
    }
}
