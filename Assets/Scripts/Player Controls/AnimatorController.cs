using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    Animator animator;
    [SerializeField]
    SpriteDirection _spriteDirection = SpriteDirection.Left;
    private SpriteDirection initDirection;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        initDirection = _spriteDirection;
    }

    public void ChangeSpriteDirection(SpriteDirection newDir)
    {
        switch(newDir)
        {
            case SpriteDirection.Left:
                {
                    break;
                }

            case SpriteDirection.Right:
                {
                    break;
                }
        }
    }
}

public enum SpriteDirection
{
    Up = 0, Right = 1, Down = 2, Left = 3
}
