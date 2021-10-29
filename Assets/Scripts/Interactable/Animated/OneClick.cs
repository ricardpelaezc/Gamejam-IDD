using System;
using System.Collections.Generic;
using UnityEngine;

public class OneClick : Animated
{
    private void Start()
    {
        _animationsNames = new List<string> { "click" };
        _actionsAfterAnimations = new List<Action> { () => transform.GetComponent<Collider>().enabled = false };
    }
}
