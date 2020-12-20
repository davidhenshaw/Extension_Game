using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "FlipSprite", menuName = "State Machines/Actions/Flip Sprite")]
public class FlipSpriteSO : StateActionSO
{
	protected override StateAction CreateAction() => new FlipSprite();
}

public class FlipSprite : StateAction
{
    MoveController moveCtrl;
    AnimatorController animCtrl;
    float threshold = 0.3f;

	public override void Awake(StateMachine stateMachine)
	{
        moveCtrl = stateMachine.GetComponent<MoveController>();
        animCtrl = stateMachine.GetComponent<AnimatorController>();
	}
		
	public override void OnUpdate()
	{
        if ( Input.GetAxisRaw("Horizontal") > Mathf.Epsilon) // Starting to move right
        {
            animCtrl.ChangeSpriteDirection(SpriteDirection.Right);
        }

        if(Input.GetAxisRaw("Horizontal") < -Mathf.Epsilon)
        {
            animCtrl.ChangeSpriteDirection(SpriteDirection.Left);
        }
	}
	
	// public override void OnStateEnter()
	// {
	// }
	
	// public override void OnStateExit()
	// {
	// }
}
