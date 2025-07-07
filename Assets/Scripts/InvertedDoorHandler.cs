using UnityEngine;

public class InvertedDoorHandler : MonoBehaviour
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
        else
        {
            door.SetActive(false);
        }
    }
}
