using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palette : Animated
{
    //public GameObject PaintedBrush;
    public override void Match()
    {
        _animationsNames = new List<string> { "Match" };
        _actionsAfterAnimations = new List<Action> { null };

        _animator.SetTrigger("Match");
    }
}
