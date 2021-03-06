using System;
using System.Collections.Generic;
using UnityEngine;

public class Lock : Animated
{
    public GameObject Door; public Animator a;
    public override void Match()
    {
        _animationsNames = new List<string> { "Match" };
        _actionsAfterAnimations = new List<Action> { null };

        _animator.SetTrigger("Match");
        DesactiveDoor(); a.SetBool("Show", false);
    }
    public void DesactiveDoor()
    {
        Door.SetActive(false);
    }
}
