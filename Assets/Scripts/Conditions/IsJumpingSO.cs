using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsJumping", menuName = "State Machines/Conditions/Is Jumping")]
public class IsJumpingSO : StateConditionSO
{
	protected override Condition CreateCondition() => new IsJumping();
}

public class IsJumping : Condition
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
