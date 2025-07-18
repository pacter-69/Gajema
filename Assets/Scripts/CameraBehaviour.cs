using System;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class CameraBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject camera1Target, camera2Target, camera3Target;
    private GameObject cameraTarget;
    public GameObject camera1, camera2, camera3;
    public float alpha;
    public float zoomedOutSize = 5;
    private Vector3 camera1Origin, camera2Origin, camera3Origin;
    private Vector3 cameraOrigin;
    public GameObject activeCamera;
    [HideInInspector] public GameObject activePlayer;
    private bool isZooming, isZoomingOut;
    public bool playerCanMove = true;
    private float timer1, timer2, timer3, timer4, zoomTimer;

    [SerializeField] private InputActionReference previousLayer;
    [SerializeField] private InputActionReference nextLayer;

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
        camera1Origin = camera1.transform.position;
        camera2Origin = camera2.transform.position;
        camera3Origin = camera3.transform.position;

        if (activeCamera == camera1)
        {
            activeCamera.GetComponent<Camera>().depth = 1;
            cameraTarget = camera1Target;
            activePlayer = cameraTarget;
            activeCamera.tag = "MainCamera";
            cameraOrigin = camera1Origin;

            camera2.GetComponent<Camera>().depth = 0;
            camera3.GetComponent<Camera>().depth = -1;
        }
        else if (activeCamera == camera2)
        {
            activeCamera.GetComponent<Camera>().depth = 1;
            cameraTarget = camera2Target;
            activeCamera.tag = "MainCamera";
            cameraOrigin = camera2Origin;
            activePlayer = cameraTarget;

            camera3.GetComponent<Camera>().depth = 0;
            camera1.GetComponent<Camera>().depth = -1;
        }
        else if (activeCamera == camera3)
        {
            activeCamera.GetComponent<Camera>().depth = 1;
            cameraTarget = camera3Target;
            activeCamera.tag = "MainCamera";
            cameraOrigin = camera3Origin;
            activePlayer = cameraTarget;

            camera2.GetComponent<Camera>().depth = 0;
            camera1.GetComponent<Camera>().depth = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (activeCamera == camera1)
        {
            activeCamera.GetComponent<Camera>().depth = 1;
            cameraTarget = camera1Target;
            activeCamera.tag = "MainCamera";
            cameraOrigin = camera1Origin;
            activePlayer = cameraTarget;

            camera2.GetComponent<Camera>().depth = 0;
            camera3.GetComponent<Camera>().depth = -1;
        }
        else if (activeCamera == camera2)
        {
            activeCamera.GetComponent<Camera>().depth = 1;
            cameraTarget = camera2Target;
            activeCamera.tag = "MainCamera";
            cameraOrigin = camera2Origin;
            activePlayer = cameraTarget;

            camera3.GetComponent<Camera>().depth = 0;
            camera1.GetComponent<Camera>().depth = -1;
        }
        else if (activeCamera == camera3)
        {
            activeCamera.GetComponent<Camera>().depth = 1;
            cameraTarget = camera3Target;
            activeCamera.tag = "MainCamera";
            cameraOrigin = camera3Origin;
            activePlayer = cameraTarget;

            camera2.GetComponent<Camera>().depth = 0;
            camera1.GetComponent<Camera>().depth = -1;
        }

        activeCamera.transform.position = new Vector3(activeCamera.transform.position.x, activeCamera.transform.position.y, -10);

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
                    if (activeCamera == camera1)
                    {
                        activeCamera = camera2;
                    }
                    else if (activeCamera == camera2)
                    {
                        activeCamera = camera3;
                    }

                    enumCamera = EnumCamera.Playing;
                    if (activeCamera == camera1)
                    {
                        activeCamera.transform.position = camera1Origin;
                    }
                    if (activeCamera == camera2)
                    {
                        activeCamera.transform.position = camera2Origin;
                    }
                    if (activeCamera == camera3)
                    {
                        activeCamera.transform.position = camera3Origin;
                        activeCamera.GetComponent<Camera>().orthographicSize = 6.5f;
                    }
                }
                break;
            case EnumCamera.Zoomout1:
                if (!isZoomingOut)
                {
                    activeCamera.transform.position = cameraTarget.transform.position;

                    if (activeCamera == camera3)
                    {
                        activeCamera = camera2;
                        activeCamera.GetComponent<Camera>().orthographicSize = cameraTarget.transform.localScale.x / 2f; //1.8f
                        activeCamera.transform.position = camera2Target.transform.position;
                    }
                    else if (activeCamera == camera2)
                    {
                        activeCamera = camera1;
                        activeCamera.GetComponent<Camera>().orthographicSize = cameraTarget.transform.localScale.x / 2f;
                        activeCamera.transform.position = camera1Target.transform.position;
                    }

                    timer1 = 0;
                    timer2 = 0;
                    timer3 = 0;
                    timer4 = 0;
                    zoomTimer = 0;
                }

                zoomTimer += Time.deltaTime;
                isZoomingOut = true;

                if(zoomTimer > 0.1)
                {
                    Zoomout();
                }
                break;
            case EnumCamera.Zoomout2:
                Zoomout2();
                break;
        }

        if(isZoomingOut || isZooming)
        {
            playerCanMove = false;
        }

        if (previousLayer.action.IsInProgress() && !isZooming && !isZoomingOut)
        {
            if (activeCamera == camera2 || activeCamera == camera3)
            {
                enumCamera = EnumCamera.Zoomout1;
            }
        }

        if (nextLayer.action.IsInProgress() && !isZooming && !isZoomingOut)
        {
            if (activeCamera == camera1 || activeCamera == camera2)
            {
                enumCamera = EnumCamera.Zoom1;
            }
        }
    }

    public void Zoom()
    {
        isZooming = true;
        timer1 += Time.deltaTime;
        activeCamera.transform.position = Vector3.Lerp(activeCamera.transform.position, cameraTarget.transform.position, alpha*2f * Time.deltaTime);

        if (timer1 > 0.6)
        {
            enumCamera = EnumCamera.Zoom2;
            timer1 = 0;
        }
    }

    public void Zoom2()
    {
        timer2 += Time.deltaTime;
        activeCamera.GetComponent<Camera>().orthographicSize = Mathf.Lerp(activeCamera.GetComponent<Camera>().orthographicSize, cameraTarget.transform.localScale.x / 2f, alpha*1.5f * Time.deltaTime);

        if (timer2 > 0.9)
        {
            enumCamera = EnumCamera.Zoom2;
            timer2 = 0;
            isZooming = false;
        }
    }

    public void Zoomout()
    {
        isZoomingOut = true;
        timer3 += Time.deltaTime;
        activeCamera.GetComponent<Camera>().orthographicSize = Mathf.Lerp(activeCamera.GetComponent<Camera>().orthographicSize, zoomedOutSize, alpha*2f * Time.deltaTime);

        if (timer3 > 0.6)
        {
            enumCamera = EnumCamera.Zoomout2;
            timer3 = 0;
        }
    }

    public void Zoomout2()
    {
        timer4 += Time.deltaTime;
        activeCamera.transform.position = Vector3.Lerp(activeCamera.transform.position, cameraOrigin, alpha*2f * Time.deltaTime);

        if (timer4 > 0.9)
        {
            enumCamera = EnumCamera.Playing;
            timer4 = 0;
            isZoomingOut = false;
        }
    }
}
