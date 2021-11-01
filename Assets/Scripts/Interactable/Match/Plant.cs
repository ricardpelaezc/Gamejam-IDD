using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : Animated
{
    //public PuzlePanel Puzle;    
    public Animator a;
    public override void Match()
    {
        _animationsNames = new List<string> { "Match" };

        _actionsAfterAnimations = new List<Action> { null };

        _animator.SetTrigger("Match");
        StartCoroutine(Grow());
        a.SetBool("Show", false);
    }

    private IEnumerator Grow()
    {
        yield return new WaitForSeconds(3.5f);
        Player.GetPlayer().UnlockRoom();

        IAState.GetIA().SetDoor();

    }
}
