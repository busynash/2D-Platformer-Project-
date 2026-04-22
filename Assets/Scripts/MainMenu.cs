using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Kevin_dev");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
  
}
