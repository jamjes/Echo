using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSpriteFunctions : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void OnEnable()
    {
        RewindController.OnEnterRewind += RewindSprite;
        RewindController.OnExitRewind += RegularSprite;
    }

    private void OnDisable()
    {
        RewindController.OnEnterRewind -= RewindSprite;
        RewindController.OnExitRewind -= RegularSprite;
    }

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void RewindSprite()
    {
        _spriteRenderer.color = Color.cyan;
    }

    private void RegularSprite()
    {
        _spriteRenderer.color = Color.white;
    }


}
