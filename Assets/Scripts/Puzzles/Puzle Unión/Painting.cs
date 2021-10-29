using System.Collections.Generic;
using UnityEngine;

public class Painting : MonoBehaviour
{
    public bool Selected=false;
    public bool AllUnionConnected; //if all trigges union objects of this gameobject are true. Then this piece is connected.
    [SerializeField] private List<UnionPaintTrigger> triggersUnion = new List<UnionPaintTrigger>();
    private Vector3 mousePos;
    private float smoothTime = 0.05f;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            triggersUnion.Add(transform.GetChild(i).GetComponent<UnionPaintTrigger>());
        }
    }
    private void Update()
    {
        if (Selected && Input.GetMouseButton(0))
            FollowMouse();
        else
            Selected = false;

        if (ComproveAllConnections())
        {
            print(gameObject.name + "Esta pieza está bien unida.");
            AllUnionConnected = true;
        }
           
    }
    
    private bool ComproveAllConnections()
    {
        for (int i = 0; i < triggersUnion.Count; i++)
        {
            if (triggersUnion[i].CorrectPaintBool != true)
                return false;
        }
        return true;
    }
    private void FollowMouse()
    {
        //    Vector3 Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Pos.z = transform.position.z;
        //print(Pos);
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector2 newPos = new Vector3(mousePos.x, mousePos.y, 0);
        transform.position = Vector3.Lerp(transform.position, newPos, smoothTime);//Vector3.MoveTowards(transform.position, Pos, smoothTime * Time.deltaTime);
    }
}
