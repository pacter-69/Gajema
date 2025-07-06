using UnityEngine;
using UnityEngine.Tilemaps;

public class ElementBehaviour : MonoBehaviour
{
    public StarsBehaviour stars;
    public GameObject layer1;
    public Tilemap star1Tilemap;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        layer1.GetComponent<LayerBehaviour>().isIce = true;
        layer1.GetComponent<LayerBehaviour>().isSticky = true;
        layer1.GetComponent<LayerBehaviour>().isWindy = true;
    }
}
