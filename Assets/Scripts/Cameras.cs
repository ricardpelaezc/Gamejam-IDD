using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameras : MonoBehaviour
{
    public Camera PerspectiveCamera;
    public Camera OrtographicCamera;


    static Cameras m_Cameras;


    private void Awake()
    {
        m_Cameras = this;
    }

    static public Cameras GetCameras() => m_Cameras;
    public void Perspective()
    {
        PerspectiveCamera.gameObject.SetActive(true);
        OrtographicCamera.gameObject.SetActive(false);
    }

    public void Ortographics()
    {
        PerspectiveCamera.gameObject.SetActive(false);
        OrtographicCamera.gameObject.SetActive(true);
    }
}
