using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    int _pointer;
    SpriteRenderer _spr;
    float _speed;
    float timeReference;
    bool reverse;

    bool isFading;

    [SerializeField] CustomAnimation _idleAnimation;
    [SerializeField] CustomAnimation _runAnimation;
    [SerializeField] CustomAnimation _jumpAnimation;
    [SerializeField] CustomAnimation _fallAnimation;
    [SerializeField] CustomAnimation _transformAnimation;

    CustomAnimation _currentAnimation;

    public PlayerStateMachine PlayerStateReference;
    public PlayerController PlayerReference;

    void Start()
    {
        _spr = GetComponent<SpriteRenderer>();
        SetState(_idleAnimation);
    }

    void AnimationTick()
    {
        if (timeReference == 0)
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

        timeReference += Time.deltaTime;

        if (timeReference >= +_speed)
        {
            timeReference = 0;
        }
    }

    private void Update()
    {
        AnimationTick();

        if (isFading)
        {
            return;
        }

        if (PlayerReference.Direction > 0
            && _spr.flipX != false)
        {
            _spr.flipX = false;
        }
        else if (PlayerReference.Direction < 0
            && _spr.flipX != true)
        {
            _spr.flipX = true;
        }
        
        switch(PlayerStateReference.CurrentState)
        {
            case PlayerStateMachine.State.Idle:
                SetState(_idleAnimation);
                break;

            case PlayerStateMachine.State.Run:
                SetState(_runAnimation);
                break;

            case PlayerStateMachine.State.Jump:
                SetState(_jumpAnimation);
                break;

            case PlayerStateMachine.State.Fall:
                SetState(_fallAnimation);
                break;
            case PlayerStateMachine.State.Transform:
                StartCoroutine(Transform());
                break;
        }
    }

    void SetState(CustomAnimation target)
    {
        if (_currentAnimation == target)
        {
            return;
        }

        _currentAnimation = target;
        _pointer = 0;
        timeReference = 0;
        _speed = 1f / (float)_currentAnimation.AnimationSpeed;
    }

    IEnumerator Transform()
    {
        CustomAnimation originalState = _currentAnimation;
        isFading = true;
        SetState(_transformAnimation);
        yield return new WaitForSeconds(3f);
        reverse = !reverse;
        isFading = false;
        SetState(originalState);
    }
}
