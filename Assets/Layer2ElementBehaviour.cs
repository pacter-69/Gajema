using UnityEngine;
using UnityEngine.Tilemaps;

public class Layer2ElementBehaviour : MonoBehaviour
{
    public StarsBehaviour stars;
    public GameObject layer3;
    public Tilemap star3Tilemap;
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
            layer3.GetComponent<LayerBehaviour>().isIce = true;
            layer3.GetComponent<LayerBehaviour>().isSticky = false;
            layer3.GetComponent<LayerBehaviour>().isWindy = false;

            stars.IceChanger(star3Tilemap);
        }
        else if (collision.CompareTag("Wind"))
        {
            layer3.GetComponent<LayerBehaviour>().isIce = false;
            layer3.GetComponent<LayerBehaviour>().isSticky = false;
            layer3.GetComponent<LayerBehaviour>().isWindy = true;

            stars.WindChanger(star3Tilemap);
        }
        else if (collision.CompareTag("Celo"))
        {
            layer3  .GetComponent<LayerBehaviour>().isIce = false;
            layer3.GetComponent<LayerBehaviour>().isSticky = true;
            layer3.GetComponent<LayerBehaviour>().isWindy = false;

            stars.CeloChanger(star3Tilemap);
        }
        else
        {
            layer3.GetComponent<LayerBehaviour>().isIce = false;
            layer3.GetComponent<LayerBehaviour>().isSticky = false;
            layer3.GetComponent<LayerBehaviour>().isWindy = false;

            stars.Floor3Changer(star3Tilemap);
        }
    }
}
