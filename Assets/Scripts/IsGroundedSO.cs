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
	public override void Awake(StateMachine stateMachine)
	{
	}
		
	protected override bool Statement()
	{
		return true;
	}
	
	// public override void OnStateEnter()
	// {
	// }
	
	// public override void OnStateExit()
	// {
	// }
}
