using UnityEngine;

public class Lock : Animated
{
    public GameObject Door;
    public override void Match()
    {
        Door.SetActive(false);
    }
}
