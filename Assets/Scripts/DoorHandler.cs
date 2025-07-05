using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    public ButtonHandler button;
    public GameObject door;
    void Start()
    {
        
    }

    void Update()
    {
        if (button.buttonPressed)
        {
            door.SetActive(false);
        }
        else
        {
            door.SetActive(true);
        }
    }
}
