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
        _actionsAfterAnimations = new List<Action> { null, () => _animator.SetTrigger("Lock"), DesactiveDoor };
    }
    public void DesactiveDoor()
    {
        Door.SetActive(false);
    }
}
