using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class Animated : Interactable
{
    protected Animator _animator;
    private bool _animationFinished;
    private bool _startedAnimation;
    private AnimatorClipInfo[] _animatorClipInfos => _animator.GetCurrentAnimatorClipInfo(0);
    private int _interactCount;
    protected List<string> _animationsNames = new List<string>();
    protected List<Action> _actionsAfterAnimations = new List<Action>();

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (_animatorClipInfos.Length > 0 && !_startedAnimation)
        {
            StartCoroutine(WaitAnimationTime(_animatorClipInfos[0].clip.length));
            _startedAnimation = true;
        }
        if (_interactCount == _animationsNames.Count && _animationFinished)
        {
            _actionsAfterAnimations[_interactCount - 1]?.Invoke();
            _animationFinished = false;
        }
    }
    public override void Interact() 
    {
        if ((_interactCount == 0 || _animationFinished) && _interactCount < _animationsNames.Count)
        {
            if (_interactCount > 0)
            {
                _actionsAfterAnimations[_interactCount - 1]?.Invoke();
            }
            if (!(_animationsNames[_interactCount] is null))
            {
                _animator.SetTrigger(_animationsNames[_interactCount]);
                _startedAnimation = false;
                _animationFinished = false;
            }
            else
            {
                _animationFinished = true;
            }
            _interactCount++;
        }
    }
    public override void Match()
    {
    }
    IEnumerator WaitAnimationTime(float time)
    {
        yield return new WaitForSeconds(time);
        _animationFinished = true;
        yield break;
    }
}
