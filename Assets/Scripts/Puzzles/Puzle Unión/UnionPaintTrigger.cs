using UnityEngine;

public class UnionPaintTrigger : MonoBehaviour
{
    public bool CorrectPaintBool;
    public SpriteRenderer CorrectPaint;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponentInParent<SpriteRenderer>().sprite == CorrectPaint.sprite)
            CorrectPaintBool = true;
        else if (other.gameObject.GetComponent<SpriteRenderer>() != null)
            if(other.gameObject.GetComponent<SpriteRenderer>().sprite== CorrectPaint.sprite)
                CorrectPaintBool = true;
            else
            CorrectPaintBool = false;
            
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponentInParent<SpriteRenderer>().sprite == CorrectPaint.sprite)
            CorrectPaintBool = true;
        else if (other.gameObject.GetComponent<SpriteRenderer>() != null)
            if (other.gameObject.GetComponent<SpriteRenderer>().sprite == CorrectPaint.sprite)
                CorrectPaintBool = true;
            else
                CorrectPaintBool = false;

    }
    private void OnTriggerExit(Collider other)
    {
        
            CorrectPaintBool = false;
    }

    //private void ComproveEnter(Collider other)
    //{
    //    if (other.gameObject.GetComponentInParent<SpriteRenderer>().sprite == CorrectPaint.sprite)
    //    {
    //        CorrectPaintBool = true;
    //    }
    //    else
    //        CorrectPaintBool = false;
    //}

}
