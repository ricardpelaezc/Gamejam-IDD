using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoHung : Animated
{
    public PuzlePanel Puzle;

    private void Start()
    {
        _animationsNames = new List<string> {"Hit", "Hit", "Hit"};
        _actionsAfterAnimations = new List<Action> { null, () => _animator.SetTrigger("Fall"), Puzle.ShowPanel};
    }
}

