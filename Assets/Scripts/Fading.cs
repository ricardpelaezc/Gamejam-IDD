using UnityEngine;

public class Fading : MonoBehaviour
{
    //private bool RoomsFadeInOut(GameObject roomFadeOut, GameObject roomFadeIn)
    //{
    //    Color roomColor0 = roomFadeOut.GetComponentInChildren<Renderer>().material.color;
    //    Color roomColor1 = roomFadeIn.GetComponentInChildren<Renderer>().material.color;

    //    if (roomColor0.a > 0 && roomColor1.a < 1)
    //    {
    //        float fadeOutAmount = roomColor0.a - (_fadeSpeed * Time.deltaTime);
    //        RoomFade(roomFadeOut, fadeOutAmount);
    //        float fadeInAmount = roomColor1.a + (_fadeSpeed * Time.deltaTime);
    //        RoomFade(roomFadeIn, fadeInAmount);
    //    }
    //    else
    //    {
    //        RoomFade(roomFadeOut, 0);
    //        RoomFade(roomFadeIn, 1);
    //        return false;
    //    }

    //    return true;
    //}
    //private bool RoomsFadeIn(GameObject roomFadeIn)
    //{
    //    Color roomColor1 = roomFadeIn.GetComponentInChildren<Renderer>().material.color;

    //    if (roomColor1.a < 1)
    //    {
    //        float fadeInAmount = roomColor1.a + (_fadeSpeed * Time.deltaTime);
    //        RoomFade(roomFadeIn, fadeInAmount);
    //    }
    //    else
    //    {
    //        RoomFade(roomFadeIn, 1);
    //        return false;
    //    }

    //    return true;
    //}
    //private void RoomFade(GameObject room, float fadeAmount)
    //{
    //    Renderer[] renderers = room.GetComponentsInChildren<Renderer>();
    //    foreach (Renderer renderer in renderers)
    //    {
    //        Color color = renderer.material.color;
    //        color = new Color(color.r, color.g, color.b, fadeAmount);
    //        renderer.material.color = color;
    //    }
    //}
    //private void ChangeChildsLayer(Transform transf, LayerMask layer)
    //{
    //    transf.gameObject.layer = layer;

    //    foreach (Transform child in transf)
    //    {
    //        ChangeChildsLayer(child, layer);
    //    }
    //}
}
