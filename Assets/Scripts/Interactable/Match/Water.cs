using System;
using System.Collections.Generic;
using UnityEngine;

public class Water : Animated
{
    public override void Match()
    {
        _animationsNames = new List<string> { "Match" };
        _actionsAfterAnimations = new List<Action> { null };

        _animator.SetTrigger("Match");
    }
}