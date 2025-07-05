using UnityEngine;

public class ViewportPositionConvertor : MonoBehaviour
{
    public GameObject viewPortTarget;
    public GameObject cameraLayer1;
    public float xOffset, yOffset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Camera>().rect = new Rect(new Vector2(cameraLayer1.GetComponent<Camera>().ScreenToViewportPoint(viewPortTarget.transform.position).x + xOffset, cameraLayer1.GetComponent<Camera>().ScreenToViewportPoint(viewPortTarget.transform.position).y + yOffset), GetComponent<Camera>().rect.size);


        /*GetComponent<Camera>().rect = new Rect(new Vector2(cameraLayer1.GetComponent<Camera>().ScreenToViewportPoint(viewPortTarget.transform.position).x + xOffset, cameraLayer1.GetComponent<Camera>().ScreenToViewportPoint(viewPortTarget.transform.position).y + yOffset), GetComponent<Camera>().rect.size);
        
        GetComponent<Camera>().WorldToViewportPoint*/
    }
}
