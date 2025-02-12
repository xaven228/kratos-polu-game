using UnityEngine;

public class LavaScript : MonoBehaviour
{
    public bool isLavaSafe = false; // Whether the lava is safe to walk on

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isLavaSafe)
            {
                GameManager.Instance.GameOver();
            }
        }
    }
}