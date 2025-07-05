using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public bool buttonPressed;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Sound").GetComponent<AudioManager>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        buttonPressed = !buttonPressed;
        audioManager.PlaySFX(audioManager.button);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        buttonPressed = !buttonPressed;
    }
}
