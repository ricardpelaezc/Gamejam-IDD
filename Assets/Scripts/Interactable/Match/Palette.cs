using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palette : Animated
{
    public GameObject PaintedBrush;
    public override void Match()
    {
        PaintedBrush.SetActive(true);
    }
}
