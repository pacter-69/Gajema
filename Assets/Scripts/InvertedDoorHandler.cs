using UnityEngine;

public class InvertedDoorHandler : MonoBehaviour
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
            door.SetActive(true);
        }
        else
        {
            door.SetActive(false);
        }
    }
}
