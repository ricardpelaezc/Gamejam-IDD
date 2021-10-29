using System;
using System.Collections.Generic;

public class Passer : Animated
{
    public PuzlePanel Puzle;
    private void Start()
    {
        _animationsNames = new List<string> { null };
        _actionsAfterAnimations = new List<Action> { Puzle.ShowPanel };
    }
}