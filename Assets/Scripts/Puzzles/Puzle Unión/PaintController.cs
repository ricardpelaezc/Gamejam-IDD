using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintController : MonoBehaviour
{
    private float maxDistance=200f;

    public List<Painting> AllPieces;
    public LayerMask m_LayerMask;

    public PuzlePanel m_PuzzlePanel;
    public GameObject Paint; //hijo
    public GameObject PicturePaint; //la foto

    bool once = false;

    Camera m_Camera => Cameras.GetCameras().OrtographicCamera;

    public void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
            PaintSelected();

        if (ComproveAllPiecesConnected() && !once)
        {
            once = true;
            StartCoroutine(Solved());
        }
    }

    public IEnumerator Solved()
    {
        yield return new WaitForSeconds(2f);

        PicturePaint.SetActive(true);
        Cameras.GetCameras().OrtographicCamera.GetComponent<Animator>().SetBool("Start", false);
        Cameras.GetCameras().OrtographicCamera.GetComponent<Animator>().SetBool("End", true);
        m_PuzzlePanel.ResetPanel();
        Cameras.GetCameras().Perspective();

        yield return new WaitForSeconds(2f);
        
        
        Player.GetPlayer().UnlockRoom();
       // m_PuzzlePanel.currentPuzzle++;

        yield return new WaitForSeconds(4f);
        gameObject.SetActive(false);
        IAState.GetIA().SetDoor();
        //gameObject.transform.parent.gameObject.SetActive(false);
    }

    public bool ComproveAllPiecesConnected()
    {
        for (int i = 0; i < AllPieces.Count; i++)
        {
            if (AllPieces[i].AllUnionConnected == false)
                return false;
        }
        return true;
    }

    public void PaintSelected()
    {
        RaycastHit hit;
        Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray, out hit, maxDistance, m_LayerMask))
        {
            print(hit.collider.name);
            hit.collider.GetComponent<Painting>().Selected = true;
        }
        
    }
}
