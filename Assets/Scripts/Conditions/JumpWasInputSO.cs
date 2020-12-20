using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "JumpWasInput", menuName = "State Machines/Conditions/Jump Was Input")]
public class JumpWasInputSO : StateConditionSO
{
	protected override Condition CreateCondition() => new JumpWasInput();
}

public class JumpWasInput : Condition
{
	public override void Awake(StateMachine stateMachine)
	{
	}
		
	protected override bool Statement()
	{
        return Input.GetButtonDown("Jump");
	}
	
	// public override void OnStateEnter()
	// {
	// }
	
	// public override void OnStateExit()
	// {
	// }
}
