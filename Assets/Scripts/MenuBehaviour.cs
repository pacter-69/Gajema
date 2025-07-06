using UnityEngine;

public class MenuBehaviour : MonoBehaviour
{
    //Conecto con transition
    public FadeTransition fade;
    public string levelSelector, tutorial;

    public void StartGame()
    {
        fade.FadeToBlack(levelSelector);
    }

    public void Tutorial()
    {
        fade.FadeToBlack(tutorial);
    }
    
    
    public void Close()
    {
        Application.Quit();
    }
}
