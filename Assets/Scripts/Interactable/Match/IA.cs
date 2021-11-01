using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class IA : Animated
{
    public GameObject Zoom;
    public override void Match()
    {
        StartCoroutine(ChangeScene());
    }

    public IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(0.3f);
        Cameras.GetCameras().PerspectiveCamera.GetComponent<Animator>().SetBool("Final", true);
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(2);
    }
}
