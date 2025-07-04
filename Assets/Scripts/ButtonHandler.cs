using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public bool buttonPressed;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        buttonPressed = !buttonPressed;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        buttonPressed = !buttonPressed;
    }
}
