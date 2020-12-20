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
    float prevVel = 0;

	public override void Awake(StateMachine stateMachine)
	{
        moveCtrl = stateMachine.GetComponent<MoveController>();
        animCtrl = stateMachine.GetComponent<AnimatorController>();
	}
		
	public override void OnUpdate()
	{
        float currVel = moveCtrl.GetVelocity().x;
        if ( currVel > 0 && prevVel <= 0 ) // Starting to move right
        {
            animCtrl.ChangeSpriteDirection(SpriteDirection.Right);
        }

        if( currVel < 0 && prevVel >= 0)
        {
            animCtrl.ChangeSpriteDirection(SpriteDirection.Left);
        }

        prevVel = currVel;
	}
	
	// public override void OnStateEnter()
	// {
	// }
	
	// public override void OnStateExit()
	// {
	// }
}
