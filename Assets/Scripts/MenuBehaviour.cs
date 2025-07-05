using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour
{
    public string levelSelector, tutorial;

    public void StartGame()
    {
        SceneManager.LoadScene(levelSelector);
    }

    public void Tutorial()
    {
        SceneManager.LoadScene(tutorial);
    }

    public void Close()
    {
        Application.Quit();
    }
}
