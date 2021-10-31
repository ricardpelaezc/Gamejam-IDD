using System;
using System.Collections.Generic;

public class PhotoHung : Animated
{
   // public IAState Puzle;

    private void Start()
    {
        _animationsNames = new List<string> {"Hit"};
        _actionsAfterAnimations = new List<Action> { IAState.GetIA().SetEvent };
    }
}

