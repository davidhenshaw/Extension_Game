using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsFalling", menuName = "State Machines/Conditions/Is Falling")]
public class IsFallingSO : StateConditionSO
{
	protected override Condition CreateCondition() => new IsFalling();
}

public class IsFalling : Condition
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
