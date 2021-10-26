using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintController : MonoBehaviour
{
    private float maxDistance=200f;
    private GameObject currentSelected;

    public LayerMask m_LayerMask;
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
            PaintSelected();  
    }

    public void PaintSelected()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, maxDistance, m_LayerMask)) //deseleccionamos
        {
            //currentSelected = null;
            hit.collider.GetComponent<Paint>().Selected = true;
        }
    }


}
