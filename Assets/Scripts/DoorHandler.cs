using Unity.VisualScripting;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    public ButtonHandler button;
    public GameObject door;
    void Start()
    {
        if (door.GetComponent<SpriteRenderer>() != null)
        {
            door.GetComponent<SpriteRenderer>().color = button.GetComponent<SpriteRenderer>().color;
        }
        else
        {
            foreach (SpriteRenderer spr in door.GetComponentsInChildren<SpriteRenderer>())
            {
                spr.color = button.GetComponent<SpriteRenderer>().color;
            }
        }
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

            if (door.GetComponent<SpriteRenderer>() != null)
            {
                door.GetComponent<SpriteRenderer>().color = button.GetComponent<SpriteRenderer>().color;
            }
            else
            {
                foreach (SpriteRenderer spr in door.GetComponentsInChildren<SpriteRenderer>())
                {
                    spr.color = button.GetComponent<SpriteRenderer>().color;
                }
            }
        }
    }
}
