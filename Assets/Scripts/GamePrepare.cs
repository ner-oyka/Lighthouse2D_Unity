using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamePrepare : MonoBehaviour
{
    public Camera CameraCapture2D;
    public Camera CameraCapture3D;
    public RenderTexture ScreenRenderTexture;

    public GameObject BlurPlane2D;
    public GameObject BlurPlane3D;


    void Start()
    {
        CameraCapture2D.gameObject.SetActive(true);
        CameraCapture3D.gameObject.SetActive(true);

        BlurPlane2D.SetActive(true);
        BlurPlane3D.SetActive(true);

        CameraCapture2D.targetTexture = ScreenRenderTexture;
        CameraCapture3D.targetTexture = ScreenRenderTexture;
        
        CameraCapture2D.Render();
        CameraCapture3D.Render();
    }
}
