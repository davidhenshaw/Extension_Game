using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "WalkWasInput", menuName = "State Machines/Conditions/Walk Was Input")]
public class WalkWasInputSO : StateConditionSO
{
	protected override Condition CreateCondition() => new WalkWasInput();
}

public class WalkWasInput : Condition
{
    float deadZone = 0;
	public override void Awake(StateMachine stateMachine)
	{
	}
		
	protected override bool Statement()
	{
        return Mathf.Abs(Input.GetAxis("Horizontal")) > deadZone;
	}
	
	// public override void OnStateEnter()
	// {
	// }
	
	// public override void OnStateExit()
	// {
	// }
}
