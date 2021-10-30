using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoHung : Animated
{
    public PuzlePanel Puzle;

    private void Start()
    {
        _animationsNames = new List<string> {"Hit"};
        _actionsAfterAnimations = new List<Action> { Puzle.MakePuzzle};
    }
}

