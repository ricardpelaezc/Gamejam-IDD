using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : Animated
{
    public GameObject flower;
    private void Start()
    {
        flower.SetActive(false);
    }
    private void GrowUp()
    {
        flower.SetActive(true);
    }
    public override void Match()
    {
        _animator.SetTrigger("Match");
    }
}
