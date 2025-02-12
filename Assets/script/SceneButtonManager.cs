using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneButtonManager : MonoBehaviour
{
    public int sceneNumber; // Номер сцены, на которую будет переходить кнопка

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(LoadScene);
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
