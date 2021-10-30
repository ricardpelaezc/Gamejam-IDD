using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameras : MonoBehaviour
{
    public Animator Camera;
    public Camera OrtographicCamera;

    static Cameras m_Cameras;


    private void Awake()
    {
        m_Cameras = this;
    }

    static public Cameras GetCameras() => m_Cameras;
    public void Perspective()
    {

    }

    public void Ortographics()
    {

    }
}
