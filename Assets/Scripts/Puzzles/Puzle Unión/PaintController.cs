using System.Collections.Generic;
using UnityEngine;

public class PaintController : MonoBehaviour
{
    private float maxDistance=200f;

    public List<Painting> AllPieces;
    public LayerMask m_LayerMask;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
            PaintSelected();

        if (ComproveAllPiecesConnected())
            print("Solucionado!!");
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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, maxDistance, m_LayerMask))
            hit.collider.GetComponent<Painting>().Selected = true;
        
    }
}
