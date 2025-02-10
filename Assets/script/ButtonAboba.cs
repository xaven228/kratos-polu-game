using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonAboba : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenGame()
    {
        SceneManager.LoadScene(1);
    }


    void Update()
    {
        if (Input.GetKey("escape")) 
        {
            SceneManager.LoadScene(0);   
        }
    }

}