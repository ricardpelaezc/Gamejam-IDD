using UnityEngine;

public class Plant : Animated
{
    public PuzlePanel Puzle;
    public override void Match()
    {
        Puzle.ShowPanel();
    }
}
