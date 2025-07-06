using UnityEngine;
using UnityEngine.Tilemaps;

public class Layer1ElementBehaviour : MonoBehaviour
{
    public StarsBehaviour stars;
    public GameObject layer2;
    public Tilemap star2Tilemap;
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
            layer2.GetComponent<LayerBehaviour>().isIce = true;
            layer2.GetComponent<LayerBehaviour>().isSticky = false;
            layer2.GetComponent<LayerBehaviour>().isWindy = false;

            stars.IceChanger(star2Tilemap);
        }
        else if (collision.CompareTag("Wind"))
        {
            layer2.GetComponent<LayerBehaviour>().isIce = false;
            layer2.GetComponent<LayerBehaviour>().isSticky = false;
            layer2.GetComponent<LayerBehaviour>().isWindy = true;

            stars.WindChanger(star2Tilemap);
        }
        else if (collision.CompareTag("Celo"))
        {
            layer2.GetComponent<LayerBehaviour>().isIce = false;
            layer2.GetComponent<LayerBehaviour>().isSticky = true;
            layer2.GetComponent<LayerBehaviour>().isWindy = false;

            stars.CeloChanger(star2Tilemap);
        }
        else
        {
            layer2.GetComponent<LayerBehaviour>().isIce = false;
            layer2.GetComponent<LayerBehaviour>().isSticky = false;
            layer2.GetComponent<LayerBehaviour>().isWindy = false;

            stars.Floor2Changer(star2Tilemap);
        }
    }
}
