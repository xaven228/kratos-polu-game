using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform playerTransform; // Ссылка на игрока
    public GameObject projectilePrefab; // Префаб снаряда
    public float fireInterval = 1f; // Интервал стрельбы
    public float projectileSpeed = 10f; // Скорость снаряда
    private float timeSinceLastFire = 0f;

    void Update()
    {
        LookAtPlayer();
        FireProjectiles();
    }

    void LookAtPlayer()
    {
        if (playerTransform == null)
        {
            Debug.LogError("Player Transform is not assigned.");
            return;
        }

        Vector3 direction = playerTransform.position - transform.position;
        direction.y = 0; // Игнорируем вертикальное направление
        direction.Normalize();

        // Поворот головы врага в сторону игрока
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f); // Плавный поворот
    }

    void FireProjectiles()
    {
        timeSinceLastFire += Time.deltaTime;
        if (timeSinceLastFire >= fireInterval)
        {
            timeSinceLastFire = 0f;
            FireInDirection(playerTransform.position - transform.position);
        }
    }

    void FireInDirection(Vector3 direction)
    {
        // Добавляем небольшое отклонение для промахов
        Vector3 randomOffset = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
        Vector3 adjustedDirection = (direction + randomOffset).normalized;

        // Создаем снаряд чуть впереди головы врага
        Vector3 spawnPosition = transform.position + transform.forward * 1f; // Снаряд вылетает чуть впереди головы врага
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.LookRotation(adjustedDirection));
        ProjectileController projectileController = projectile.GetComponent<ProjectileController>();
        if (projectileController != null)
        {
            projectileController.targetType = ProjectileController.TargetType.Player; // Устанавливаем цель как игрока
            projectileController.Initialize(adjustedDirection, projectileSpeed);
        }
        // Уничтожить снаряд через 2 секунды
        Destroy(projectile, 2f);
    }
}