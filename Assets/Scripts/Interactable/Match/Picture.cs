using UnityEngine;

public class Picture : Animated
{
    public PuzlePanel Puzle;
    public override void Match()
    {
        Puzle.ShowPanel();
    }
}
