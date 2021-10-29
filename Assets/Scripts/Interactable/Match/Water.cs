using UnityEngine;

public class Water : Animated
{
    public override void Match()
    {
        _animator.SetTrigger("Match");
    }
}
