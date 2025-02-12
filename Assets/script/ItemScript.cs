using UnityEngine;
using UnityEngine.UI;

public class ItemScript : MonoBehaviour
{
    public enum ItemType { LavaProtection, WinItem }
    public ItemType itemType;
    public Text messageText; // Ссылка на UI-текст для отображения сообщений

    private void Start()
    {
        if (messageText != null)
        {
            messageText.text = "Подберите зелье";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (itemType)
            {
                case ItemType.LavaProtection:
                    // Make lava safe
                    LavaScript[] lavaScripts = Object.FindObjectsByType<LavaScript>(FindObjectsSortMode.None);
                    foreach (var lava in lavaScripts)
                    {
                        lava.isLavaSafe = true;
                    }
                    if (messageText != null)
                    {
                        messageText.text = "Добудьте кубок";
                    }
                    Destroy(gameObject); // Remove the item from the scene
                    break;

                case ItemType.WinItem:
                    GameManager.Instance.WinGame();
                    Destroy(gameObject); // Remove the item from the scene
                    break;
            }
        }
    }
}
