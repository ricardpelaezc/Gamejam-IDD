using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnionPaintTrigger : MonoBehaviour
{
    public bool CorrectPaintBool;
    public SpriteRenderer CorrectPaint;


    private void OnTriggerEnter(Collider other)
    {
        print("hola");

        print(other.name);
        if (other.gameObject.GetComponentInParent<SpriteRenderer>().sprite == CorrectPaint.sprite)
        {
            print("aaaaaaaa");
            CorrectPaintBool = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CorrectPaintBool = false;
    }

}
