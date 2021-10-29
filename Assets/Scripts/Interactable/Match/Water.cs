using UnityEngine;

public class Water : Animated
{
    public GameObject WaterBucket;
    public override void Match()
    {
        WaterBucket.SetActive(true);
        transform.gameObject.SetActive(false);
    }
}
