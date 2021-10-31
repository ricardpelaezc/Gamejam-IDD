using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameras : MonoBehaviour
{
    public Animator CameraAnim;
    public Camera OrtographicCamera;

    public GameObject PickColliders;
    static Cameras m_Cameras;


    private void Awake()
    {
        m_Cameras = this;
    }

    static public Cameras GetCameras() => m_Cameras;
    public void Perspective()
    {
       // Camera.main.orthographic = false;
        PickColliders.SetActive(true);
    }

    public void Ortographics()
    {
        PickColliders.SetActive(false);
        //Camera.main.orthographic = true;
    }
}
