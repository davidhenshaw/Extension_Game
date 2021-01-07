using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public readonly string WALKING_BOOL = "isWalking";
    public readonly string DEAD_BOOL = "isDead";

    Animator _animator;
    [SerializeField]
    SpriteDirection _spriteDirection;

    SpriteRenderer _spriteRenderer;

    public Animator Animator { get => _animator; }

    //private SpriteDirection initDirection;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void ChangeSpriteDirection(SpriteDirection newDir)
    {
        if(newDir != _spriteDirection)
        {
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
            _spriteDirection = newDir;
        }
    }
}

public enum SpriteDirection
{
    Right, Left
}
