using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    public ButtonHandler button;
    public GameObject door;
    void Start()
    {
        door.GetComponent<SpriteRenderer>().color = button.GetComponent<SpriteRenderer>().color;
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
