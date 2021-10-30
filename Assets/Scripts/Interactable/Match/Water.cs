using System;
using System.Collections.Generic;
using UnityEngine;

public class Water : Animated
{
    public override void Match()
    {
        _animator.SetTrigger("Match");
    }
}