using UnityEngine;

public class ProjectileController : MonoBehaviour
{
	public enum TargetType { Player, Enemy }

	public TargetType targetType; // Цель снаряда

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
		switch (targetType)
		{
			case TargetType.Player:
				if (other.CompareTag("Player"))
				{
					GameManager.Instance.GameOver();
					Destroy(gameObject); // Уничтожить снаряд после столкновения
				}
				break;
			case TargetType.Enemy:
				if (other.CompareTag("Enemy"))
				{
					Destroy(other.gameObject); // Уничтожить врага
					Destroy(gameObject); // Уничтожить снаряд после столкновения
					// Вызываем метод WinGame при уничтожении врага
        			GameManager.Instance.WinGame();
				}
				break;
		}
	}
}