using System;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject camera1Target, camera2Target, camera3Target;
    private GameObject cameraTarget;
    public GameObject camera1, camera2, camera3;
    public float alpha;
    public float zoomedOutSize, zoomedInSize;
    private Vector3 camera1Origin, camera2Origin, camera3Origin;
    public GameObject activeCamera;
    private bool isZooming, isZoomingOut, playerCanMove;
    private float timer1, timer2;
    public enum EnumCamera
    {
        Playing,
        Zoom1,
        Zoom2,
        Zoomout1,
        Zoomout2
    }
    public EnumCamera enumCamera;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if(activeCamera == camera1)
        {
            activeCamera.GetComponent<Camera>().depth = 1;
            cameraTarget = camera1Target;
            activeCamera.tag = "MainCamera";

            camera2.GetComponent<Camera>().depth = 0;
            camera3.GetComponent<Camera>().depth = -1;
        }
        else if (activeCamera == camera2)
        {
            activeCamera.GetComponent<Camera>().depth = 1;
            cameraTarget = camera2Target;
            activeCamera.tag = "MainCamera";

            camera3.GetComponent<Camera>().depth = 0;
            camera1.GetComponent<Camera>().depth = -1;
        }
        else if (activeCamera == camera3)
        {
            activeCamera.GetComponent<Camera>().depth = 1;
            cameraTarget = camera3Target;
            activeCamera.tag = "MainCamera";

            camera2.GetComponent<Camera>().depth = 0;
            camera1.GetComponent<Camera>().depth = -1;
        }

        switch (enumCamera)
        {
            case EnumCamera.Playing:
                playerCanMove = true;
                break;
            case EnumCamera.Zoom1:
                Zoom();
                break;
            case EnumCamera.Zoom2:
                Zoom2();
                if (!isZooming)
                {
                    if(activeCamera == camera1)
                    {
                        activeCamera = camera2;
                    }
                    else if(activeCamera == camera2)
                    {
                        activeCamera = camera3;
                    }

                    enumCamera = EnumCamera.Playing;
                }
                break;
            case EnumCamera.Zoomout1:
                break;
            case EnumCamera.Zoomout2:
                break;
        }

        activeCamera.transform.position = new Vector3(activeCamera.transform.position.x, activeCamera.transform.position.y, -10);

    }

    public void ChangeCamera(GameObject newCamera)
    {

        if(activeCamera == camera1)
        {
            enumCamera = EnumCamera.Zoom1;
        }

        if(activeCamera == camera2)
        {
            if(newCamera == camera1)
            {
                enumCamera = EnumCamera.Zoomout1;
            }
            else
            {
                enumCamera = EnumCamera.Zoom1;
            }
        }

        if(activeCamera == camera3)
        {
            enumCamera = EnumCamera.Zoomout1;
        }

    }

    public void Zoom()
    {
        isZooming = true;
        timer1 += Time.deltaTime;
        activeCamera.transform.position = Vector3.Lerp(activeCamera.transform.position, cameraTarget.transform.position, (alpha-1.25f) * Time.deltaTime);
        /*if (isZooming &&  (cameraTarget.transform.position - activeCamera.transform.position).magnitude < 0.1)
        {
            enumCamera = EnumCamera.Zoom2;
        }*/
        if(timer1 > 0.8)
        {
            enumCamera = EnumCamera.Zoom2;
            timer1 = 0;
        }
    }

    public void Zoom2()
    {
        timer2 += Time.deltaTime;
        activeCamera.GetComponent<Camera>().orthographicSize = Mathf.Lerp(activeCamera.GetComponent<Camera>().orthographicSize, cameraTarget.transform.localScale.x/2, alpha * Time.deltaTime);
/*        if(1.1f - activeCamera.GetComponent<Camera>().orthographicSize < 0.1)
        {
            isZooming = false;
        }*/
        if (timer2 > 1.35)
        {
            enumCamera = EnumCamera.Zoom2;
            timer2 = 0;
            isZooming = false;
        }
    }

    public void CheckStateChange()
    {

    }
}
