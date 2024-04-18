using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    int _pointer;
    SpriteRenderer _spr;
    float _speed;
    float timeReference;
    bool reverse;

    [SerializeField] CustomAnimation _idleAnimation;
    [SerializeField] CustomAnimation _runAnimation;
    [SerializeField] CustomAnimation _jumpAnimation;
    [SerializeField] CustomAnimation _fallAnimation;
    [SerializeField] CustomAnimation _transformAnimation;

    CustomAnimation _currentAnimation;

    void Start()
    {
        _spr = GetComponent<SpriteRenderer>();
        SetState(_idleAnimation);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            reverse = !reverse;
        }
        
        if (Input.GetKeyDown(KeyCode.LeftArrow)
            && _spr.flipX != true)
        {
            _spr.flipX = true;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)
            && _spr.flipX != false)
        {
            _spr.flipX = false;
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha1) 
            && _currentAnimation != _idleAnimation)
        {
            SetState(_idleAnimation);
            
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)
            && _currentAnimation != _runAnimation)
        {
            SetState(_runAnimation);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)
            && _currentAnimation != _jumpAnimation)
        {
            SetState(_jumpAnimation);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4)
            && _currentAnimation != _fallAnimation)
        {
            SetState(_fallAnimation);
        }

        timeReference += Time.deltaTime;

        if (timeReference < _speed)
        {
            return;
        }

        if (_pointer < _currentAnimation.AnimationFrames.Length)
        {
            if (!reverse) _spr.sprite = _currentAnimation.AnimationFrames[_pointer];
            else _spr.sprite = _currentAnimation.ReverseAnimationFrames[_pointer];
            _pointer++;
        }

        if (_pointer == _currentAnimation.AnimationFrames.Length
            && _currentAnimation.Loop)
        {
            _pointer = 0;
        }
        
        timeReference = 0;
    }

    void SetState(CustomAnimation target)
    {
        _pointer = 0;
        timeReference = 0;
        _currentAnimation = target;
        _speed = 1f / (float)_currentAnimation.AnimationSpeed;
    }

}
