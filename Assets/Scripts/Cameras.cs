using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameras : MonoBehaviour
{
    //public Animator CameraAnim;
    public Camera OrtographicCamera;
    public Camera PerspectiveCamera;

    public GameObject PickColliders;

    static Cameras m_Cameras;
    public Camera CurrentCamera;


    private void Start()
    {
        CurrentCamera = PerspectiveCamera;
    }

    private void Awake()
    {
        m_Cameras = this;
    }

    static public Cameras GetCameras() => m_Cameras;
    public void Perspective()
    {
        CurrentCamera = PerspectiveCamera;
        PerspectiveCamera.gameObject.SetActive(true);
        OrtographicCamera.gameObject.SetActive(false);
        PerspectiveCamera.GetComponent<Animator>().SetBool("ZoomOut", true);
    }

    public void Ortographics()
    {

        PerspectiveCamera.GetComponent<Animator>().SetBool("Zoom",true);

        PerspectiveCamera.GetComponent<Animator>().SetBool("ZoomOut", false);
        StartCoroutine(OrtographicsEnum());
        
    }

    private IEnumerator OrtographicsEnum()
    {
        yield return new WaitForSeconds(2f);
        CurrentCamera = OrtographicCamera;
        PerspectiveCamera.gameObject.SetActive(false);
        OrtographicCamera.gameObject.SetActive(true);

        OrtographicCamera.GetComponent<Animator>().SetBool("Start",true);
    }
}
