using UnityEngine;

public class MenuBehaviours : MonoBehaviour
{
    //Conecto con transition
    public FadeTransition fade;

    public void gotomainMenu()
    {
        Debug.Log("HOla");
        fade.FadeToBlack("MainMenu");
    }
}