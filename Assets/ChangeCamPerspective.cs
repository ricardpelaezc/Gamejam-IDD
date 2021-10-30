using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamPerspective : MonoBehaviour
{
    public void CameraToOrthographic()
    {
        Cameras.GetCameras().Ortographics();
    }
}
