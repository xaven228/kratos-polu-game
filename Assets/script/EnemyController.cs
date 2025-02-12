using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform playerTransform; // Ссылка на игрока
    public float speed = 2f; // Скорость движения врага
    public GameObject projectilePrefab; // Префаб снаряда
    public float fireInterval = 2f; // Интервал стрельбы
    public float projectileSpeed = 10f; // Скорость снаряда
    public float minDistanceToPlayer = 5f; // Минимальное расстояние до игрока
    public float attackRange = 10f; // Дальность атаки
    private float timeSinceLastFire = 0f;

    void Update()
    {
        FollowPlayer();
        FireProjectiles();
    }

    void FollowPlayer()
    {
        if (playerTransform == null)
        {
            Debug.LogError("Player Transform is not assigned.");
            return;
        }

        Vector3 direction = playerTransform.position - transform.position;
        float distanceToPlayer = direction.magnitude;

        if (distanceToPlayer > minDistanceToPlayer && distanceToPlayer <= attackRange)
        {
            direction.Normalize();
            transform.Translate(direction * speed * Time.deltaTime, Space.World);

            // Поворот врага в сторону игрока
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f); // Плавный поворот
        }
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

        // Создаем снаряд чуть впереди тела врага
        Vector3 spawnPosition = transform.position + transform.forward * 1f; // Снаряд вылетает чуть впереди врага
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
        }
    }

    private void OnDestroy()
    {
        // Вызываем метод WinGame при уничтожении врага
        GameManager.Instance.WinGame();
    }
}