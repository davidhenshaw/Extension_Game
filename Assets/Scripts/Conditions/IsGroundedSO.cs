using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsGrounded", menuName = "State Machines/Conditions/Is Grounded")]
public class IsGroundedSO : StateConditionSO
{
	protected override Condition CreateCondition() => new IsGrounded();
}

public class IsGrounded : Condition
{
    Collider2D groundCollider;
    int jumpableLayers = LayerMask.GetMask("Ground"); //The layers that the player can jump from

	public override void Awake(StateMachine stateMachine)
	{
        groundCollider = stateMachine.GetComponent<PlayerSensorHandler>().GetGroundCollider();
	}
		
	protected override bool Statement()
	{
		return groundCollider.IsTouchingLayers(jumpableLayers);
	}
	
	// public override void OnStateEnter()
	// {
	// }
	
	// public override void OnStateExit()
	// {
	// }
}
