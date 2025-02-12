using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform[] waypoints; // Точки, между которыми будет двигаться враг
    public float speed = 2f; // Скорость движения врага
    public GameObject projectilePrefab; // Префаб снаряда
    public float fireInterval = 2f; // Интервал стрельбы
    public float projectileSpeed = 5f; // Скорость снаряда

    private int currentWaypointIndex = 0;
    private float timeSinceLastFire = 0f;

    void Update()
    {
        MoveEnemy();
        FireProjectiles();
    }

    void MoveEnemy()
    {
        if (waypoints.Length == 0) return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = targetWaypoint.position - transform.position;

        if (direction.magnitude < 0.1f)
        {
            // Переключиться на следующую точку
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
        else
        {
            // Двигаться к текущей точке
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        }
    }

    void FireProjectiles()
    {
        timeSinceLastFire += Time.deltaTime;

        if (timeSinceLastFire >= fireInterval)
        {
            timeSinceLastFire = 0f;
            FireInDirection(Vector3.forward);
            FireInDirection(Vector3.back);
            FireInDirection(Vector3.left);
            FireInDirection(Vector3.right);
        }
    }

    void FireInDirection(Vector3 direction)
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        ProjectileController projectileController = projectile.GetComponent<ProjectileController>();
        if (projectileController != null)
        {
            projectileController.Initialize(direction, projectileSpeed);
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
}
