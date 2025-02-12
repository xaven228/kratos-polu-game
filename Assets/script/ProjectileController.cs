using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Vector3 direction;
    private float speed;

    public void Initialize(Vector3 direction, float speed)
    {
        this.direction = direction;
        this.speed = speed;
    }

    void Update()
    {
        // Двигать снаряд в заданном направлении
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
            Destroy(gameObject); // Уничтожить снаряд после столкновения
        }
    }
}
