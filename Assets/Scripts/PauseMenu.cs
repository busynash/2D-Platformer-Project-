using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject Container;
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Container.SetActive(true);
            Time.timeScale = 0; 
        }
    }

    public void ResumeButton()
    {
        Container.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainMenuButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }
}
