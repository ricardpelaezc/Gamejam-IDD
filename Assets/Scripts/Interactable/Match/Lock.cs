using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : Animated
{
    public GameObject Door;
    public void Start()
    {
        _animationsNames = new List<string> { "Lock" };
        _actionsAfterAnimations = new List<Action> { DesactiveDoor };//, () => _animator.SetTrigger("Lock"), DesactiveDoor };

        _animator.SetTrigger("Lock");
    }
    public void DesactiveDoor()
    {
        Door.SetActive(false);
    }
}
