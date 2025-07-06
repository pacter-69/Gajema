using UnityEngine;

public class Layer1ElementBehaviour : MonoBehaviour
{
    public StarsBehaviour stars;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ice"))
        {

        }
        else if (collision.CompareTag("Wind"))
        {

        }
        else if (collision.CompareTag("Celo"))
        {

        }
        else
        {

        }
    }
}
