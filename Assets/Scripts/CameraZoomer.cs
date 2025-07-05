using System.Drawing;
using UnityEngine;

public class CameraZoomer : MonoBehaviour
{
    public GameObject toZoom;

    // Update is called once per frame
    void Update()
    {
        Zoom();
    }

    void Zoom()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.gameObject.transform.position = new Vector3(toZoom.transform.position.x, toZoom.transform.position.y, -10);
            gameObject.GetComponent<Camera>().orthographicSize = CalculateSize();
        }
    }

    float CalculateSize()
    {
        float size;
        size = toZoom.transform.localScale.y / 2;
        return size;
    }
}
